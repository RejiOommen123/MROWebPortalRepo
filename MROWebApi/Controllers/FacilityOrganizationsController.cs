using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
    public class FacilityOrganizationsController : ControllerBase
    {
        #region Facility Organizations Constructor
        private readonly DBConnectionInfo _info;
        public FacilityOrganizationsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility Location/Locations
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/sFacilityOrgID={sFacilityOrgID}&sAdminUserID={sAdminUserID}")]
        public async Task<FacilityOrganizations> GetFacilityOrganizationSingle(string sFacilityOrgID,string sAdminUserID)
        {
            FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
            bool resultFacilityOrganization = int.TryParse(sFacilityOrgID, out int nFacilityOrgID);
            bool resultAdminUserID = int.TryParse(sAdminUserID, out int nAdminUserID);

            FacilityOrganizations organization =  await facilityOrganizationsRepository.Select(nFacilityOrgID);

            #region Logging
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities facility = await rpFac.Select(organization.nFacilityID);
            if (facility.bFacilityLogging) {
                //MROLogger logger = new MROLogger(_info);
                //string sDescription = "Get Location Method was called for Location ID: " + sFacilitylocationID;
                //logger.LogAdminRecords(nAdminUserID, sDescription, "Get Location By ID", "Manage Locations");
                AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                string sDescription = "Get Organization Method was called for Organization ID: " + nFacilityOrgID;
                AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                {
                    nAdminUserID = nAdminUserID,
                    sDescription = sDescription,
                    sModuleName = "Manage Organizations",
                    sEventName = "Get Organization By ID",
                    nFacilityID = organization.nFacilityID,
                    dtLogTime = DateTime.Now,
                };
                adminModuleLoggerRepository.Insert(adminModuleLogger);
            }
            #endregion
            return organization;
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

        [HttpGet("GetFacilityOrganizationsByFacilityID/sFacilityID={sFacilityID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityOrganizationsByFacilityID(string sFacilityID,string sAdminUserID)
        {
            try
            {
                bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
                bool resultadminUserID = int.TryParse(sAdminUserID, out int nAdminUserID);
                FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
                IEnumerable<dynamic> organizations = await facilityOrganizationsRepository.GetOrganizationsList(nFacilityID);
                FacilitiesRepository facilityRepo = new FacilitiesRepository(_info);
                Facilities facility = await facilityRepo.Select(nFacilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                #region Logging

                if (facility.bFacilityLogging)
                {
                    //MROLogger logger = new MROLogger(_info);
                    //string sDescription = "Get Facility Locations Method was called for Facility ID: " + sFacilityID;
                    //logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Locations By Facility ID", "Manage Facilities");
                    AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                    string sDescription = "Get Organizations Method was called for Facility ID: " + nFacilityID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Manage Organizations",
                        sEventName = "Get Facility Organizations By Facility ID",
                        nFacilityID = facility.nFacilityID,
                        dtLogTime = DateTime.Now,
                    };
                    adminModuleLoggerRepository.Insert(adminModuleLogger);
                }
                #endregion
                return Ok(new { organizations, faciName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Add Facility Organization
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<FacilityOrganizations>> AddFacilityOrganization(AddOrganization addOrganization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //IEnumerable<FacilityLocations> dbLocations = await facilityLocationsRepository.SelectWhere("sLocationName", facilityLocation.sLocationName);
                    //if (dbLocations.Count() != 0)
                    //{
                    //    //Exit
                    //    return BadRequest("Cannot Add Location with Same Name");
                    //}

                    #region Data ! From UI
                    int? addedOrgID = null;
                    string sValidationTextGlobal = "";
                    FacilityOrganizations facilityOrganization = addOrganization.cOrganization;
                    facilityOrganization.sGUID = Guid.NewGuid().ToString().ToUpper();
                    facilityOrganization.dtCreated = DateTime.Now;
                    facilityOrganization.dtLastUpdate = DateTime.Now;
                    string removedSpecialChar = Regex.Replace(facilityOrganization.sOrgName, @"[^0-9a-zA-Z]+", "");
                    string finalString = Regex.Replace(removedSpecialChar, @"\s+", "");
                    finalString = "MRO" + finalString;
                    ////Generate MROLocationOther Normalized Keyword for If the String Contain other
                    //if (finalString.ToLower().Contains("other"))
                    //{
                    //    finalString = "MROLocationOther";
                    //    IEnumerable<FacilityLocations> dbOtherLocation = await facilityLocationsRepository.SelectLike("sNormalizedLocationName", "MROLocationOther");
                    //    if (dbLocations.Count() != 0)
                    //    {
                    //        //Exit
                    //        return BadRequest("Cannot Add more than one 'Other' Location in current Facility!");
                    //    }

                    //}
                    //Default value for uAuthExpirationMonths is 6
                    #endregion

                    #region Server Validation for Duplication check and 'Other' Organization check
                    string sStatusMessage = await ValidationForFacilityOrganization(facilityOrganization);
                    if (sStatusMessage != string.Empty)
                    {
                        return BadRequest(sStatusMessage);
                    }
                    #endregion

                    if (string.IsNullOrEmpty(facilityOrganization.sConfigLogoData)) {
                        facilityOrganization.sConfigLogoData = null;
                        facilityOrganization.sConfigLogoName = "";
                    }

                    if (string.IsNullOrEmpty(facilityOrganization.sConfigBackgroundData))
                    {
                        facilityOrganization.sConfigBackgroundData = null;
                        facilityOrganization.sConfigBackgroundName = "";
                    }

                    FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
                    addedOrgID = facilityOrganizationsRepository.Insert(facilityOrganization);

                    if (addedOrgID != null)
                    {
                        IEnumerable<FacilityOrganizations> addedFacilityOrganizationRepo = await facilityOrganizationsRepository.SelectWhere("nFacilityOrgID", addedOrgID);
                        FacilityOrganizations addedFacilityOrganization = addedFacilityOrganizationRepo.FirstOrDefault();

                        #region Logging
                        FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                        Facilities facility = await rpFac.Select(facilityOrganization.nFacilityID);
                        if (facility.bFacilityLogging)
                        {
                            MROLogger logger = new MROLogger(_info);
                            string sDescription = "Add Organization Method was called & Created Organization with ID: " + addedOrgID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = facilityOrganization.nCreatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Organization",
                                sEventName = "Add Organization",
                                nFacilityID = addedFacilityOrganization.nFacilityID
                            };
                            logger.InsertAuditSingle(addedFacilityOrganization, adminModuleLogger);
                        }
                        #endregion

                        #region Updating Locations with New Org ID
                        if (addOrganization.locationIds?.Count > 0)
                        {
                            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                            addedFacilityOrganization.nFacilityOrgID = await facilityLocationsRepository.UpdateLocationOrgID(addedFacilityOrganization.nFacilityOrgID, addOrganization.locationIds.ToArray());
                        }
                        #endregion
                    }
                    else
                        return Content("Error Adding organization!");

                    return Ok(sValidationTextGlobal);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
        [HttpPost("EditFacilityOrganization/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityOrganization(int id, FacilityOrganizations facilityOrganization)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    #region Server Validation for Duplication check
                    string removedSpecialChar = Regex.Replace(facilityOrganization.sOrgName, @"[^0-9a-zA-Z]+", "");
                    string sStatusMessage = await ValidationForFacilityOrganization(facilityOrganization);
                    if (sStatusMessage != string.Empty)
                    {
                        return BadRequest(sStatusMessage);
                    }
                    #endregion

                    if (id != facilityOrganization.nFacilityOrgID)
                    {
                        return BadRequest("Bad Request: ID Not Equals Organization ID");
                    }
                    string sValidationTextGlobal = "";

                    if (string.IsNullOrEmpty(facilityOrganization.sConfigLogoData))
                    {
                        facilityOrganization.sConfigLogoData = null;
                        facilityOrganization.sConfigLogoName = "";
                    }

                    if (string.IsNullOrEmpty(facilityOrganization.sConfigBackgroundData))
                    {
                        facilityOrganization.sConfigBackgroundData = null;
                        facilityOrganization.sConfigBackgroundName = "";
                    }

                    FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
                    facilityOrganization.dtLastUpdate = DateTime.Now;

                    IEnumerable<FacilityOrganizations> dbOldFacilityOrganization = await facilityOrganizationsRepository.SelectWhere("nFacilityOrgID", facilityOrganization.nFacilityOrgID);
                    FacilityOrganizations oldFacilityOrganization = dbOldFacilityOrganization.FirstOrDefault();

                    facilityOrganizationsRepository.Update(facilityOrganization);
                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Edit Organization Method was called for Facility Organization ID: " + facilityOrganization.nFacilityOrgID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = facilityOrganization.nUpdatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Facility Organization",
                        sEventName = "Edit Facility Organization",
                        nFacilityID = facilityOrganization.nFacilityID
                    };
                    logger.UpdateAuditSingle(oldFacilityOrganization, facilityOrganization, adminModuleLogger);
                    #endregion
                    return Ok(sValidationTextGlobal);

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
        [HttpPost("ToggleFacilityLocation")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ToggleFacilityLocation(ToggleLocation toggleLocation)
        {
            try
            {
                int nFacilityLocationID = toggleLocation.nFacilityLocationID;
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                FacilityLocations location = await facilityLocationsRepository.Select(nFacilityLocationID);
                if (nFacilityLocationID != location.nFacilityLocationID)
                {
                    return BadRequest();
                }

                IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", location.nFacilityID);
                locationList = locationList.Where(c => c.bLocationActiveStatus == false);
                if (await ValidateFacilityLocation(location, locationList, _info))
                {
                    if (await facilityLocationsRepository.ToggleSoftDelete("bLocationActiveStatus", nFacilityLocationID))
                    {
                        #region Logging
                        FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                        Facilities facility = await rpFac.Select(location.nFacilityID);
                        if (facility.bFacilityLogging)
                        {
                            AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                            string sDescription = "Toggle Location Method was called for Location ID: " + toggleLocation.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = location.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Location",
                                sEventName = "Toggle Facility Location",
                                sNewValue = "{Active Status : " + !location.bLocationActiveStatus + "}",
                                sOldValue = "{Active Status : " + location.bLocationActiveStatus + "}",
                                dtLogTime = DateTime.Now,
                                nFacilityID = location.nFacilityID,
                                nFacilityLocationID = location.nFacilityLocationID

                            };
                            adminModuleLoggerRepository.Insert(adminModuleLogger);
                        }
                        #endregion
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
                if (!await FacilityLocationExists(toggleLocation.nFacilityLocationID))
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

        #region Toggle Facility Location
        [HttpPost("ToggleFacilityLocationIncludeInFacility")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ToggleFacilityLocationIncludeInFacility(ToggleLocation toggleLocation)
        {
            try
            {
                int nFacilityLocationID = toggleLocation.nFacilityLocationID;
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                FacilityLocations location = await facilityLocationsRepository.Select(nFacilityLocationID);
                if (nFacilityLocationID != location.nFacilityLocationID)
                {
                    return BadRequest();
                }

                //IEnumerable<FacilityLocations> locationList = await facilityLocationsRepository.SelectWhere("nFacilityID", location.nFacilityID);
                //locationList = locationList.Where(c => c.bLocationActiveStatus == false);
                //if (await ValidateFacilityLocation(location, locationList, _info))
                //{
                    if (await facilityLocationsRepository.ToggleSoftDelete("bIncludeInFacilityLevel", nFacilityLocationID))
                    {
                        #region Logging
                        FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                        Facilities facility = await rpFac.Select(location.nFacilityID);
                        if (facility.bFacilityLogging)
                        {
                            AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                            string sDescription = "Toggle Include In Facility Level Method was called for Location ID: " + toggleLocation.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = location.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Location",
                                sEventName = "Toggle Include In Facility Level For Location",
                                sNewValue = "{Include In Facility Level : " + !location.bIncludeInFacilityLevel + "}",
                                sOldValue = "{Include In Facility Level : " + location.bIncludeInFacilityLevel + "}",
                                dtLogTime = DateTime.Now,
                                nFacilityID = location.nFacilityID,
                                nFacilityLocationID = location.nFacilityLocationID

                            };
                            adminModuleLoggerRepository.Insert(adminModuleLogger);
                        }
                        #endregion
                        return Ok("Success");
                    }
                    else
                    {
                        return NoContent();
                    }
                //}
                //else
                //{
                //    return Content("Provide Valid Authorization PDF");
                //}
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await FacilityLocationExists(toggleLocation.nFacilityLocationID))
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
                if (!string.IsNullOrEmpty(location.sAuthTemplate)) {
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
                return !string.IsNullOrEmpty(location.sAuthTemplate) && checkPDF;
            }
        }
        #endregion

        #region Get ROI Location ID
        //[HttpGet]
        //[Route("[action]/{id}")]
        //public int GetROILocationID(string id)
        //{
        //    FacilityLocationsRepository locationsRepository = new FacilityLocationsRepository(_info);
        //    bool reslt = Int32.TryParse(id, out int number);
        //    int nROILocationID = locationsRepository.GetROILocationID("nFacilityID", number);
        //    return nROILocationID;
        //}
        #endregion

        #region Get Location Button & GUID
        [HttpGet("GetBtnCodeAndGUID/{sFacilityOrgID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetBtnCodeAndGUID(string sFacilityOrgID)
        {
            try
            {
                bool resultFacilityOrgID = int.TryParse(sFacilityOrgID, out int nFacilityOrgID);
                FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
                FacilityOrganizations facilityOrganizations = await facilityOrganizationsRepository.Select(nFacilityOrgID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities dbFacility = await rpFac.Select(facilityOrganizations.nFacilityID);
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                IEnumerable<FacilityConnections> connections = await connectionRepo.SelectWhere("nFacilityID", dbFacility.nFacilityID);
                FacilityConnections connection = connections.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(facilityOrganizations.sGUID))
                {
                    MROHelperRepository helperRepo = new MROHelperRepository(_info);
                    MROHelper helper = await helperRepo.Select(1);
                    string patternFacGUID = @"\bMROFacilityGuid\b";
                    string replaceFacGUID = connection.sGUID;
                    string patternOrgGUID = @"\bMROFacilityOrgGuid\b";
                    string replaceOrgGUID = facilityOrganizations.sGUID;
                    
                    helper.sFacilityOrgURL = Regex.Replace(helper.sFacilityOrgURL, patternFacGUID, replaceFacGUID);
                    helper.sFacilityOrgURL = Regex.Replace(helper.sFacilityOrgURL, patternOrgGUID, replaceOrgGUID);

                    helper.sFacilityOrgButtonHTMLCode = Regex.Replace(helper.sFacilityOrgButtonHTMLCode, patternFacGUID, replaceFacGUID);
                    helper.sFacilityOrgButtonHTMLCode = Regex.Replace(helper.sFacilityOrgButtonHTMLCode, patternOrgGUID, replaceOrgGUID);
                    string patternFname = @"\bMROFacilityName\b";
                    string replaceFname = dbFacility.sFacilityName;
                    helper.sFacilityButtonHTMLCode = Regex.Replace(helper.sFacilityButtonHTMLCode, patternFname, replaceFname);

                    return Ok(new{helper.sFacilityOrgURL, helper.sFacilityOrgButtonHTMLCode });
                }
                else
                    return BadRequest();
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Server Validation for Duplicate Organization names
        private async Task<string> ValidationForFacilityOrganization(FacilityOrganizations facilityOrganization)
        {
            FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
            IEnumerable<FacilityOrganizations> dbOrganizations = await facilityOrganizationsRepository.SelectLocationByLocationName(facilityOrganization.nFacilityOrgID, facilityOrganization.sOrgName);
            if (dbOrganizations.Count() != 0)
            {
                return "Cannot Add Organization with Same Name";
            }

            return string.Empty;
        }

        private async Task<string> ValidationForFacilityLocation(FacilityLocations facilityLocation)
        {
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            IEnumerable<FacilityLocations> dbLocations = await facilityLocationsRepository.SelectLocationByLocationName(facilityLocation.nFacilityLocationID, facilityLocation.sLocationName);
            if (dbLocations.Count() != 0)
            {
                return "Cannot Add Location with Same Name";
            }


            //Generate MROLocationOther Normalized Keyword for If the String Contain other
            if (facilityLocation.sNormalizedLocationName.ToLower().Contains("other"))
            {
                facilityLocation.sNormalizedLocationName = "MROLocationOther";
                IEnumerable<dynamic> dbOtherLocation = await facilityLocationsRepository.GetLocationByNormalizedName(facilityLocation.nFacilityID, facilityLocation.nFacilityLocationID, facilityLocation.sNormalizedLocationName);
                if (dbOtherLocation.Count() != 0)
                {
                    return "Cannot Add more than one 'Other' Location in current Facility!";
                }

            }

            return string.Empty;
        }

        #endregion
    }
}
