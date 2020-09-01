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
    public class MasterController : ControllerBase
    {
        #region Master Constructor
        private readonly DBConnectionInfo _info;
        public MasterController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion


        #region Get RecordType / RecordTypes
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetRecordTypes()
        {
            try
            {
                RecordTypesRepository rtFac = new RecordTypesRepository(_info);
                IEnumerable<RecordTypes> recordTypes = await rtFac.GetAllASC(1000, "sRecordTypeName");
                return Ok(recordTypes);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("GetRecordType/sRecordTypeID={sRecordTypeID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<RecordTypes>> GetRecordType(string sRecordTypeID, string sAdminUserID)
        {
            RecordTypesRepository rtFac = new RecordTypesRepository(_info);
            bool resultRecordTypeID = int.TryParse(sRecordTypeID, out int nRecordTypeID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            RecordTypes recordType = await rtFac.Select(nRecordTypeID);

            #region Logging
            
            MROLogger logger = new MROLogger(_info);
            string sDescription = "Admin with ID: " + sAdminUserID + " called Get Record Type Method for Record Type ID: " + sRecordTypeID;
            logger.LogAdminRecords(nAdminUserID, sDescription, "Get Record Type By ID", "Master Entry - Record Type");
           
            #endregion

            if (recordType == null)
                return NotFound();
            return recordType;
        }
        #endregion

        #region Add Record Type
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> AddRecordType(RecordTypes recordType)
        {
            if (ModelState.IsValid)
            {
                //Check if there's a record type with same name 
                RecordTypesRepository rtFac = new RecordTypesRepository(_info);
                IEnumerable<RecordTypes> dbRecordTypes = await rtFac.SelectWhere("sRecordTypeName", recordType.sRecordTypeName);
                if (dbRecordTypes.Count() != 0)
                {
                    //Exit
                    return BadRequest("Cannot Add Record Type \"" + recordType.sRecordTypeName + "\", Record Type with Same Name Exists");
                }
                try
                {
                    #region Data Addition ! from UI
                    recordType.nWizardID = 10;
                    recordType.dtLastUpdate = DateTime.Now;
                    //spaces remove
                    string sNormalizedName = GetNormalizedName(recordType.sRecordTypeName);
                    recordType.sNormalizedRecordTypeName = await rtFac.GetNormalizedNameByMasterName(sNormalizedName);

                    #endregion

                   // RecordTypesRepository rpFac = new RecordTypesRepository(_info);

                    int GeneratedID = (int)rtFac.Insert(recordType);

                    #region Logging
                        MROLogger logger = new MROLogger(_info);
                    //string sDescription = "Admin with ID: " + recordType. + " called Add Facility Method & Created Facility with ID: " + GeneratedID;
                       // logger.LogAdminRecords(facility.nCreatedAdminUserID, sDescription, "Add Facility", "Add Facility");
                    #endregion

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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

        #region Edit RecordType
        //[HttpPost("EditRecordType/{id}")]
        [HttpPost("EditRecordType")]
        [AllowAnonymous]
        [Route("[action]")]
        //public async Task<ActionResult<RecordTypes>> EditRecordType(int id, RecordTypes recordType)
        public async Task<IActionResult> EditRecordType(RecordTypes recordType)
        {
            if (ModelState.IsValid)
            {
                //if (id != recordType.nRecordTypeID)
                //{
                //    return BadRequest("Bad Request: ID Not Equals Facility ID");
                //}
                //else
                //{
                    //Check if there's a facility with same name 
                    RecordTypesRepository rtFac = new RecordTypesRepository(_info);
                    IEnumerable<RecordTypes> dbRecordTypes = await rtFac.SelectWhere("sRecordTypeName", recordType.sRecordTypeName);
                    if (dbRecordTypes.Count() != 0)
                    {
                        if (dbRecordTypes.First().nRecordTypeID != recordType.nRecordTypeID)
                        {
                            //Exit
                            return BadRequest("Cannot Edit Record Type \"" + recordType.sRecordTypeName + "\", Record Type with Same Name Exists");
                        }
                    }
                //}
                //FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                try
                {
                    recordType.dtLastUpdate = DateTime.Now;
                    if (rtFac.Update(recordType))
                    {
                        #region Logging

                        MROLogger logger = new MROLogger(_info);
                        //string sDescription = "Admin with ID: " + facility.nUpdatedAdminUserID + " called Edit Facility Method for Facility ID: " + facility.nFacilityID;
                        //logger.LogAdminRecords(facility.nUpdatedAdminUserID, sDescription, "Edit Facility", "Edit Facility");

                        #endregion
                        return Ok();
                    }
                    else
                    { return NotFound(); }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
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

        private string GetNormalizedName(string normalizedString)
        {
            string removedSpecialChar = Regex.Replace(normalizedString, @"[^0-9a-zA-Z]+", "");
            string finalString = Regex.Replace(removedSpecialChar, @"\s+", "");
            finalString = "MRO" + finalString;
            return finalString;
        }
    }
}
