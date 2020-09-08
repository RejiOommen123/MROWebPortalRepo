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
                FacilityDisclaimersRepository disclaimersRepository = new FacilityDisclaimersRepository(_info);
                IEnumerable<dynamic> disclaimers = await disclaimersRepository.EditDisclaimers(nFacilityID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(nFacilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                #region Logging
                if (facility.bFacilityLogging)
                {
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + nAdminUserID + " called Get Facility Disclaimers Method for Facility ID: " + nFacilityID;
                    logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Disclaimers By ID", "Manage Facilities");
                }
                #endregion
                return Ok(new { disclaimers, faciName });
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion
        #region Edit Facility Field Maps 
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityDisclaimers([FromBody] FacilityDisclaimers[] facilityDisclaimers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FacilityDisclaimersRepository fdFac = new FacilityDisclaimersRepository(_info);
                    List<FacilityDisclaimers> facilityDisclaimersList = new List<FacilityDisclaimers>();
                    foreach (var disclaimer in facilityDisclaimers)
                    {
                        facilityDisclaimersList.Add(
                            new FacilityDisclaimers()
                            {
                                nFacilityWizardHelperID = disclaimer.nFacilityWizardHelperID,
                                nFacilityID = disclaimer.nFacilityID,
                                sWizardHelperType = disclaimer.sWizardHelperType,
                                sWizardHelperValue = disclaimer.sWizardHelperValue,
                                nWizardID = disclaimer.nWizardID,
                                dtLastUpdate = DateTime.Now,
                                nUpdatedAdminUserID = disclaimer.nUpdatedAdminUserID,
                                dtCreated = disclaimer.dtCreated,
                                nCreatedAdminUserID = disclaimer.nCreatedAdminUserID
                            });

                    }
                    fdFac.UpdateMany(facilityDisclaimersList);
                    
                    #region Logging
                    var mapTable = facilityDisclaimers.FirstOrDefault();
                    int nAdminUserID = mapTable.nUpdatedAdminUserID;
                    int nFacilityID = mapTable.nFacilityID;
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(nFacilityID);
                    if (facility.bFacilityLogging)
                    {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + nAdminUserID + " called Edit Disclaimers Method for Facility ID: " + nFacilityID + " and Updated Disclaimer";
                        logger.LogAdminRecords(nAdminUserID, sDescription, "Edit Disclaimers", "Edit Disclaimers");
                    }
                    #endregion

                    return Ok("Success");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion
    }
}
