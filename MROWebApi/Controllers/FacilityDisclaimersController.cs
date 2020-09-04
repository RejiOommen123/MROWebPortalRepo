﻿using MailKit.Net.Smtp;
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
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [APIKeyAuth]
    public class FacilityDisclaimersController : ControllerBase
    {
        #region Facility Disclaimers Constructor
        private readonly DBConnectionInfo _info;
        public FacilityDisclaimersController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion


        #region Get Facility Disclaimers
        [HttpGet("GetDisclaimersByFacilityID/nFacilityID={nFacilityID}&nAdminUserID={nAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetDisclaimersByFacilityID(int nFacilityID, int nAdminUserID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                IEnumerable<dynamic> fields = await fieldsRepository.EditFields(nFacilityID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(nFacilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                #region Logging
                if (facility.bFacilityLogging)
                {
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + nAdminUserID + " called Get Facility Fields Method for Facility ID: " + nFacilityID;
                    logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Fields By ID", "Manage Facilities");
                }
                #endregion
                return Ok(new { fields, faciName });
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
#endregion
    }
}
