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

        #region Master - Record Type - Methods

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

                    int GeneratedID = (int)rtFac.Insert(recordType);

                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + recordType.nCreatedAdminUserID + " called Add Record Type Method & Created Record Type with ID: " + GeneratedID;
                    recordType.nRecordTypeID = GeneratedID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = recordType.nCreatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Record Type",
                        sEventName = "Add Record Type"
                    };
                    logger.InsertAuditSingle(recordType, adminModuleLogger);
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
        [HttpPost("EditRecordType")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditRecordType(RecordTypes recordType)
        {
            if (ModelState.IsValid)
            {
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
                try
                {
                    recordType.dtLastUpdate = DateTime.Now;
                    RecordTypes oldRecordType = await rtFac.Select(recordType.nRecordTypeID);
                    if (rtFac.Update(recordType))
                    {
                        #region Logging

                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + recordType.nUpdatedAdminUserID + " called Edit Record Type Method for Record Type ID: " + recordType.nRecordTypeID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = recordType.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Master Entry - Record Type",
                            sEventName = "Edit Record Type"
                        };
                        logger.UpdateAuditSingle(oldRecordType, recordType, adminModuleLogger);

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
        public async Task<IActionResult> DeleteRecordType(string sRecordTypeID,string sAdminUserID)
        {
            RecordTypesRepository rtFac = new RecordTypesRepository(_info);
            FacilityRecordTypesRepository rtFacilityFac = new FacilityRecordTypesRepository(_info);
            bool resultRecordTypeID = int.TryParse(sRecordTypeID, out int nRecordTypeID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            try
                {
                RecordTypes masterRecordType = await rtFac.Select(nRecordTypeID);
                IEnumerable<FacilityRecordTypes> facilityRecordTypeIEnum = await rtFacilityFac.SelectWhere("nRecordTypeID", nRecordTypeID);
                List<FacilityRecordTypes> facilityRecordTypeList = facilityRecordTypeIEnum.ToList();
                if (rtFac.DeleteOneToMany(nRecordTypeID, "lnkFacilityRecordTypes"))
                    {
                        #region Logging
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + sAdminUserID + " called Delete Record Type Method for Record Type ID: " + sRecordTypeID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = nAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Master Entry - Record Type",
                            sEventName = "Delete Record Type"
                        };
                        logger.DeleteAuditMany(facilityRecordTypeList, adminModuleLogger);
                        logger.DeleteAuditSingle(masterRecordType, adminModuleLogger);
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

        #endregion

        #region Master - Primary Reason - Methods

        #region Get PrimaryReason / PrimaryReasons
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetPrimaryReasons()
        {
            try
            {
                PrimaryReasonsRepository rtFac = new PrimaryReasonsRepository(_info);
                IEnumerable<PrimaryReasons> primaryReasons = await rtFac.GetAllASC(1000, "sPrimaryReasonName");
                return Ok(primaryReasons);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("GetPrimaryReason/sPrimaryReasonID={sPrimaryReasonID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<PrimaryReasons>> GetPrimaryReason(string sPrimaryReasonID, string sAdminUserID)
        {
            PrimaryReasonsRepository rtFac = new PrimaryReasonsRepository(_info);
            bool resultPrimaryReasonID = int.TryParse(sPrimaryReasonID, out int nPrimaryReasonID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            PrimaryReasons primaryReason = await rtFac.Select(nPrimaryReasonID);

            #region Logging

            MROLogger logger = new MROLogger(_info);
            string sDescription = "Admin with ID: " + sAdminUserID + " called Get Primary Reason Method for Primary Reason ID: " + sPrimaryReasonID;
            logger.LogAdminRecords(nAdminUserID, sDescription, "Get Primary Reason By ID", "Master Entry - Primary Reason");

            #endregion

            if (primaryReason == null)
                return NotFound();
            return primaryReason;
        }
        #endregion

        #region Add Primary Reason
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> AddPrimaryReason(PrimaryReasons primaryReason)
        {
            if (ModelState.IsValid)
            {
                //Check if there's a primary reason with same name 
                PrimaryReasonsRepository rtFac = new PrimaryReasonsRepository(_info);
                IEnumerable<PrimaryReasons> dbPrimaryReasons = await rtFac.SelectWhere("sPrimaryReasonName", primaryReason.sPrimaryReasonName);
                if (dbPrimaryReasons.Count() != 0)
                {
                    //Exit
                    return BadRequest("Cannot Add Primary Reason \"" + primaryReason.sPrimaryReasonName + "\", Primary Reason with Same Name Exists");
                }
                try
                {
                    #region Data Addition ! from UI
                    primaryReason.nWizardID = 12;
                    primaryReason.dtLastUpdate = DateTime.Now;
                    primaryReason.dtCreated = DateTime.Now;
                    //spaces remove
                    string sNormalizedName = GetNormalizedName(primaryReason.sPrimaryReasonName);
                    primaryReason.sNormalizedPrimaryReasonName = await rtFac.GetNormalizedNameByMasterName(sNormalizedName);

                    #endregion

                    int GeneratedID = (int)rtFac.Insert(primaryReason);

                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + primaryReason.nCreatedAdminUserID + " called Add Primary Reason Method & Created Primary Reason with ID: " + GeneratedID;
                    primaryReason.nPrimaryReasonID = GeneratedID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = primaryReason.nCreatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Primary Reason",
                        sEventName = "Add Primary Reason"
                    };
                    logger.InsertAuditSingle(primaryReason, adminModuleLogger);
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

        #region Edit PrimaryReason
        [HttpPost("EditPrimaryReason")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditPrimaryReason(PrimaryReasons primaryReason)
        {
            if (ModelState.IsValid)
            {
                
                //Check if there's a facility with same name 
                PrimaryReasonsRepository rtFac = new PrimaryReasonsRepository(_info);
                IEnumerable<PrimaryReasons> dbPrimaryReasons = await rtFac.SelectWhere("sPrimaryReasonName", primaryReason.sPrimaryReasonName);
                if (dbPrimaryReasons.Count() != 0)
                {
                    if (dbPrimaryReasons.First().nPrimaryReasonID != primaryReason.nPrimaryReasonID)
                    {
                        //Exit
                        return BadRequest("Cannot Edit Primary Reason \"" + primaryReason.sPrimaryReasonName + "\", Primary Reason with Same Name Exists");
                    }
                }
                
                try
                {
                    primaryReason.dtLastUpdate = DateTime.Now;
                    PrimaryReasons oldPrimaryReason = await rtFac.Select(primaryReason.nPrimaryReasonID);
                    if (rtFac.Update(primaryReason))
                    {
                        #region Logging

                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + primaryReason.nUpdatedAdminUserID + " called Edit Primary Reason Method for Primary Reason ID: " + primaryReason.nPrimaryReasonID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = primaryReason.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Master Entry - Primary Reason",
                            sEventName = "Edit Primary Reason"
                        };
                        logger.UpdateAuditSingle(oldPrimaryReason, primaryReason, adminModuleLogger);

                        #endregion
                        return Ok();
                    }
                    else
                    { return NotFound(); }
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

        #region Delete PrimaryReason      
        [HttpGet("DeletePrimaryReason/sPrimaryReasonID={sPrimaryReasonID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> DeletePrimaryReason(string sPrimaryReasonID, string sAdminUserID)
        {
            PrimaryReasonsRepository rtFac = new PrimaryReasonsRepository(_info);
            FacilityPrimaryReasonsRepository rtFacilityFac = new FacilityPrimaryReasonsRepository(_info);
            bool resultPrimaryReasonID = int.TryParse(sPrimaryReasonID, out int nPrimaryReasonID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            try
            {
                PrimaryReasons masterPrimaryReason = await rtFac.Select(nPrimaryReasonID);
                IEnumerable<FacilityPrimaryReasons> facilityPrimaryReasonIEnum = await rtFacilityFac.SelectWhere("nPrimaryReasonID", nPrimaryReasonID);
                List<FacilityPrimaryReasons> facilityPrimaryReasonList = facilityPrimaryReasonIEnum.ToList();
                if (rtFac.DeleteOneToMany(nPrimaryReasonID, "lnkFacilityPrimaryReasons"))
                {
                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + sAdminUserID + " called Delete Primary Reason Method for Primary Reason ID: " + sPrimaryReasonID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Primary Reason",
                        sEventName = "Delete Primary Reason"
                    };
                    logger.DeleteAuditMany(facilityPrimaryReasonList, adminModuleLogger);
                    logger.DeleteAuditSingle(masterPrimaryReason, adminModuleLogger);
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

        #endregion

        #region Master - Sensitive Info - Methods

        #region Get SensitiveInfo / SensitiveInfos
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetSensitiveInfo()
        {
            try
            {
                SensitiveInfoRepository rtFac = new SensitiveInfoRepository(_info);
                IEnumerable<SensitiveInfo> sensitiveInfos = await rtFac.GetAllASC(1000, "sSensitiveInfoName");
                return Ok(sensitiveInfos);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("GetSensitiveInfo/sSensitiveInfoID={sSensitiveInfoID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<SensitiveInfo>> GetSensitiveInfo(string sSensitiveInfoID, string sAdminUserID)
        {
            SensitiveInfoRepository rtFac = new SensitiveInfoRepository(_info);
            bool resultSensitiveInfoID = int.TryParse(sSensitiveInfoID, out int nSensitiveInfoID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            SensitiveInfo sensitiveInfo = await rtFac.Select(nSensitiveInfoID);

            #region Logging

            MROLogger logger = new MROLogger(_info);
            string sDescription = "Admin with ID: " + sAdminUserID + " called Get Sensitive Info Method for Sensitive Info ID: " + sSensitiveInfoID;
            logger.LogAdminRecords(nAdminUserID, sDescription, "Get Sensitive Info By ID", "Master Entry - Sensitive Info");

            #endregion

            if (sensitiveInfo == null)
                return NotFound();
            return sensitiveInfo;
        }
        #endregion

        #region Add Sensitive Info
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> AddSensitiveInfo(SensitiveInfo sensitiveInfo)
        {
            if (ModelState.IsValid)
            {
                //Check if there's asensitive info with same name 
                SensitiveInfoRepository rtFac = new SensitiveInfoRepository(_info);
                IEnumerable<SensitiveInfo> dbSensitiveInfo = await rtFac.SelectWhere("sSensitiveInfoName", sensitiveInfo.sSensitiveInfoName);
                if (dbSensitiveInfo.Count() != 0)
                {
                    //Exit
                    return BadRequest("Cannot Add Sensitive Info \"" + sensitiveInfo.sSensitiveInfoName + "\", Sensitive Info with Same Name Exists");
                }
                try
                {
                    #region Data Addition ! from UI
                    sensitiveInfo.nWizardID = 11;
                    sensitiveInfo.dtLastUpdate = DateTime.Now;
                    sensitiveInfo.dtCreated = DateTime.Now;
                    //spaces remove
                    string sNormalizedName = GetNormalizedName(sensitiveInfo.sSensitiveInfoName);
                    sensitiveInfo.sNormalizedSensitiveInfoName = await rtFac.GetNormalizedNameByMasterName(sNormalizedName);

                    #endregion

                    int GeneratedID = (int)rtFac.Insert(sensitiveInfo);

                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + sensitiveInfo.nCreatedAdminUserID + " called Add Sensitive Info Method & Created Sensitive Info with ID: " + GeneratedID;
                    sensitiveInfo.nSensitiveInfoID = GeneratedID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = sensitiveInfo.nCreatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Sensitive Info",
                        sEventName = "Add Sensitive Info"
                    };
                    logger.InsertAuditSingle(sensitiveInfo, adminModuleLogger);
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

        #region Edit SensitiveInfo
        [HttpPost("EditSensitiveInfo")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditSensitiveInfo(SensitiveInfo sensitiveInfo)
        {
            if (ModelState.IsValid)
            {
                //Check if there's a facility with same name 
                SensitiveInfoRepository rtFac = new SensitiveInfoRepository(_info);
                IEnumerable<SensitiveInfo> dbSensitiveInfo = await rtFac.SelectWhere("sSensitiveInfoName", sensitiveInfo.sSensitiveInfoName);
                if (dbSensitiveInfo.Count() != 0)
                {
                    if (dbSensitiveInfo.First().nSensitiveInfoID != sensitiveInfo.nSensitiveInfoID)
                    {
                        //Exit
                        return BadRequest("Cannot Edit Sensitive Info \"" + sensitiveInfo.sSensitiveInfoName + "\", Sensitive Info with Same Name Exists");
                    }
                }
                try
                {
                    sensitiveInfo.dtLastUpdate = DateTime.Now;
                    SensitiveInfo oldSensitiveInfo = await rtFac.Select(sensitiveInfo.nSensitiveInfoID);
                    if (rtFac.Update(sensitiveInfo))
                    {
                        #region Logging

                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + sensitiveInfo.nUpdatedAdminUserID + " called Edit Sensitive Info Method for Sensitive Info ID: " + sensitiveInfo.nSensitiveInfoID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = sensitiveInfo.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Master Entry - Sensitive Info",
                            sEventName = "Edit Sensitive Info"
                        };
                        logger.UpdateAuditSingle(oldSensitiveInfo, sensitiveInfo, adminModuleLogger);

                        #endregion
                        return Ok();
                    }
                    else
                    { return NotFound(); }
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

        #region Delete SensitiveInfo      
        [HttpGet("DeleteSensitiveInfo/sSensitiveInfoID={sSensitiveInfoID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> DeleteSensitiveInfo(string sSensitiveInfoID, string sAdminUserID)
        {
            SensitiveInfoRepository rtFac = new SensitiveInfoRepository(_info);
            FacilitySensitiveInfoRepository rtFacilityFac = new FacilitySensitiveInfoRepository(_info);
            bool resultSensitiveInfoID = int.TryParse(sSensitiveInfoID, out int nSensitiveInfoID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            try
            {
                SensitiveInfo masterSensitiveInfo = await rtFac.Select(nSensitiveInfoID);
                IEnumerable<FacilitySensitiveInfo> facilitySensitiveInfoIEnum = await rtFacilityFac.SelectWhere("nSensitiveInfoID", nSensitiveInfoID);
                List<FacilitySensitiveInfo> facilitySensitiveInfoList = facilitySensitiveInfoIEnum.ToList();
                if (rtFac.DeleteOneToMany(nSensitiveInfoID, "lnkFacilitySensitiveInfo"))
                {
                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + sAdminUserID + " called Delete Sensitive Info Method for Sensitive Info ID: " + sSensitiveInfoID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Sensitive Info",
                        sEventName = "Delete Sensitive Info"
                    };
                    logger.DeleteAuditMany(facilitySensitiveInfoList, adminModuleLogger);
                    logger.DeleteAuditSingle(masterSensitiveInfo, adminModuleLogger);
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

        #endregion

        #region Master - Shipment Type - Methods

        #region Get ShipmentType / ShipmentTypes
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetShipmentTypes()
        {
            try
            {
                ShipmentTypesRepository rtFac = new ShipmentTypesRepository(_info);
                IEnumerable<ShipmentTypes> shipmentTypes = await rtFac.GetAllASC(1000, "sShipmentTypeName");
                return Ok(shipmentTypes);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("GetShipmentType/sShipmentTypeID={sShipmentTypeID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<ShipmentTypes>> GetShipmentType(string sShipmentTypeID, string sAdminUserID)
        {
            ShipmentTypesRepository rtFac = new ShipmentTypesRepository(_info);
            bool resultShipmentTypeID = int.TryParse(sShipmentTypeID, out int nShipmentTypeID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            ShipmentTypes shipmentType = await rtFac.Select(nShipmentTypeID);

            #region Logging

            MROLogger logger = new MROLogger(_info);
            string sDescription = "Admin with ID: " + sAdminUserID + " called Get Shipment Type Method for Shipment Type ID: " + sShipmentTypeID;
            logger.LogAdminRecords(nAdminUserID, sDescription, "Get Shipment Type By ID", "Master Entry - Shipment Type");

            #endregion

            if (shipmentType == null)
                return NotFound();
            return shipmentType;
        }
        #endregion

        #region Add Shipment Type
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> AddShipmentType(ShipmentTypes shipmentType)
        {
            if (ModelState.IsValid)
            {
                //Check if there's a shipment type with same name 
                ShipmentTypesRepository rtFac = new ShipmentTypesRepository(_info);
                IEnumerable<ShipmentTypes> dbShipmentTypes = await rtFac.SelectWhere("sShipmentTypeName", shipmentType.sShipmentTypeName);
                if (dbShipmentTypes.Count() != 0)
                {
                    //Exit
                    return BadRequest("Cannot Add Shipment Type \"" + shipmentType.sShipmentTypeName + "\", Shipment Type with Same Name Exists");
                }
                try
                {
                    #region Data Addition ! from UI
                    shipmentType.nWizardID = 14;
                    shipmentType.dtLastUpdate = DateTime.Now;
                    shipmentType.dtCreated = DateTime.Now;
                    //spaces remove
                    string sNormalizedName = GetNormalizedName(shipmentType.sShipmentTypeName);
                    shipmentType.sNormalizedShipmentTypeName = await rtFac.GetNormalizedNameByMasterName(sNormalizedName);

                    #endregion

                    int GeneratedID = (int)rtFac.Insert(shipmentType);

                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + shipmentType.nCreatedAdminUserID + " called Add Shipment Type Method & Created Shipment Type with ID: " + GeneratedID;
                    shipmentType.nShipmentTypeID = GeneratedID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = shipmentType.nCreatedAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Shipment Type",
                        sEventName = "Add Shipment Type"
                    };
                    logger.InsertAuditSingle(shipmentType, adminModuleLogger);
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

        #region Edit ShipmentType
        [HttpPost("EditShipmentType")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditShipmentType(ShipmentTypes shipmentType)
        {
            if (ModelState.IsValid)
            {
                //Check if there's a facility with same name 
                ShipmentTypesRepository rtFac = new ShipmentTypesRepository(_info);
                IEnumerable<ShipmentTypes> dbShipmentTypes = await rtFac.SelectWhere("sShipmentTypeName", shipmentType.sShipmentTypeName);
                if (dbShipmentTypes.Count() != 0)
                {
                    if (dbShipmentTypes.First().nShipmentTypeID != shipmentType.nShipmentTypeID)
                    {
                        //Exit
                        return BadRequest("Cannot Edit Shipment Type \"" + shipmentType.sShipmentTypeName + "\", Shipment Type with Same Name Exists");
                    }
                }
                try
                {
                    shipmentType.dtLastUpdate = DateTime.Now;
                    ShipmentTypes oldShipmentType = await rtFac.Select(shipmentType.nShipmentTypeID);
                    if (rtFac.Update(shipmentType))
                    {
                        #region Logging

                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + shipmentType.nUpdatedAdminUserID + " called Edit Shipment Type Method for Shipment Type ID: " + shipmentType.nShipmentTypeID;
                        AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = shipmentType.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Master Entry - Shipment Type",
                            sEventName = "Edit Shipment Type"
                        };
                        logger.UpdateAuditSingle(oldShipmentType, shipmentType, adminModuleLogger);

                        #endregion
                        return Ok();
                    }
                    else
                    { return NotFound(); }
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

        #region Delete ShipmentType      
        [HttpGet("DeleteShipmentType/sShipmentTypeID={sShipmentTypeID}&sAdminUserID={sAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> DeleteShipmentType(string sShipmentTypeID, string sAdminUserID)
        {
            ShipmentTypesRepository rtFac = new ShipmentTypesRepository(_info);
            FacilityShipmentTypesRepository rtFacilityFac = new FacilityShipmentTypesRepository(_info);
            bool resultShipmentTypeID = int.TryParse(sShipmentTypeID, out int nShipmentTypeID);
            bool resultAdminID = int.TryParse(sAdminUserID, out int nAdminUserID);
            try
            {
                ShipmentTypes masterShipmentType = await rtFac.Select(nShipmentTypeID);
                IEnumerable<FacilityShipmentTypes> facilityShipmentTypeIEnum = await rtFacilityFac.SelectWhere("nShipmentTypeID", nShipmentTypeID);
                List<FacilityShipmentTypes> facilityShipmentTypeList = facilityShipmentTypeIEnum.ToList();
                if (rtFac.DeleteOneToMany(nShipmentTypeID, "lnkFacilityShipmentTypes"))
                {
                    #region Logging
                    MROLogger logger = new MROLogger(_info);
                    string sDescription = "Admin with ID: " + sAdminUserID + " called Delete Shipment Type Method for Shipment Type ID: " + sShipmentTypeID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Master Entry - Shipment Type",
                        sEventName = "Delete Shipment Type"
                    };
                    logger.DeleteAuditMany(facilityShipmentTypeList, adminModuleLogger);
                    logger.DeleteAuditSingle(masterShipmentType, adminModuleLogger);
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

        #endregion

        #region Generic GetNormalizedName method
        private string GetNormalizedName(string normalizedString)
        {
            string removedSpecialChar = Regex.Replace(normalizedString, @"[^0-9a-zA-Z]+", "");
            string finalString = Regex.Replace(removedSpecialChar, @"\s+", "");
            finalString = "MRO" + finalString;
            return finalString;
        }
        #endregion
    }
}
