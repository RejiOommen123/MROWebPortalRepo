using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
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
        [HttpGet("GetDisclaimersByFacilityID/nFacilityID={nFacilityID}&nFacilityLocationID={nFacilityLocationID}&nAdminUserID={nAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetDisclaimersByFacilityID(int nFacilityID, int nFacilityLocationID, int nAdminUserID)
        {
            try
            {
                FacilityDisclaimersRepository disclaimersRepository = new FacilityDisclaimersRepository(_info);
                IEnumerable<dynamic> disclaimers = await disclaimersRepository.EditDisclaimers(nFacilityID, nFacilityLocationID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(nFacilityID);
                FacilityLocations facilityLocation = null;
                if (nFacilityLocationID != 0)
                {
                    FacilityLocationsRepository flFac = new FacilityLocationsRepository(_info);
                    facilityLocation = await flFac.Select(nFacilityLocationID);
                }
                if (facility == null)
                    return NotFound();
                var titleName = nFacilityLocationID == 0 ? facility.sFacilityName : facilityLocation.sLocationName;
                #region Logging
                if (facility.bFacilityLogging)
                {
                    //MROLogger logger = new MROLogger(_info);
                    //string sDescription = "Get Facility Disclaimers Method was called for Facility ID: " + nFacilityID;
                    //logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Disclaimers By ID", "Manage Facilities");
                    AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                    string sDescription = "Get Facility Disclaimers Method was called for Facility ID: " + nFacilityID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Manage Facilities",
                        sEventName = "Get Facility Disclaimers By ID",
                        nFacilityID = facility.nFacilityID,
                        dtLogTime = DateTime.Now,
                    };
                    adminModuleLoggerRepository.Insert(adminModuleLogger);
                }
                #endregion
                return Ok(new { disclaimers, titleName });
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion

        #region Edit Facility Disclaimers
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityDisclaimers(EditDisclaimers editDisclaimers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<FacilityDisclaimers> InsertDisclaimers = null, UpdateDisclaimers = null;
                    FacilityDisclaimersRepository fdFac = new FacilityDisclaimersRepository(_info);
                    List<FacilityDisclaimers> facilityDisclaimersList = new List<FacilityDisclaimers>();
                    List<int> idList = new List<int>();
                    var singleRecord = editDisclaimers.facilityDisclaimers.FirstOrDefault();
                    int nAdminUserID = singleRecord.nUpdatedAdminUserID;
                    int nFacilityID = singleRecord.nFacilityID;
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(nFacilityID);


                    if (editDisclaimers.nFacilityLocationID != 0)
                    {
                        InsertDisclaimers = editDisclaimers.facilityDisclaimers.Where(map => map.nFacilityLocationID == 0);
                        UpdateDisclaimers = editDisclaimers.facilityDisclaimers.Where(map => map.nFacilityLocationID != 0);
                    }
                    else
                    {
                        UpdateDisclaimers = editDisclaimers.facilityDisclaimers;
                    }

                    if (editDisclaimers.nFacilityLocationID != 0)
                    {
                        if (InsertDisclaimers.Count() != 0)
                        {
                            foreach (var disclaimer in InsertDisclaimers)
                            {
                                facilityDisclaimersList.Add(
                                    new FacilityDisclaimers()
                                    {
                                        nFacilityID = disclaimer.nFacilityID,
                                        sWizardHelperType = disclaimer.sWizardHelperType,
                                        sWizardHelperValue = disclaimer.sWizardHelperValue,
                                        nWizardID = disclaimer.nWizardID,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = disclaimer.nUpdatedAdminUserID,
                                        dtCreated = disclaimer.dtCreated,
                                        nCreatedAdminUserID = disclaimer.nCreatedAdminUserID,
                                        nFacilityLocationID = editDisclaimers.nFacilityLocationID
                                    });
                            }
                            fdFac.InsertMany(facilityDisclaimersList);

                            #region Logging

                            if (facility.bFacilityLogging)
                            {
                                MROLogger logger = new MROLogger(_info);
                                string sDescription = "Edit Disclaimers Location ID: " + editDisclaimers.nFacilityLocationID;
                                AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = sDescription,
                                    sModuleName = "Edit Facility Fields/Disclaimers",
                                    sEventName = "Edit Facility Disclaimers",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(facilityDisclaimersList, adminModuleLogger);
                            }
                            #endregion
                        }
                    }

                    if (UpdateDisclaimers.Count() != 0)
                    {
                        foreach (var disclaimer in UpdateDisclaimers)
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
                                    nCreatedAdminUserID = disclaimer.nCreatedAdminUserID,
                                    nFacilityLocationID = editDisclaimers.nFacilityLocationID
                                });
                            idList.Add(disclaimer.nFacilityWizardHelperID);
                        }
                        int[] idArray = idList.ToArray();
                        List<FacilityDisclaimers> oldFacilityDisclaimers = new List<FacilityDisclaimers>();
                        var elist = await fdFac.SelectListByInClause(idArray, "lnkFacilityWizardHelper", "nFacilityWizardHelperID", singleRecord.nFacilityID, editDisclaimers.nFacilityLocationID);

                        foreach (var item in elist)
                        {
                            oldFacilityDisclaimers.Add(item);
                        }

                        fdFac.UpdateMany(facilityDisclaimersList);

                        #region Logging
                        if (facility.bFacilityLogging)
                        {
                            MROLogger logger = new MROLogger(_info);
                            string sDescription = "Edit Disclaimers Location ID: " + editDisclaimers.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Edit Facility Fields/Disclaimers",
                                sEventName = "Edit Facility Disclaimers",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldFacilityDisclaimers, facilityDisclaimersList, adminModuleLogger, "nFacilityWizardHelperID");
                        }
                        #endregion
                    }
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

        #region Reset To Default Facility Disclaimers
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ResetToDefault(FacilityDisclaimers facilityDisclaimer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(facilityDisclaimer.nFacilityID);
                    FacilityDisclaimersRepository fdFac = new FacilityDisclaimersRepository(_info);
                    FacilityDisclaimers dbFacilityDisclaimer = await fdFac.SelectFourWhereClause("sWizardHelperType", facilityDisclaimer.sWizardHelperType, "nWizardID", facilityDisclaimer.nWizardID,"nFacilityId",facilityDisclaimer.nFacilityID,"nFacilityLocationId",0);

                    bool deleteResult = fdFac.Delete(facilityDisclaimer.nFacilityWizardHelperID);
                    if (deleteResult)
                    {
                        FacilityDisclaimers returnFacilityDisclaimers = new FacilityDisclaimers()
                        {
                            nFacilityWizardHelperID = dbFacilityDisclaimer.nFacilityWizardHelperID,
                            nFacilityID = dbFacilityDisclaimer.nFacilityID,
                            nFacilityLocationID = dbFacilityDisclaimer.nFacilityLocationID,
                            sWizardHelperType = dbFacilityDisclaimer.sWizardHelperType,
                            sWizardHelperValue = dbFacilityDisclaimer.sWizardHelperValue,
                            nWizardID = dbFacilityDisclaimer.nWizardID,
                            nCreatedAdminUserID = dbFacilityDisclaimer.nCreatedAdminUserID,
                            dtCreated = dbFacilityDisclaimer.dtCreated,
                            nUpdatedAdminUserID = dbFacilityDisclaimer.nUpdatedAdminUserID,
                            dtLastUpdate = dbFacilityDisclaimer.dtLastUpdate
                        };
                        #region Logging
                        if (facility.bFacilityLogging)
                        {
                            MROLogger logger = new MROLogger(_info);
                            string sDescription = "Reset to Default Disclaimer Location ID: " + facilityDisclaimer.nFacilityLocationID;
                            AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = facilityDisclaimer.nUpdatedAdminUserID,
                                sDescription = sDescription,
                                sModuleName = "Edit Facility Fields/Disclaimers",
                                sEventName = "Reset to Default Disclaimer",
                                nFacilityID = facilityDisclaimer.nFacilityID
                            };
                            logger.UpdateAuditSingle(facilityDisclaimer,dbFacilityDisclaimer,adminModuleLogger);
                        }
                        #endregion
                        return Ok(dbFacilityDisclaimer);
                    }
                    else {
                        return BadRequest("Record not deleted");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
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
