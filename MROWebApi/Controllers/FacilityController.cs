using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
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

            //Decrypting
            MROLogger decrypt = new MROLogger(_info);
            facility.sSMTPPassword = decrypt.DecryptString(facility.sSMTPPassword);
            facility.sFTPPassword = decrypt.DecryptString(facility.sFTPPassword);

            #region Logging
            if (facility.bFacilityLogging) {
                //MROLogger logger = new MROLogger(_info);
                //string sDescription = "Get Facility Method was called for Facility ID: " + sFacilityID;
                //logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility By ID", "Manage Facilities");
                AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                string sDescription = "Get Facility Method was called for Facility ID: " + sFacilityID;
                AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                {
                    nAdminUserID = nAdminUserID,
                    sDescription = sDescription,
                    sModuleName = "Manage Facilities",
                    sEventName = "Get Facility By ID",
                    nFacilityID = facility.nFacilityID,
                    dtLogTime = DateTime.Now,
                };
                adminModuleLoggerRepository.Insert(adminModuleLogger);
            }
            #endregion

            if (facility == null)
                return NotFound();
            return facility;
        }
        #endregion

        #region Get MRO Connection String 
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMROConnectionString()
        {
            MROConnectionStringRepository mROConnectionStringRepository = new MROConnectionStringRepository(_info);
            //bool reslt = Int32.TryParse(id, out int number);
            IEnumerable<MROConnectionString> arrayconnectionString = await mROConnectionStringRepository.GetAllASC(1000,"nConnectionId");
            return Ok(arrayconnectionString);
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
                    facility.bFacilityLogging = true;
                    facility.bActiveStatus = false;
                    facility.dtCreated = DateTime.Now;
                    facility.dtLastUpdate = DateTime.Now;
                    facility.bRequestorEmailVerify = false;
                    #endregion

                    FacilitiesRepository rpFac = new FacilitiesRepository(_info);

                    #region Encrypt Passwords
                    MROLogger password = new MROLogger(_info);
                    facility.sSMTPPassword = password.EncryptString(facility.sSMTPPassword);
                    facility.sFTPPassword = password.EncryptString(facility.sFTPPassword);
                    #endregion

                    int GeneratedID = (int)rpFac.Insert(facility);             

                    Facilities dbFacility = await rpFac.Select(GeneratedID);

                    #region Logging
                    if (dbFacility.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Add Facility Method was called & Created Facility with ID: " + GeneratedID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = dbFacility.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Manage Facilities",
                            sEventName = "Add Facility",
                            nFacilityID= dbFacility.nFacilityID
                        };
                        logger.InsertAuditSingle(dbFacility, adminModuleLogger);
                    }
                    #endregion

                    rpFac.AddDependencyRecordsForFacility(GeneratedID, addFacility.nConnectionID, facility.nCreatedAdminUserID);
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
        public async Task<ActionResult<Facilities>> EditFacility(int id, Facilities facility)
        {            
            if (ModelState.IsValid) {
                FacilitiesRepository fpRepo = new FacilitiesRepository(_info);
                if (id != facility.nFacilityID)
                {
                    return BadRequest("Bad Request: ID Not Equals Facility ID");
                }
                else
                {
                    //Check if there's a facility with same name                     
                    IEnumerable<Facilities> dbFacilitites = await fpRepo.SelectWhere("sFacilityName", facility.sFacilityName);                   
                    if (dbFacilitites.Count() != 0)
                    {
                        if (dbFacilitites.First().nFacilityID != facility.nFacilityID)
                        {
                            //Exit
                            return BadRequest("Cannot Add Facility \"" + facility.sFacilityName + "\", Facility with Same Name Exists");
                        }
                    }
                }
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);

                #region Encrypt Passwords
                MROLogger password = new MROLogger(_info);
                facility.sSMTPPassword = password.EncryptString(facility.sSMTPPassword);
                facility.sFTPPassword = password.EncryptString(facility.sFTPPassword);
                #endregion

                IEnumerable<Facilities> dbOldFacilitites = await fpRepo.SelectWhere("nFacilityID", facility.nFacilityID);
                Facilities oldFacility = dbOldFacilitites.FirstOrDefault();
                if (rpFac.Update(facility))
                {
                    #region Logging
                    if (facility.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Edit Facility Method was called for Facility ID: " + facility.nFacilityID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = facility.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Manage Facilities",
                            sEventName = "Edit Facility",
                            nFacilityID=facility.nFacilityID
                        };
                      logger.UpdateAuditSingle(oldFacility, facility, adminModuleLogger);             
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

        #region Test SMTP
        //TestSMTP
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult> TestSMTPDetails(TestSMTP testSMTP)
        {

            #region Decrypt SMTP Password
            MROLogger password = new MROLogger(_info);
            #endregion

            //From 
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("From",testSMTP.sOutboundEmail);
            message.From.Add(from);

            //To

            MailboxAddress to = new MailboxAddress("To", testSMTP.sToTestEmail);
            message.To.Add(to);

            //Subject
            message.Subject = "Test SMTP Email";
            BodyBuilder bodyBuilder = new BodyBuilder();
            string bodyText = "This is just test email.";
            bodyBuilder.HtmlBody = bodyText;
            message.Body = bodyBuilder.ToMessageBody();
            //GET Port number
            //Make SSL true
            try
            {
                if (testSMTP.sSMTPUrl.Contains("protection"))
                {
                    SmtpClient client = new SmtpClient();
                    client.Connect(testSMTP.sSMTPUrl, 25, false);
                    client.Capabilities &= ~SmtpCapabilities.Pipelining;
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
                else
                {
                    SmtpClient client = new SmtpClient();
                    client.Connect(testSMTP.sSMTPUrl, 25, false);
                    client.Authenticate(testSMTP.sSMTPUsername, testSMTP.sSMTPPassword);
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
                //client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Test SMTP Failed", ex.Message, _info);
                return BadRequest(ex.Message);
            }
            return Ok();
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
                        AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                        string sDescription = "Toggle Facility Method was called for Facility ID: " + nFacilityID;
                        //logger.LogAdminRecords(toggleFacility.nAdminuserID, sDescription, "Toggle Facility By ID", "Manage Facilities");
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = facilityDB.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Manage Facilities",
                            sEventName = "Toggle Facility",
                            sNewValue = "{Active Status : " + !facilityDB.bActiveStatus + "}",
                            sOldValue= "{Active Status : " + facilityDB.bActiveStatus + "}",
                            dtLogTime=DateTime.Now,
                            nFacilityID= facilityDB.nFacilityID
                        };
                        adminModuleLoggerRepository.Insert(adminModuleLogger);
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
