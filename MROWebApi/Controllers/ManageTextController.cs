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

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult>getGridData()
        {
            try
            {
                List<ManageTextDataTable> gridData = new List<ManageTextDataTable>();

                //ManageTextDataTable list = new ManageTextDataTable();

                gridData.Add(new ManageTextDataTable());
                gridData[0].GFOL = "G";
                gridData[0].Text = "Text";
                gridData[0].Type = "Type";
                gridData[0].Extra = "Extra";
                gridData[0].Button = true;
                gridData[0].GroupBy = "Static";

                gridData.Add(new ManageTextDataTable());
                gridData[1].GFOL = "F";
                gridData[1].Text = "Text1";
                gridData[1].Type = "Type1";
                gridData[1].Extra = "Extra1";
                gridData[1].Button = true;
                gridData[1].GroupBy = "Dynamic";

                gridData.Add(new ManageTextDataTable());
                gridData[2].GFOL = "O";
                gridData[2].Text = "Text2";
                gridData[2].Type = "Type2";
                gridData[2].Extra = "Extra2";
                gridData[2].Button = true;
                gridData[2].GroupBy = "Static";

                gridData.Add(new ManageTextDataTable());
                gridData[3].GFOL = "G";
                gridData[3].Text = "Text3";
                gridData[3].Type = "Type3";
                gridData[3].Extra = "Extra3";
                gridData[3].Button = true;
                gridData[3].GroupBy = "Dynamic";


                gridData.Add(new ManageTextDataTable());
                gridData[4].GFOL = "G";
                gridData[4].Text = "Text4";
                gridData[4].Type = "Type4";
                gridData[4].Extra = "Extra4";
                gridData[4].Button = true;
                gridData[4].GroupBy = "Common";

                gridData.Add(new ManageTextDataTable());
                gridData[5].GFOL = "G";
                gridData[5].Text = "Text5";
                gridData[5].Type = "Type5";
                gridData[5].Extra = "Extra5";
                gridData[5].Button = true;
                gridData[5].GroupBy = "Common";

                return Ok(gridData);
                    
            }
            catch (Exception ex)
            {
                throw ex;
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
        public async Task<IActionResult> EditGridData(ManageTextDataTable editGrid)
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
        public async Task<IActionResult> DeleteGridData(ManageTextDataTable deleteData)
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
