using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstMigration.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MROWebApi.Context;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System.Text;
using System.Text.RegularExpressions;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityLocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DBConnectionInfo _info;

        #region Facility Location Constructor
        public FacilityLocationsController(ApplicationDbContext context, DBConnectionInfo info)
        {
            _context = context;
            _info = info;
        }
        #endregion

        #region Get Facility Location
        // GET: api/Facility Location
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{id}")]
        public async Task<FacilityLocations> GetFacilityLocationSingle(string id)
        {
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            bool result = int.TryParse(id, out int nId);
            return await facilityLocationsRepository.Select(nId);
        }

        // GET: api/FacilityLocation/5
        [HttpGet("GetFacilityLocation/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IEnumerable<FacilityLocations>> GetFacilityLocation(string id)
        {
            bool result = int.TryParse(id, out int nId);
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            var facilityLocations = await facilityLocationsRepository.GetAllASC(1000, "nFacilityLocationID");
            facilityLocations = facilityLocations.Where(c => c.nFacilityID == nId);
            return facilityLocations;
        }


        [HttpGet("GetFacilityLocationByFacilityID/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityLocationByFacilityID(string id)
        {
            try
            {
                bool result = int.TryParse(id, out int facilityID);
                //var locations = (from fln in _context.lnkROIFacilityLocations
                //                 join f in _context.tblROIFacilities
                //               on fln.nROIFacilityID.ToString() equals f.nROIFacilityID.ToString()
                //                 where fln.nROIFacilityID == facilityID
                //                 select new
                //                 {
                //                     LocationID = fln.nLocationID,
                //                     LocationCode = fln.sLocationCode,
                //                     LocationName = fln.sLocationName,
                //                     LocationAddress = fln.sLocationAddress,
                //                     PhoneNo = fln.sPhoneNo,
                //                     FaxNo = fln.sFaxNo,
                //                     FacilityId = fln.nROIFacilityID,
                //                     FacilityName = f.sFacilityName,
                //                     FacilityDescription = f.sDescription,
                //                 }).ToList();
                //lnkROIFacilityLocations falo = _context.lnkROIFacilityLocations.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
                //var faloName = falo.nROIFacility.sFacilityName;
                //return Ok(new { locations, faloName });
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                //IEnumerable<dynamic> locations = await facilityLocationsRepository.InnerJoin("nFacilityID", "nFacilityID", "tblFacilityLocations", "tblFacilities");
                //locations = locations.Where(c => c.nFacilityID == facilityID);


                IEnumerable<dynamic> locations = await facilityLocationsRepository.GetLocationsList(facilityID);
                FacilitiesRepository facilityRepo = new FacilitiesRepository(_info);
                Facilities facility = await facilityRepo.Select(facilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                return Ok(new { locations, faciName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Add Facility Location
        // POST: api/FacilityLocation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<FacilityLocations>> AddFacilityLocation(FacilityLocations facilityLocation)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info); 
                    IEnumerable<FacilityLocations> dbLocations = await facilityLocationsRepository.SelectWhere("sLocationName", facilityLocation.sLocationName);
                    if (dbLocations.Count() != 0)
                    {
                        //exit
                        return BadRequest("Cannot Add Location with Same Name");
                    }
                    //Data which not present in data coming from UI
                    //Added Lin efor Checkout
                    int? addedLocationID = null;
                    bool checkPDF = false;
                    string sValidationTextGlobal = "";
                    facilityLocation.bLocationActiveStatus = false;
                    facilityLocation.nCreatedAdminUserID = 1;
                    facilityLocation.dtCreated = DateTime.Now;
                    facilityLocation.nUpdatedAdminUserID = 1;
                    facilityLocation.dtLastUpdate = DateTime.Now;
                    string removedSpecialChar = Regex.Replace(facilityLocation.sLocationName, @"[^0-9a-zA-Z]+", "");
                    string finalString = Regex.Replace(removedSpecialChar, @"\s+", "");
                    finalString = "MRO" + finalString;
                    facilityLocation.sNormalizedLocationName = finalString;
                    facilityLocation.nAuthExpirationMonths = 6;
                    if (facilityLocation.sAuthTemplate != "")
                    {
                        facilityLocation.sAuthTemplate = facilityLocation.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                        byte[] pdfByteArray = Convert.FromBase64String(facilityLocation.sAuthTemplate);
                        ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                        IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                        LocationAuthorizationDocument locationAuthorizationDocument = new LocationAuthorizationDocument();
                        checkPDF = locationAuthorizationDocument.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);

                    }
                    addedLocationID = facilityLocationsRepository.Insert(facilityLocation);
                    if (addedLocationID != null)
                    {
                        if (checkPDF)
                        {
                            await facilityLocationsRepository.ToggleSoftDelete("bLocationActiveStatus", (int)addedLocationID);
                        }
                        FacilitiesRepository fRepo = new FacilitiesRepository(_info);
                        Facilities facility = new Facilities();
                        facility = await fRepo.Select(facilityLocation.nFacilityID);
                        IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", facilityLocation.nFacilityID);
                        //facility.nFacLocCount = locationList.Count();
                        fRepo.Update(facility);
                        if (locationList.Count() == 1)
                        {
                            //Making BActive to True
                            FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                            await facilitiesRepository.ToggleSoftDelete("bActiveStatus", facilityLocation.nFacilityID);
                        }
                        return Ok("Success");
                    }
                    else
                        return NoContent();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion

        #region Edit Facility Location
        // PUT: api/FacilityLocation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditFacilityLocation/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityLocation(int id, FacilityLocations facilityLocation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != facilityLocation.nFacilityLocationID)
                    {
                        return BadRequest();
                    }
                    bool checkPDF = true;
                    FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                    string sValidationTextGlobal = "";
                    if (facilityLocation.sAuthTemplate != "")
                    {
                        facilityLocation.sAuthTemplate = facilityLocation.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                        byte[] pdfByteArray = Convert.FromBase64String(facilityLocation.sAuthTemplate);
                        ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                        IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                        LocationAuthorizationDocument locationAuthorizationDocument = new LocationAuthorizationDocument();
                        checkPDF = locationAuthorizationDocument.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);
                        if (checkPDF)
                        {
                            await facilityLocationsRepository.ToggleSoftDelete("bLocationActiveStatus", facilityLocation.nFacilityLocationID);

                        }
                    }
                    //if (continueAhead)
                    //{
                    facilityLocation.nUpdatedAdminUserID = 1;
                    facilityLocation.dtLastUpdate = DateTime.Now;
                    return facilityLocationsRepository.Update(facilityLocation) ? Ok("Sucess") : (IActionResult)NoContent();
                    //}
                    //else
                    //    return Content(sValidationTextGlobal);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await FacilityLocationExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion

        #region Remove Facility Location
        // DELETE: api/FacilityLocation/
        [HttpPost("DeleteFacilityLocation")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> DeleteFacilityLocation([FromBody] int id)
        {
            try
            {
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                FacilityLocations location = await facilityLocationsRepository.Select(id);
                if (id != location.nFacilityLocationID)
                {
                    return BadRequest();
                }

                IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", location.nFacilityID);
                locationList = locationList.Where(c => c.bLocationActiveStatus == false);
                if (await ValidateFacilityLocation(location, locationList, _info))
                {
                    if (await facilityLocationsRepository.ToggleSoftDelete("bLocationActiveStatus", id))
                    {
                        return Ok("Success");
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return Content("Provide Valid Authorization PDF");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await FacilityLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Facility Location Exist
        //To check the Facility Location Exisit
        private async Task<bool> FacilityLocationExists(int id)
        {
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            FacilityLocations location = await facilityLocationsRepository.Select(id);
            return location == null;
        }
        #endregion

        #region ValidateFacilityLocation
        private async static Task<bool> ValidateFacilityLocation(FacilityLocations location, IEnumerable<FacilityLocations> locationList, DBConnectionInfo _info)
        {
            if (location.bLocationActiveStatus)//means user trying to deactivate
            {
                //if last location in deactivate state fac as well, else normal
                if (locationList.Count() == 1)
                {
                    FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                    await facilitiesRepository.ToggleSoftDelete("bActiveStatus", location.nFacilityID);
                }
                return true;
            }
            else //trying to activate
            {
                bool checkPDF = false;
                if (location.sAuthTemplate!="") {
                    byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);
                    ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                    IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                    LocationAuthorizationDocument locationAuthorizationDocument = new LocationAuthorizationDocument();
                    checkPDF = locationAuthorizationDocument.ValidateAuthorizationDocument(pdfByteArray, validationRules, out string sValidationTextGlobal);
                    FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                    Facilities fac = await facilitiesRepository.Select(location.nFacilityID);
                    if (checkPDF && location.sAuthTemplate != "" && !fac.bActiveStatus)
                    {
                        await facilitiesRepository.ToggleSoftDelete("bActiveStatus", location.nFacilityID);
                    }
                }
                return location.sAuthTemplate != "" && checkPDF;
            }
        }
        #endregion

        #region GET ROI Location ID
        [HttpGet]
        [Route("[action]/{id}")]
        public int GetROILocationID(string id)
        {
            FacilityLocationsRepository locationsRepository = new FacilityLocationsRepository(_info);
            bool reslt = Int32.TryParse(id, out int number);
            int nROILocationID = locationsRepository.GetROILocationID("nFacilityID", number);
            return nROILocationID;
        }
        #endregion

    }
}
