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
    public class ManageTextController : ControllerBase
    {
        #region ManageText Constructor
        private readonly DBConnectionInfo _info;
        public ManageTextController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityNWizardData()
        {
            try
            {
                WizardRepository wizardRepository = new WizardRepository(_info);
                IEnumerable<Wizards> Wizard = await wizardRepository.GetAllASC(1000, "nWizardID");
                FacilitiesRepository facilitiesRepository = new FacilitiesRepository(_info);
                IEnumerable<dynamic> Facilities = await facilitiesRepository.GetFacilityData();
                return Ok(new { Wizard , Facilities});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GetManageTextData")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult>GetManageTextData(ManageTextFilterParam manageTextFilterParam)
        {
            try
            {
                OverrideTextRepository overrideTextRepository = new OverrideTextRepository(_info);
                IEnumerable<dynamic> gridData = await overrideTextRepository.GetManageTextData( manageTextFilterParam);
                return Ok(new { gridData });
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }             
        }


        [HttpGet("GetOrgNlocationData/sFacilityID={sFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetOrgNlocationData(string sFacilityID)
        {
            try
            {
                bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
                //bool resultadminUserID = int.TryParse(sAdminUserID, out int nAdminUserID);
                FacilityOrganizationsRepository facilityOrganizationsRepository = new FacilityOrganizationsRepository(_info);
                IEnumerable<dynamic> orgData = await facilityOrganizationsRepository.GetOrgData(nFacilityID);
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                IEnumerable<dynamic> locationData = await facilityLocationsRepository.GetLocData(nFacilityID);

                return Ok(new { orgData , locationData});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetLocationData/sFacilityID={sFacilityID}&sFacilityOrgID={sFacilityOrgID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetLocationData(string sFacilityID, string sFacilityOrgID)
        {
            try
            {
                bool resultFacilityID = int.TryParse(sFacilityID, out int nFacilityID);
                bool resultFacilityOrgID = int.TryParse(sFacilityOrgID, out int nFacilityOrgID);
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                IEnumerable<dynamic> locations = await facilityLocationsRepository.GetLocationData(nFacilityID, nFacilityOrgID);
                return Ok(new { locations });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditGridData(ManageText editGrid)
        {
            try
            {
                return Ok(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ResetToPrevious(CombineManageText combineManageText)
        {
            try  
            {
                OverrideTextRepository overrideTextRepository = new OverrideTextRepository(_info);
                int nSubID = 0,
                    nControlID = combineManageText.manageText.nControlID,
                    nWizardID = combineManageText.manageTextFilterParam.nWizardID;
                int? nCommonControlID = combineManageText.manageText.nCommonControlID;
                switch (combineManageText.manageText.sLevel)
                {
                    case "F":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityID;
                        break;
                    case "O":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityOrgID;
                        break;
                    case "L":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityLocationID;
                        break;
                }

                #region Get single old OverrideText from DB 
                OverrideText overrideText = new OverrideText()
                {
                    sPlace = combineManageText.manageText.sLevel,
                    nSubID = nSubID,
                    sTextType = combineManageText.manageText.sTextType,
                    nWizardID = combineManageText.manageText.nCommonControlID != null ? 500 : combineManageText.manageTextFilterParam.nWizardID,
                    nControlID = combineManageText.manageText.nCommonControlID != null ? combineManageText.manageText.nCommonControlID ?? default(int) : combineManageText.manageText.nControlID,
                    nLanguageID = combineManageText.manageTextFilterParam.nLanguageID,
                    nCommonControlID = combineManageText.manageText.nCommonControlID != null ? null : combineManageText.manageText.nCommonControlID,
                };
                OverrideText dbOldOverrideText = await overrideTextRepository.GetSingleOverrideText(overrideText);
                bool isOTDeleted = overrideTextRepository.DeleteSingleOverrideText(dbOldOverrideText);
                #endregion

                #region Set filter param to get single db manage text
                combineManageText.manageTextFilterParam.ID = combineManageText.manageText.nControlID;
                combineManageText.manageTextFilterParam.sTextType = combineManageText.manageText.sTextType;
                combineManageText.manageTextFilterParam.nCommonControlID = combineManageText.manageText.nCommonControlID;

                IEnumerable<ManageText> manageTexts = await overrideTextRepository.GetManageTextData(combineManageText.manageTextFilterParam);
                ManageText manageText = manageTexts.FirstOrDefault();
                #endregion

                #region Get single new OverrideText from DB 
                overrideText.sPlace = manageText.sLevel;
                if (manageText.sLevel == "O" && combineManageText.manageTextFilterParam.nFacilityOrgID == 0)
                {
                    FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                    FacilityLocations facilityLocation = await facilityLocationsRepository.Select(combineManageText.manageTextFilterParam.nFacilityLocationID);
                    combineManageText.manageTextFilterParam.nFacilityOrgID = facilityLocation.nFacilityOrgID ?? default(int);
                }
                switch (manageText.sLevel)
                {
                    case "G":
                        nSubID = 0;
                        break;
                    case "F":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityID;
                        break;
                    case "O":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityOrgID;
                        break;
                    case "L":
                        nSubID = combineManageText.manageTextFilterParam.nFacilityLocationID;
                        break;
                }                
                overrideText.nSubID = nSubID;
                overrideText.sPlace = manageText.sLevel;
                OverrideText dbNewOverrideText = await overrideTextRepository.GetSingleOverrideText(overrideText);
                #endregion

                #region Reset dbOldOverrideText & dbNewOverrideText
                dbOldOverrideText.nControlID = nControlID;
                dbOldOverrideText.nWizardID = nWizardID;
                dbOldOverrideText.nCommonControlID = nCommonControlID;

                dbNewOverrideText.nControlID = nControlID;
                dbNewOverrideText.nWizardID = nWizardID;
                dbNewOverrideText.nCommonControlID = nCommonControlID;
                #endregion

                #region Logging
                FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                Facilities facility = await facRepo.Select(combineManageText.manageTextFilterParam.nFacilityID);
                MROLogger logger = new MROLogger(_info);
                AdminModuleLogger adminModuleLogger = null;
                if (facility.bFacilityLogging)
                {
                    string sDescription = $"Reset to previous level TextType: {dbOldOverrideText.sTextType}, ControlID: {dbOldOverrideText.nControlID}, WizardID: {dbOldOverrideText.nWizardID} ";
                    adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = dbOldOverrideText.nUpdatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Manage Text",
                        sEventName = "Reset to previous level",
                        nFacilityID = combineManageText.manageTextFilterParam.nFacilityID
                    };
                    logger.UpdateAuditSingle(dbOldOverrideText, dbNewOverrideText, adminModuleLogger);
                }
                #endregion

                return Ok(new { manageText });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
