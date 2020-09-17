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
    [APIKeyAuth]
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
        [Route("[action]/sFacilitylocationID={sFacilitylocationID}&sAdminUserID={sAdminUserID}")]
        public async Task<FacilityLocations> GetFacilityLocationSingle(string sFacilitylocationID,string sAdminUserID)
        {
            FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
            bool resultFacilityLocation = int.TryParse(sFacilitylocationID, out int nFacilitylocationID);
            bool resultAdminUserID = int.TryParse(sAdminUserID, out int nAdminUserID);

            FacilityLocations location =  await facilityLocationsRepository.Select(nFacilitylocationID);

            #region Logging
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities facility = await rpFac.Select(location.nFacilityID);
            if (facility.bFacilityLogging) {
                MROLogger logger = new MROLogger(_info);
                string sDescription = "Admin with ID: " + sAdminUserID + " called Get Location Method for Location ID: " + sFacilitylocationID;
                logger.LogAdminRecords(nAdminUserID, sDescription, "Get Location By ID", "Manage Locations");
            }
            #endregion
            return location;
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

        [HttpGet("GetFacilityLocationByFacilityID/sFacilityID={sFacilityID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityLocationByFacilityID(string sFacilityID,string sAdminUserID)
        {
            try
            {
                bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
                bool resultadminUserID = int.TryParse(sAdminUserID, out int nAdminUserID);
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                IEnumerable<dynamic> locations = await facilityLocationsRepository.GetLocationsList(nFacilityID);
                FacilitiesRepository facilityRepo = new FacilitiesRepository(_info);
                Facilities facility = await facilityRepo.Select(nFacilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                #region Logging

                if (facility.bFacilityLogging)
                {
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + sAdminUserID + " called Get Facility locations Method for Facility ID: " + sFacilityID;
                    logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Locations By Facility ID", "Manage Facilities");
                }
                #endregion
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
                    //IEnumerable<FacilityLocations> dbLocations = await facilityLocationsRepository.SelectWhere("sLocationName", facilityLocation.sLocationName);
                    //if (dbLocations.Count() != 0)
                    //{
                    //    //Exit
                    //    return BadRequest("Cannot Add Location with Same Name");
                    //}

                    #region Data ! From UI
                    int? addedLocationID = null;
                    bool checkPDF = false;
                    string sValidationTextGlobal = "";
                    facilityLocation.bLocationActiveStatus = false;
                    facilityLocation.dtCreated = DateTime.Now;
                    facilityLocation.dtLastUpdate = DateTime.Now;
                    string removedSpecialChar = Regex.Replace(facilityLocation.sLocationName, @"[^0-9a-zA-Z]+", "");
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
                    facilityLocation.sNormalizedLocationName = finalString;
                    //Default value for uAuthExpirationMonths is 6
                    facilityLocation.nAuthExpirationMonths = 6;
                    #endregion

                    #region Server Validation for Duplication check and 'Other' Location check
                    string sStatusMessage = await ValidationForFacilityLocation(facilityLocation);
                    if (sStatusMessage != string.Empty)
                    {
                        return BadRequest(sStatusMessage);
                    }
                    #endregion

                    if (string.IsNullOrEmpty(facilityLocation.sConfigLogoData)) {
                        facilityLocation.sConfigLogoData = null;
                        facilityLocation.sConfigLogoName = "";
                    }

                    if (string.IsNullOrEmpty(facilityLocation.sConfigBackgroundData))
                    {
                        facilityLocation.sConfigBackgroundData = null;
                        facilityLocation.sConfigBackgroundName = "";
                    }

                    //Validating Authorization Template
                    if (!string.IsNullOrEmpty(facilityLocation.sAuthTemplate))
                    {
                        facilityLocation.sAuthTemplate = facilityLocation.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                        byte[] pdfByteArray = Convert.FromBase64String(facilityLocation.sAuthTemplate);
                        ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                        IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                        LocationAuthorizationDocumentController locationAuthorizationDocumentCntrl = new LocationAuthorizationDocumentController();
                        checkPDF = locationAuthorizationDocumentCntrl.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);
                    }

                    FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);

                    if (checkPDF)
                    {
                        facilityLocation.bLocationActiveStatus = true;
                        addedLocationID = facilityLocationsRepository.Insert(facilityLocation);
                    }
                    else
                    {
                        facilityLocation.sAuthTemplateName = null;
                        facilityLocation.sAuthTemplate = null;
                        //Make Active Status False, then insert record & then call SP
                        //In SP call,as location active status is false, hence facility won't be activated
                        facilityLocation.bLocationActiveStatus = false;
                        addedLocationID = facilityLocationsRepository.Insert(facilityLocation);
                    }

                    if (addedLocationID != null)
                    {
                        IEnumerable<FacilityLocations> addedFacilityLocationRepo = await facilityLocationsRepository.SelectWhere("nFacilityLocationID", addedLocationID);
                        FacilityLocations addedFacilityLocation = addedFacilityLocationRepo.FirstOrDefault();
                        //Call spAddDepedencyRecordsforFacilityLocation & Log
                        facilityLocationsRepository.AddDependencyRecordsForFacilityLocation((int)addedLocationID, facilityLocation.nFacilityID);

                        #region Logging
                        FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                        Facilities facility = await rpFac.Select(facilityLocation.nFacilityID);
                        if (facility.bFacilityLogging)
                        {
                            MROLogger logger = new MROLogger(_info);
                            string sDescription = "Admin with ID: " + facilityLocation.nCreatedAdminUserID + " called Add Location Method & Created Location with ID: " + addedLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = facilityLocation.nCreatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Location",
                                sEventName = "Add Location"
                            };
                            logger.InsertAuditSingle(addedFacilityLocation, adminModuleLogger);
                        }
                        #endregion
                    }
                    else
                        return Content("Error Adding location!");

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
        [HttpPost("EditFacilityLocation/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityLocation(int id, FacilityLocations facilityLocation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    #region Server Validation for Duplication check and 'Other' Location check
                    string removedSpecialChar = Regex.Replace(facilityLocation.sLocationName, @"[^0-9a-zA-Z]+", "");
                    string finalString = Regex.Replace(removedSpecialChar, @"\s+", "");
                    facilityLocation.sNormalizedLocationName = "MRO" + finalString;
                    string sStatusMessage = await ValidationForFacilityLocation(facilityLocation);
                    if (sStatusMessage != string.Empty)
                    {
                        return BadRequest(sStatusMessage);
                    }
                    #endregion

                    if (id != facilityLocation.nFacilityLocationID)
                    {
                        return BadRequest("Bad Request: ID Not Equals Location ID");
                    }
                    bool checkPDF = false;
                    string sValidationTextGlobal = "";

                    if (string.IsNullOrEmpty(facilityLocation.sConfigLogoData))
                    {
                        facilityLocation.sConfigLogoData = null;
                        facilityLocation.sConfigLogoName = "";
                    }

                    if (string.IsNullOrEmpty(facilityLocation.sConfigBackgroundData))
                    {
                        facilityLocation.sConfigBackgroundData = null;
                        facilityLocation.sConfigBackgroundName = "";
                    }

                    FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                    if (!string.IsNullOrEmpty(facilityLocation.sAuthTemplate))
                    {
                        facilityLocation.sAuthTemplate = facilityLocation.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                        byte[] pdfByteArray = Convert.FromBase64String(facilityLocation.sAuthTemplate);
                        ValidateAuthorizationDocRepository validateDoc = new ValidateAuthorizationDocRepository(_info);
                        IEnumerable<ValidateAuthorizationDoc> validationRules = await validateDoc.GetAllASC(1000, "nFieldID");
                        LocationAuthorizationDocumentController locationAuthorizationDocumentCntrl = new LocationAuthorizationDocumentController();
                        checkPDF = locationAuthorizationDocumentCntrl.ValidateAuthorizationDocument(pdfByteArray, validationRules, out sValidationTextGlobal);
                    }
                    facilityLocation.dtLastUpdate = DateTime.Now;

                    IEnumerable<FacilityLocations> dbOldFacilityLocation = await facilityLocationsRepository.SelectWhere("nFacilityLocationID", facilityLocation.nFacilityLocationID);
                    FacilityLocations oldFacilityLocation = dbOldFacilityLocation.FirstOrDefault();
                    if (checkPDF)
                    {
                        facilityLocation.bLocationActiveStatus = true;
                        facilityLocationsRepository.Update(facilityLocation);

                        #region Logging
                        FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                        Facilities facility = await rpFac.Select(facilityLocation.nFacilityID);
                        if (facility.bFacilityLogging)
                        {
                            MROLogger logger = new MROLogger(_info);
                            string sDescription = "Admin with ID: " + facilityLocation.nUpdatedAdminUserID + " called Edit Location Method for Facility Location ID: " + facilityLocation.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = facilityLocation.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Location",
                                sEventName = "Edit Facility Location"
                            };
                            logger.UpdateAuditSingle(oldFacilityLocation, facilityLocation, adminModuleLogger);
                        }
                        #endregion
                        return Ok(sValidationTextGlobal);
                    }
                    else {
                        facilityLocation.bLocationActiveStatus = false;
                        facilityLocation.sAuthTemplate = null;
                        facilityLocation.sAuthTemplateName = "";
                        facilityLocationsRepository.Update(facilityLocation);
                        #region Logging
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + facilityLocation.nUpdatedAdminUserID + " called Edit Location Method for Facility Location ID: " + facilityLocation.nFacilityLocationID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = facilityLocation.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Facility Location",
                            sEventName = "Edit Facility Location"
                        };
                        logger.UpdateAuditSingle(oldFacilityLocation, facilityLocation, adminModuleLogger);
                        #endregion
                        return Ok(sValidationTextGlobal);
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
                            string sDescription = "Admin with ID: " + toggleLocation.nAdminUserID + " called Toggle Location Method for Location ID: " + toggleLocation.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = location.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Facility Location",
                                sEventName = "Toggle Facility Location",
                                sNewValue = "{bLocationActiveStatus : " + location.bLocationActiveStatus + "}",
                                sOldValue = "{bLocationActiveStatus : " + !location.bLocationActiveStatus + "}",
                                dtLogTime = DateTime.Now
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

        #region Server Validation for Duplicate Location names and check for only one 'Other' Location
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
