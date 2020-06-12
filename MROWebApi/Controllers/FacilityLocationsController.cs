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
            var facilityLocations = await facilityLocationsRepository.GetAll(1000, "nFacilityLocationID");
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
                IEnumerable<dynamic> locations = await facilityLocationsRepository.InnerJoin("nFacilityID", "nFacilityID", "tblFacilityLocations", "tblFacilities");
                locations = locations.Where(c => c.nFacilityID == facilityID);
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
            try
            {
                //Data which not present in data coming from UI
                facilityLocation.sAuthTemplate = "T Template";
                facilityLocation.nCreatedAdminUserID = 1;
                facilityLocation.dtCreated = DateTime.Now;
                facilityLocation.nUpdatedAdminUserID = 1;
                facilityLocation.dtLastUpdate = DateTime.Now;


                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                if (facilityLocationsRepository.Insert(facilityLocation) != null)
                {
                    IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", facilityLocation.nFacilityID);

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
            try
            {
                if (id != facilityLocation.nFacilityLocationID)
                {
                    return BadRequest();
                }
                facilityLocation.nUpdatedAdminUserID = 1;
                facilityLocation.dtLastUpdate = DateTime.Now;
                //_context.Entry(facilityLocation).State = EntityState.Modified;

                //await _context.SaveChangesAsync();
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
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
                //lnkROIFacilityLocations facilityLocation = _context.lnkROIFacilityLocations.First(x => x.nLocationID == id);
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                FacilityLocations location = await facilityLocationsRepository.Select(id);
                if (id != location.nFacilityLocationID)
                {
                    return BadRequest();
                }
                //Checking count==1 before delete
                //IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.GetAll(1000, "nFacilityLocationID");


                IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", location.nFacilityID);
                //locationList = locationList.Where(c => c.nFacilityID == location.nFacilityID);
                if (locationList.Count() == 1)//last loc for field
                {
                    FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                    await facilitiesRepository.ToggleSoftDelete("bActiveStatus", location.nFacilityID);
                }
                if (facilityLocationsRepository.Delete(id))
                {
                    return Ok("Success");
                }
                else
                {
                    return NoContent();
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

    }
}
