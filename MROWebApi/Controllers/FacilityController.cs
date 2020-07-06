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
using MROWebApi.Services;
using System.Text.RegularExpressions;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
    public class FacilityController : ControllerBase
    {
        #region Facility Constructor
        //private readonly IDataProtector _protector;
        private readonly DBConnectionInfo _info;
        public FacilityController(DBConnectionInfo info)
        {
            _info = info;
            //this._protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.Key);
        }
        #endregion

        #region Get Facility/Facilities
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilities()
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
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("GetFacility/sFacilityID={sFacilityID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> GetFacility(string sFacilityID, string sAdminUserID)
        {
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            Facilities facility = await rpFac.Select(nFacilityID);

            #region Logging
            if (facility.bFacilityLogging) {
                MROLogger logger = new MROLogger(_info);
                string sDescription = "Admin with ID: " + sAdminUserID + " called Get Facility Method for Facility ID: " + sFacilityID;
                logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility By ID", "Manage Facilities");
            }
            #endregion

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
                    return BadRequest("Cannot Add Facility \"" + facility.sFacilityName + "\", Facility with Same Name Exists");
                }
                try
                {
                    #region Data Addition ! from UI
                    facility.bFacilityLogging = false;
                    facility.bActiveStatus = false;
                    facility.dtCreated = DateTime.Now;
                    facility.dtLastUpdate = DateTime.Now;
                    facility.bRequestorEmailVerify = true;
                    #endregion

                    FacilitiesRepository rpFac = new FacilitiesRepository(_info);

                    #region Encrypt Passwords
                    MROLogger password = new MROLogger(_info);
                    facility.sSMTPPassword = password.EncryptString(facility.sSMTPPassword);
                    facility.sFTPPassword = password.EncryptString(facility.sFTPPassword);
                    #endregion

                    int GeneratedID = (int)rpFac.Insert(facility);

                    #region Logging
                    if (facility.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + facility.nCreatedAdminUserID + " called Add Facility Method & Created Facility with ID: " + GeneratedID;
                        logger.LogAdminRecords(facility.nCreatedAdminUserID, sDescription, "Add Facility", "Add Facility");
                    }
                    #endregion

                    Facilities dbFacility = await rpFac.Select(GeneratedID);
                    rpFac.AddDependencyRecordsForFacility(GeneratedID, addFacility.sConnectionString, facility.nCreatedAdminUserID);
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

                #region Encrypt Passwords
                MROLogger password = new MROLogger(_info);
                facility.sSMTPPassword = password.EncryptString(facility.sSMTPPassword);
                facility.sFTPPassword = password.EncryptString(facility.sFTPPassword);
                #endregion

                if (rpFac.Update(facility))
                {
                    #region Logging
                    if (facility.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + facility.nUpdatedAdminUserID + " called Edit Facility Method for Facility ID: " + facility.nFacilityID;
                        logger.LogAdminRecords(facility.nUpdatedAdminUserID, sDescription, "Edit Facility", "Edit Facility");
                    }
                    #endregion
                    return Ok();
                }
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
        [HttpPost("ToggleFacility")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> ToggleFacility(ToggleFacility toggleFacility)
        {
            int nFacilityID = toggleFacility.nFacilityID;
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", nFacilityID);
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities facilityDB = await rpFac.Select(nFacilityID);
            if (nFacilityID != facilityDB.nFacilityID)
            {
                return BadRequest();
            }
            //Validate Facility Before Toggling Active Status
            if (ValidateFacility(facilityDB, locationList, _info))
            {
                if (await rpFac.ToggleSoftDelete("bActiveStatus", nFacilityID))
                {
                    #region Logging
                    if (facilityDB.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + toggleFacility.nAdminuserID + " called Toggle Facility Method for Facility ID: " + nFacilityID;
                        logger.LogAdminRecords(toggleFacility.nAdminuserID, sDescription, "Toggle Facility By ID", "Manage Facilities");
                    }
                    #endregion
                    return Ok();
                }
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
                IEnumerable<FacilityConnections> connections = await connectionRepo.SelectWhere("nFacilityID", nFacilityID);

                if (connections != null && connections.Count() == 1) {
                    if (connections.First().sGUID != null)
                    {
                        MROHelperRepository helperRepo = new MROHelperRepository(_info);
                        MROHelper helper = await helperRepo.Select(1);
                        string patternGUID = @"\bMROFacilityGuid\b";
                        string replaceGUID = connections.First().sGUID;
                        helper.sFacilityURL = Regex.Replace(helper.sFacilityURL, patternGUID, replaceGUID);
                        return Ok(helper.sFacilityURL);
                    }
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

        #region HTML Code Call
        [HttpGet("GetHTMLButtonCode/sFacilityID={sFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<string>> GetHTMLButtonCode(string sFacilityID)
        {
            try
            {
                bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                Facilities dbFacility = await rpFac.Select(nFacilityID);
                IEnumerable<FacilityConnections> connections = await connectionRepo.SelectWhere("nFacilityID", dbFacility.nFacilityID);
                FacilityConnections connection = connections.FirstOrDefault();
                MROHelperRepository helperRepo = new MROHelperRepository(_info);
                MROHelper helper = await helperRepo.Select(1);
                string patternGUID = @"\bMROFacilityGuid\b";
                string replaceGUID = connection.sGUID;
                helper.sFacilityButtonHTMLCode = Regex.Replace(helper.sFacilityButtonHTMLCode, patternGUID, replaceGUID);
                string patternFname = @"\bMROFacilityName\b";
                string replaceFname = dbFacility.sFacilityName;
                helper.sFacilityButtonHTMLCode = Regex.Replace(helper.sFacilityButtonHTMLCode, patternFname, replaceFname);
                return helper.sFacilityButtonHTMLCode;
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
