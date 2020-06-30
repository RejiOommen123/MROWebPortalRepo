using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MRODBL.Entities;
using MRODBL.BaseClassRepositories;
using MRODBL.BaseClasses;


namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityController : ControllerBase
    {
        #region Facility Constructor
        private readonly DBConnectionInfo _info;
        public FacilityController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility/Facilities
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacility()
        {
            try 
            {
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                IEnumerable<Facilities> facilities = await rpFac.GetAllASC(1000, "sFacilityName");
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                IList<FacilitiesList> facilitiesList = new List<FacilitiesList>();
                foreach (Facilities fac in facilities)
                {
                    FacilitiesList list = new FacilitiesList();
                    list.Facilities = fac;
                    list.nFacLocCount = await facilityLocationsRepository.CountWhere("nFacilityID", fac.nFacilityID);
                    
                    facilitiesList.Add(list);
                }
                return Ok(facilitiesList);
            }
            catch (Exception exp) {
                return Content(exp.Message);
            }
        }

        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> GetFacility(string id)
        {
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            bool result = int.TryParse(id, out int nId);
            Facilities facility = await rpFac.Select(nId);
            if (facility == null)
                return NotFound();
            return facility;
        }
        #endregion

        #region Add Facility
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> AddFacility(AddFacility addFacility)
        {
            if (ModelState.IsValid)
            {
                Facilities facility = addFacility.cFacility;
                //Check if there's a facility with same name 
                FacilitiesRepository fpRepo = new FacilitiesRepository(_info);
                IEnumerable<Facilities> dbFacilitites = await fpRepo.SelectWhere("sFacilityName", facility.sFacilityName);
                if (dbFacilitites.Count() != 0) {
                    //Exit
                    return BadRequest("Cannot Add Facility with Same Name");
                }
                try
                {
                    #region Data Addition ! from UI
                    facility.bFacilityLogging = true;
                    facility.bActiveStatus = false;
                    facility.nUpdatedAdminUserID = 1;
                    facility.dtCreated = DateTime.Now;
                    facility.nCreatedAdminUserID = 1;
                    facility.nUpdatedAdminUserID = 1;
                    facility.dtLastUpdate = DateTime.Now;
                    #endregion

                    FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                    int GeneratedID = (int)rpFac.Insert(facility);
                    Facilities dbFacility = await rpFac.Select(GeneratedID);
                    rpFac.AddDependencyRecordsForFacility(GeneratedID,addFacility.sConnectionString, 1);
                    return dbFacility;
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
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

        #region Edit Facility
        [HttpPost("EditFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<Facilities> EditFacility(int id, Facilities facility)
        {
            if (ModelState.IsValid) {
                if (id != facility.nFacilityID)
                {
                    return BadRequest("Bad Request: ID Not Equals Facility ID");
                }
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                if (rpFac.Update(facility))
                { return NoContent(); }
                else
                { return NotFound(); }
            }
            else {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion

        #region Toggle Facility Active Status
        [HttpPost("DeleteFacility")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> DeleteFacility([FromBody] int id)
        {

            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", id);
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities facilityDB = await rpFac.Select(id);
            if (id != facilityDB.nFacilityID)
            {
                return BadRequest();
            }
            //Validate Facility Before Toggling Active Status
            if (ValidateFacility(facilityDB, locationList,_info))
            {
                if (await rpFac.ToggleSoftDelete("bActiveStatus", id))
                { return Ok(); }
                else
                { return NotFound(); }
            }
            else {
                return Content("Cannot Activate Facility, Location Count = 0");
            }
        }
        #endregion

        #region Check Facility Exists
        private async Task<bool> FacilityExistsAsync(int id)
        {
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities f = await rpFac.Select(id);
            return f == null;
        }
        #endregion

        #region Get Facility GUID
        [HttpGet("GetFacilityGUID/{nFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityGUID(int nFacilityID) {
            try
            {
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                IEnumerable<FacilityConnections> connections = await connectionRepo.SelectWhere("nFacilityID",nFacilityID);

                if (connections!=null&&connections.Count()==1) {
                    if (connections.First().sGUID!=null)
                        return Ok(connections.First().sGUID);
                    else
                        return NoContent();
                }
                return Content("No Such Facility");
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion

        #region Validate Facility
        private static bool ValidateFacility(Facilities facility, IEnumerable<FacilityLocations> locationList, DBConnectionInfo _info)
        {

            if (facility.bActiveStatus)
            {
                //Trying to deactivate
                return true;
            }
            else
            {
                //Trying to activate
                //Check Location Count for that Facility if its greater than 0 & then allow to activate, else don't
                return locationList.Count() > 0;
            }
        }
        #endregion
    }
}
