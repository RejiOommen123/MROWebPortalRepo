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
                    recordType.dtCreated = DateTime.Now;
                    //spaces remove
                    string sNormalizedName = GetNormalizedName(recordType.sRecordTypeName);
                    recordType.sNormalizedRecordTypeName = await rtFac.GetNormalizedNameByMasterName(sNormalizedName);

                    #endregion

                   // RecordTypesRepository rpFac = new RecordTypesRepository(_info);

                    int GeneratedID = (int)rtFac.Insert(recordType);

                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + recordType.nCreatedAdminUserID + " called Add Record Type Method & Created Record Type with ID: " + GeneratedID;
                     logger.LogAdminRecords(recordType.nCreatedAdminUserID, sDescription, "Add Record Type", "Record Type");
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
                        string sDescription = "Admin with ID: " + recordType.nUpdatedAdminUserID + " called Edit Record Type Method for Record Type ID: " + recordType.nRecordTypeID;
                        logger.LogAdminRecords(recordType.nUpdatedAdminUserID, sDescription, "Edit Record Type", "Record Type");

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
        #region Delete RecordType      
        [HttpGet("DeleteRecordType/sRecordTypeID={sRecordTypeID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        //public async Task<ActionResult<RecordTypes>> EditRecordType(int id, RecordTypes recordType)
        public async Task<IActionResult> DeleteRecordType(string sRecordTypeID,string sAdminUserID)
        {
            RecordTypesRepository rtFac = new RecordTypesRepository(_info);
            bool resultRecordTypeID = int.TryParse(sRecordTypeID, out int nRecordTypeID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            try
                {
                    if (rtFac.DeleteOneToMany(nRecordTypeID, "lnkFacilityRecordTypes"))
                    {
                        #region Logging
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + sAdminUserID + " called Delete Record Type Method for Record Type ID: " + sRecordTypeID;
                        logger.LogAdminRecords(nAdminUserID, sDescription, "Delete Record Type", "Record Type");
                        #endregion
                        return Ok();
                    }
                    else
                    { return BadRequest("Error occur while delete record"); }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
