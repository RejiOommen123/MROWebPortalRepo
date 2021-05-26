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
                IEnumerable<dynamic> locations = await facilityLocationsRepository.GetLocData(nFacilityID);

                return Ok(new { orgData , locations});
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
        public async Task<IActionResult> DeleteGridData(ManageText deleteData)
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

    }
}
