using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System.Text.RegularExpressions;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityLocationsController : ControllerBase
    {
        #region Facility Locations Constructor
        private readonly DBConnectionInfo _info;
        public FacilityLocationsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility Location/Locations
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{id}")]
        public async Task<FacilityLocations> GetFacilityLocationSingle(string id)
        {
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            bool result = int.TryParse(id, out int nId);
            return await facilityLocationsRepository.Select(nId);
        }

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
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
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
                        //Exit
                        return BadRequest("Cannot Add Location with Same Name");
                    }

                    #region Data ! From UI
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
                    #endregion

                    if (facilityLocation.sAuthTemplate != "")
                    {
                        facilityLocation.sAuthTemplate = facilityLocation.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                        byte[] pdfByteArray = Convert.FromBase64String(facilityLocation.sAuthTemplate);
                        ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                        IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                        LocationAuthorizationDocumentController locationAuthorizationDocumentCntrl = new LocationAuthorizationDocumentController();
                        checkPDF = locationAuthorizationDocumentCntrl.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);
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
                        if (locationList.Count() == 1)
                        {
                            //Making facility active, if the location added was the first one
                            FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                            await facilitiesRepository.ToggleSoftDelete("bActiveStatus", facilityLocation.nFacilityID);
                        }
                        return Ok(sValidationTextGlobal);
                    }
                    else
                        return Content("Location Cannot be Added");
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
                        return BadRequest("Bad Request: ID Not Equals Location ID");
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
                        LocationAuthorizationDocumentController locationAuthorizationDocumentCntrl = new LocationAuthorizationDocumentController();
                        checkPDF = locationAuthorizationDocumentCntrl.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);
                        if (checkPDF)
                        {
                            await facilityLocationsRepository.ToggleSoftDelete("bLocationActiveStatus", facilityLocation.nFacilityLocationID);

                        }
                    }
                    facilityLocation.nUpdatedAdminUserID = 1;
                    facilityLocation.dtLastUpdate = DateTime.Now;
                    return facilityLocationsRepository.Update(facilityLocation) ? Ok("Sucess") : (IActionResult)NoContent();
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

        #region Toggle Facility Location
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
            if (location.bLocationActiveStatus)
            {
                //User trying to deactivate
                //if this is the last location user is deactivating, make Facility state Deactivate, else normal
                if (locationList.Count() == 1)
                {
                    FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                    await facilitiesRepository.ToggleSoftDelete("bActiveStatus", location.nFacilityID);
                }
                return true;
            }
            else 
            {
                //User trying to Activate
                bool checkPDF = false;
                if (location.sAuthTemplate!="") {
                    byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);
                    ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                    IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                    LocationAuthorizationDocumentController locationAuthorizationDocumentCntrl = new LocationAuthorizationDocumentController();
                    checkPDF = locationAuthorizationDocumentCntrl.ValidateAuthorizationDocument(pdfByteArray, validationRules, out string sValidationTextGlobal);
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

        #region Get ROI Location ID
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
