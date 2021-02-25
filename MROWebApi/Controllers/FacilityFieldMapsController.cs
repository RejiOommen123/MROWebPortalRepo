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
    public class FacilityFieldMapsController : ControllerBase
    {
        #region Facility Field Maps Constructor
        private readonly DBConnectionInfo _info;
        public FacilityFieldMapsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility Field Maps
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IEnumerable<FacilityFieldMaps>> GetFacilityFieldMaps()
        {
            FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
            return await facilityFeldMapsRepository.GetAllASC(1000, "nFacilityFieldMapID");
        }

        [HttpGet("GetFacilityFieldMaps/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<FacilityFieldMaps>> GetFacilityFieldMaps(string id)
        {
            FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
            bool result = int.TryParse(id, out int nId);
            FacilityFieldMaps facilityFieldMaps = await facilityFeldMapsRepository.Select(nId);
            if (facilityFieldMaps == null)
                return NotFound();
            return facilityFieldMaps;
        }

        [HttpGet("GetFieldsByFacilityID/nFacilityID={nFacilityID}&nFacilityLocationID={nFacilityLocationID}&nAdminUserID={nAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFieldsByFacilityID(int nFacilityID, int nFacilityLocationID, int nAdminUserID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                IEnumerable<dynamic> fields = await fieldsRepository.EditFields(nFacilityID, nFacilityLocationID, nAdminUserID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(nFacilityID);
                FacilityLocations facilityLocation = null;
                if (nFacilityLocationID != 0) { 
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
                    //string sDescription = "Get Facility Fields Method was called for Facility ID: " + nFacilityID;
                    //logger.LogAdminRecords(nAdminUserID, sDescription, "Get Facility Fields By ID", "Manage Facilities");
                    AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                    string sDescription = "Get Facility Fields Method was called for Facility ID: " + nFacilityID;
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger()
                    {
                        nAdminUserID = nAdminUserID,
                        sDescription = sDescription,
                        sModuleName = "Manage Facilities",
                        sEventName = "Get Facility Fields By ID",
                        nFacilityID = facility.nFacilityID,
                        dtLogTime = DateTime.Now,
                    };
                    adminModuleLoggerRepository.Insert(adminModuleLogger);
                }
                #endregion
                return Ok(new { fields, titleName });
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
        public async Task<IActionResult> EditFacilityFields(EditFields editFields)
        {
            IEnumerable<FacilitiesFieldMapTable> InsertPRList = null, InsertRTList = null, InsertSIList = null, InsertSTList = null, InsertFMList = null, InsertPRepList = null,
                UpdatePRList = null, UpdateRTList = null, UpdateSIList = null, UpdateSTList = null, UpdateFMList = null, UpdatePRepList = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (editFields.nFacilityLocationID != 0)
                    {
                        InsertPRList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons" && map.nFacilityLocationID == 0);
                        InsertRTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes" && map.nFacilityLocationID == 0);
                        InsertSIList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo" && map.nFacilityLocationID == 0);
                        InsertSTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes" && map.nFacilityLocationID == 0);
                        InsertFMList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps" && map.nFacilityLocationID == 0);
                        InsertPRepList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPatientRepresentatives" && map.nFacilityLocationID == 0);

                        UpdatePRList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons" && map.nFacilityLocationID != 0);
                        UpdateRTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes" && map.nFacilityLocationID != 0);
                        UpdateSIList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo" && map.nFacilityLocationID != 0);
                        UpdateSTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes" && map.nFacilityLocationID != 0);
                        UpdateFMList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps" && map.nFacilityLocationID != 0);
                        UpdatePRepList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPatientRepresentatives" && map.nFacilityLocationID != 0);
                    }
                    else
                    {
                        UpdatePRList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons");
                        UpdateRTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes");
                        UpdateSIList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo");
                        UpdateSTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes");
                        UpdateFMList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps");
                        UpdatePRepList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPatientRepresentatives");
                    }
                    //Repos Created
                    FacilityFieldMapsRepository facilityFieldMapsRepository = new FacilityFieldMapsRepository(_info);
                    FacilityPrimaryReasonsRepository primaryReasonRepo = new FacilityPrimaryReasonsRepository(_info);
                    FacilityShipmentTypesRepository shipmentTypeRepo = new FacilityShipmentTypesRepository(_info);
                    FacilitySensitiveInfoRepository sensitiveInfoRepo = new FacilitySensitiveInfoRepository(_info);
                    FacilityRecordTypesRepository recordTypeRepo = new FacilityRecordTypesRepository(_info);
                    FacilityPatientRepresentativesRepository patientRepresentativeRepo = new FacilityPatientRepresentativesRepository(_info);


                    //foreach (FacilitiesFieldMapTable row in fieldFacilityMapsTable)
                    //{
                    //    row.sFieldName = row.sFieldName.Replace("Primary Reason - ", string.Empty);
                    //    row.sFieldName = row.sFieldName.Replace("Record Type - ", string.Empty);
                    //    row.sFieldName = row.sFieldName.Replace("Sensitive Info - ", string.Empty);
                    //    row.sFieldName = row.sFieldName.Replace("Shipment Type - ", string.Empty);
                    //}

                    //Separating Table Data
                    //var PRList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons");
                    //var RTList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes");
                    //var SIList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo");
                    //var STList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes");
                    //var FMList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps");
                    //var PRepList = editFields.fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPatientRepresentatives");

                    var singleRecord = editFields.fieldFacilityMapsTable.FirstOrDefault();
                    List<int> idList = new List<int>();
                    int[] idArray = new int[] { };
                    AdminModuleLogger adminModuleLogger = new AdminModuleLogger();
                    List<FacilityShipmentTypes> oldShipmentTypes = new List<FacilityShipmentTypes>();
                    List<FacilityPrimaryReasons> oldPrimaryReasons = new List<FacilityPrimaryReasons>();
                    List<FacilityRecordTypes> oldRecordsTypes = new List<FacilityRecordTypes>();
                    List<FacilitySensitiveInfo> oldSensitiveInfos = new List<FacilitySensitiveInfo>();
                    List<FacilityFieldMaps> oldFieldMaps = new List<FacilityFieldMaps>();
                    List<FacilityPatientRepresentatives> oldPatientRepresentatives = new List<FacilityPatientRepresentatives>();

                    MROLogger logger = new MROLogger(_info);
                    int nAdminUserID = singleRecord.nUpdatedAdminUserID;
                    int nFacilityID = singleRecord.nFacilityID;
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(nFacilityID);

                    #region Update facility fields data

                    //Facility Shipment Types 
                    if (UpdateSTList.Count() != 0)
                    {
                        List<FacilityShipmentTypes> shipmentTypeList = new List<FacilityShipmentTypes>();
                        foreach (var shipmentType in UpdateSTList)
                        {
                            if (shipmentType.nFieldOrder == null)
                            {
                                shipmentType.nFieldOrder = 1;
                            }
                            shipmentTypeList.Add(
                                new FacilityShipmentTypes()
                                {
                                    nFacilityShipmentTypeID = shipmentType.nFacilityFieldMapID,
                                    nShipmentTypeID = shipmentType.nFieldID,
                                    nFacilityID = shipmentType.nFacilityID,
                                    sShipmentTypeName = shipmentType.sFieldName,
                                    nFieldOrder = shipmentType.nFieldOrder,
                                    nWizardID = shipmentType.nWizardID,
                                    bShow = shipmentType.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = shipmentType.nUpdatedAdminUserID,
                                    dtCreated = shipmentType.dtCreated,
                                    nCreatedAdminUserID = shipmentType.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(shipmentType.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var eShipmentlist = await shipmentTypeRepo.SelectListByInClause(idArray, "lnkFacilityShipmentTypes", "nShipmentTypeID", singleRecord.nFacilityID,editFields.nFacilityLocationID);

                        foreach (var item in eShipmentlist)
                        {
                            oldShipmentTypes.Add(item);
                        }

                        shipmentTypeRepo.UpdateMany(shipmentTypeList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Shipment Type for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Shipment Type",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldShipmentTypes, shipmentTypeList, adminModuleLogger, "nFacilityShipmentTypeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }
                    //Facility PrimaryReasons
                    if (UpdatePRList.Count() != 0)
                    {
                        List<FacilityPrimaryReasons> primaryReasonsList = new List<FacilityPrimaryReasons>();
                        foreach (var primaryReason in UpdatePRList)
                        {
                            if (primaryReason.nFieldOrder == null)
                            {
                                primaryReason.nFieldOrder = 1;
                            }
                            primaryReasonsList.Add(
                                new FacilityPrimaryReasons()
                                {
                                    nFacilityPrimaryReasonID = primaryReason.nFacilityFieldMapID,
                                    nFacilityID = primaryReason.nFacilityID,
                                    nPrimaryReasonID = primaryReason.nFieldID,
                                    sPrimaryReasonName = primaryReason.sFieldName,
                                    nFieldOrder = primaryReason.nFieldOrder,
                                    nWizardID = primaryReason.nWizardID,
                                    bShow = primaryReason.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = primaryReason.nUpdatedAdminUserID,
                                    dtCreated = primaryReason.dtCreated,
                                    nCreatedAdminUserID = primaryReason.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(primaryReason.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var ePrimarylist = await primaryReasonRepo.SelectListByInClause(idArray, "lnkFacilityPrimaryReasons", "nPrimaryReasonID", singleRecord.nFacilityID, editFields.nFacilityLocationID);

                        foreach (var item in ePrimarylist)
                        {
                            oldPrimaryReasons.Add(item);
                        }

                        primaryReasonRepo.UpdateMany(primaryReasonsList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Primary Reason for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Primary Reason",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldPrimaryReasons, primaryReasonsList, adminModuleLogger, "nFacilityPrimaryReasonID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Sensitive Info
                    if (UpdateSIList.Count() != 0)
                    {
                        List<FacilitySensitiveInfo> sensitiveInfoList = new List<FacilitySensitiveInfo>();
                        foreach (var sensitiveInfo in UpdateSIList)
                        {
                            if (sensitiveInfo.nFieldOrder == null)
                            {
                                sensitiveInfo.nFieldOrder = 1;
                            }
                            sensitiveInfoList.Add(
                                new FacilitySensitiveInfo()
                                {
                                    nFacilitySensitiveInfoID = sensitiveInfo.nFacilityFieldMapID,
                                    nSensitiveInfoID = sensitiveInfo.nFieldID,
                                    nFacilityID = sensitiveInfo.nFacilityID,
                                    sSensitiveInfoName = sensitiveInfo.sFieldName,
                                    nFieldOrder = sensitiveInfo.nFieldOrder,
                                    nWizardID = sensitiveInfo.nWizardID,
                                    bShow = sensitiveInfo.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = sensitiveInfo.nUpdatedAdminUserID,
                                    dtCreated = sensitiveInfo.dtCreated,
                                    nCreatedAdminUserID = sensitiveInfo.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(sensitiveInfo.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var eSensitivelist = await sensitiveInfoRepo.SelectListByInClause(idArray, "lnkFacilitySensitiveInfo", "nSensitiveInfoID", singleRecord.nFacilityID, editFields.nFacilityLocationID);

                        foreach (var item in eSensitivelist)
                        {
                            oldSensitiveInfos.Add(item);
                        }

                        sensitiveInfoRepo.UpdateMany(sensitiveInfoList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Sensitive Info for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Sensitive Info",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldSensitiveInfos, sensitiveInfoList, adminModuleLogger, "nFacilitySensitiveInfoID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Record Types 
                    if (UpdateRTList.Count() != 0)
                    {
                        List<FacilityRecordTypes> recordTypeList = new List<FacilityRecordTypes>();
                        foreach (var recordType in UpdateRTList)
                        {
                            if (recordType.nFieldOrder == null)
                            {
                                recordType.nFieldOrder = 1;
                            }
                            recordTypeList.Add(
                                new FacilityRecordTypes()
                                {
                                    nFacilityRecordTypeID = recordType.nFacilityFieldMapID,
                                    nFacilityID = recordType.nFacilityID,
                                    nRecordTypeID = recordType.nFieldID,
                                    sRecordTypeName = recordType.sFieldName,
                                    nFieldOrder = recordType.nFieldOrder,
                                    nWizardID = recordType.nWizardID,
                                    bShow = recordType.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = recordType.nUpdatedAdminUserID,
                                    dtCreated = recordType.dtCreated,
                                    nCreatedAdminUserID = recordType.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(recordType.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var eRecordlist = await recordTypeRepo.SelectListByInClause(idArray, "lnkFacilityRecordTypes", "nRecordTypeID", singleRecord.nFacilityID, editFields.nFacilityLocationID);

                        foreach (var item in eRecordlist)
                        {
                            oldRecordsTypes.Add(item);
                        }

                        recordTypeRepo.UpdateMany(recordTypeList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Record Type for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Record Type",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldRecordsTypes, recordTypeList, adminModuleLogger, "nFacilityRecordTypeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Field Maps 
                    if (UpdateFMList.Count() != 0)
                    {
                        List<FacilityFieldMaps> fieldMapsList = new List<FacilityFieldMaps>();
                        foreach (var fieldMaps in UpdateFMList)
                        {
                            fieldMapsList.Add(
                                new FacilityFieldMaps()
                                {
                                    nFacilityFieldMapID = fieldMaps.nFacilityFieldMapID,
                                    nFacilityID = fieldMaps.nFacilityID,
                                    nFieldID = fieldMaps.nFieldID,
                                    nWizardID = fieldMaps.nWizardID,
                                    bShow = fieldMaps.bShow,
                                    nFieldOrder = fieldMaps.nFieldOrder,
                                    nUpdatedAdminUserID = fieldMaps.nUpdatedAdminUserID,
                                    dtLastUpdate = DateTime.Now,
                                    dtCreated = fieldMaps.dtCreated,
                                    nCreatedAdminUserID = fieldMaps.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(fieldMaps.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var eFieldlist = await facilityFieldMapsRepository.SelectListByInClause(idArray, "lnkFacilityFieldMaps", "nFieldID", singleRecord.nFacilityID, editFields.nFacilityLocationID);

                        foreach (var item in eFieldlist)
                        {
                            oldFieldMaps.Add(item);
                        }

                        facilityFieldMapsRepository.UpdateMany(fieldMapsList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Field for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Field",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldFieldMaps, fieldMapsList, adminModuleLogger, "nFacilityFieldMapID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Patient Representative 
                    if (UpdatePRepList.Count() != 0)
                    {
                        List<FacilityPatientRepresentatives> patientRepresentativeList = new List<FacilityPatientRepresentatives>();
                        foreach (var patientRepresentative in UpdatePRepList)
                        {
                            if (patientRepresentative.nFieldOrder == null)
                            {
                                patientRepresentative.nFieldOrder = 1;
                            }
                            patientRepresentativeList.Add(
                                new FacilityPatientRepresentatives()
                                {
                                    nFacilityPatientRepresentativeID = patientRepresentative.nFacilityFieldMapID,
                                    nPatientRepresentativeID = patientRepresentative.nFieldID,
                                    nFacilityID = patientRepresentative.nFacilityID,
                                    sPatientRepresentativeName = patientRepresentative.sFieldName,
                                    nFieldOrder = patientRepresentative.nFieldOrder,
                                    nWizardID = patientRepresentative.nWizardID,
                                    bShow = patientRepresentative.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = patientRepresentative.nUpdatedAdminUserID,
                                    dtCreated = patientRepresentative.dtCreated,
                                    nCreatedAdminUserID = patientRepresentative.nCreatedAdminUserID,
                                    nFacilityLocationID = editFields.nFacilityLocationID
                                }
                                );
                            idList.Add(patientRepresentative.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var ePatientReplist = await patientRepresentativeRepo.SelectListByInClause(idArray, "lnkFacilityPatientRepresentatives", "nPatientRepresentativeID", singleRecord.nFacilityID, editFields.nFacilityLocationID);

                        foreach (var item in ePatientReplist)
                        {
                            oldPatientRepresentatives.Add(item);
                        }

                        patientRepresentativeRepo.UpdateMany(patientRepresentativeList);

                        if (facility.bFacilityLogging)
                        {
                            adminModuleLogger = new AdminModuleLogger()
                            {
                                nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                sDescription = $"Edited Patient Representative for {(editFields.nFacilityLocationID != 0 ? "Location ID : " + singleRecord.nFacilityLocationID : "Facility ID : " + singleRecord.nFacilityID)}",
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Patient Representative",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldPatientRepresentatives, patientRepresentativeList, adminModuleLogger, "nFacilityPatientRepresentativeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }
                    #endregion

                    #region Insert facility fields data
                    if (editFields.nFacilityLocationID != 0)
                    {
                        //Facility Shipment Types 
                        if (InsertSTList.Count() != 0)
                        {
                            List<FacilityShipmentTypes> shipmentTypeList = new List<FacilityShipmentTypes>();
                            foreach (var shipmentType in InsertSTList)
                            {
                                if (shipmentType.nFieldOrder == null)
                                {
                                    shipmentType.nFieldOrder = 1;
                                }
                                shipmentTypeList.Add(
                                    new FacilityShipmentTypes()
                                    {
                                        nFacilityShipmentTypeID = shipmentType.nFacilityFieldMapID,
                                        nShipmentTypeID = shipmentType.nFieldID,
                                        nFacilityID = shipmentType.nFacilityID,
                                        sShipmentTypeName = shipmentType.sFieldName,
                                        nFieldOrder = shipmentType.nFieldOrder,
                                        nWizardID = shipmentType.nWizardID,
                                        bShow = shipmentType.bShow,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = shipmentType.nUpdatedAdminUserID,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = shipmentType.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }
                            shipmentTypeRepo.InsertMany(shipmentTypeList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Shipment Type for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Shipment Type",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(shipmentTypeList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }
                        //Facility PrimaryReasons
                        if (InsertPRList.Count() != 0)
                        {
                            List<FacilityPrimaryReasons> primaryReasonsList = new List<FacilityPrimaryReasons>();
                            foreach (var primaryReason in InsertPRList)
                            {
                                if (primaryReason.nFieldOrder == null)
                                {
                                    primaryReason.nFieldOrder = 1;
                                }
                                primaryReasonsList.Add(
                                    new FacilityPrimaryReasons()
                                    {
                                        nFacilityPrimaryReasonID = primaryReason.nFacilityFieldMapID,
                                        nFacilityID = primaryReason.nFacilityID,
                                        nPrimaryReasonID = primaryReason.nFieldID,
                                        sPrimaryReasonName = primaryReason.sFieldName,
                                        nFieldOrder = primaryReason.nFieldOrder,
                                        nWizardID = primaryReason.nWizardID,
                                        bShow = primaryReason.bShow,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = primaryReason.nUpdatedAdminUserID,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = primaryReason.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }
                            primaryReasonRepo.InsertMany(primaryReasonsList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Primary Reason for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Primary Reason",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(primaryReasonsList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }

                        //Facility Sensitive Info
                        if (InsertSIList.Count() != 0)
                        {
                            List<FacilitySensitiveInfo> sensitiveInfoList = new List<FacilitySensitiveInfo>();
                            foreach (var sensitiveInfo in InsertSIList)
                            {
                                if (sensitiveInfo.nFieldOrder == null)
                                {
                                    sensitiveInfo.nFieldOrder = 1;
                                }
                                sensitiveInfoList.Add(
                                    new FacilitySensitiveInfo()
                                    {
                                        nFacilitySensitiveInfoID = sensitiveInfo.nFacilityFieldMapID,
                                        nSensitiveInfoID = sensitiveInfo.nFieldID,
                                        nFacilityID = sensitiveInfo.nFacilityID,
                                        sSensitiveInfoName = sensitiveInfo.sFieldName,
                                        nFieldOrder = sensitiveInfo.nFieldOrder,
                                        nWizardID = sensitiveInfo.nWizardID,
                                        bShow = sensitiveInfo.bShow,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = sensitiveInfo.nUpdatedAdminUserID,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = sensitiveInfo.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }

                            sensitiveInfoRepo.InsertMany(sensitiveInfoList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Sensitive Info for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Sensitive Info",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(sensitiveInfoList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }

                        //Facility Record Types 
                        if (InsertRTList.Count() != 0)
                        {
                            List<FacilityRecordTypes> recordTypeList = new List<FacilityRecordTypes>();
                            foreach (var recordType in InsertRTList)
                            {
                                if (recordType.nFieldOrder == null)
                                {
                                    recordType.nFieldOrder = 1;
                                }
                                recordTypeList.Add(
                                    new FacilityRecordTypes()
                                    {
                                        nFacilityRecordTypeID = recordType.nFacilityFieldMapID,
                                        nFacilityID = recordType.nFacilityID,
                                        nRecordTypeID = recordType.nFieldID,
                                        sRecordTypeName = recordType.sFieldName,
                                        nFieldOrder = recordType.nFieldOrder,
                                        nWizardID = recordType.nWizardID,
                                        bShow = recordType.bShow,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = recordType.nUpdatedAdminUserID,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = recordType.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }
                            recordTypeRepo.InsertMany(recordTypeList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Record Type for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Record Type",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(recordTypeList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }

                        //Facility Field Maps 
                        if (InsertFMList.Count() != 0)
                        {
                            List<FacilityFieldMaps> fieldMapsList = new List<FacilityFieldMaps>();
                            foreach (var fieldMaps in InsertFMList)
                            {
                                fieldMapsList.Add(
                                    new FacilityFieldMaps()
                                    {
                                        nFacilityFieldMapID = fieldMaps.nFacilityFieldMapID,
                                        nFacilityID = fieldMaps.nFacilityID,
                                        nFieldID = fieldMaps.nFieldID,
                                        nWizardID = fieldMaps.nWizardID,
                                        bShow = fieldMaps.bShow,
                                        nFieldOrder = fieldMaps.nFieldOrder,
                                        nUpdatedAdminUserID = fieldMaps.nUpdatedAdminUserID,
                                        dtLastUpdate = DateTime.Now,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = fieldMaps.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }
                            facilityFieldMapsRepository.InsertMany(fieldMapsList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Field for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Field",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(fieldMapsList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }

                        //Facility Patient Representative 
                        if (InsertPRepList.Count() != 0)
                        {
                            List<FacilityPatientRepresentatives> patientRepresentativeList = new List<FacilityPatientRepresentatives>();
                            foreach (var patientRepresentative in InsertPRepList)
                            {
                                if (patientRepresentative.nFieldOrder == null)
                                {
                                    patientRepresentative.nFieldOrder = 1;
                                }
                                patientRepresentativeList.Add(
                                    new FacilityPatientRepresentatives()
                                    {
                                        nFacilityPatientRepresentativeID = patientRepresentative.nFacilityFieldMapID,
                                        nPatientRepresentativeID = patientRepresentative.nFieldID,
                                        nFacilityID = patientRepresentative.nFacilityID,
                                        sPatientRepresentativeName = patientRepresentative.sFieldName,
                                        nFieldOrder = patientRepresentative.nFieldOrder,
                                        nWizardID = patientRepresentative.nWizardID,
                                        bShow = patientRepresentative.bShow,
                                        dtLastUpdate = DateTime.Now,
                                        nUpdatedAdminUserID = patientRepresentative.nUpdatedAdminUserID,
                                        dtCreated = DateTime.Now,
                                        nCreatedAdminUserID = patientRepresentative.nUpdatedAdminUserID,
                                        nFacilityLocationID = editFields.nFacilityLocationID
                                    }
                                    );
                            }
                            patientRepresentativeRepo.InsertMany(patientRepresentativeList);

                            if (facility.bFacilityLogging)
                            {
                                adminModuleLogger = new AdminModuleLogger()
                                {
                                    nAdminUserID = singleRecord.nUpdatedAdminUserID,
                                    sDescription = "Edited Patient Representative for Location ID : " + editFields.nFacilityLocationID,
                                    sModuleName = "Edit Location Fields",
                                    sEventName = "Edit Location Patient Representative",
                                    nFacilityID = singleRecord.nFacilityID
                                };
                                logger.InsertAuditMany(patientRepresentativeList, adminModuleLogger);
                                idList.Clear();
                                idArray = Array.Empty<int>();
                                adminModuleLogger = null;
                            }
                        }
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

        #region Reset To Default Facility Fields
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ResetToDefault(FacilitiesFieldMapTable facilitiesFieldMapTable)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(facilitiesFieldMapTable.nFacilityID);
                    bool deleteResult;
                    MROLogger logger = new MROLogger(_info);
                    AdminModuleLogger adminModuleLogger = null;

                    #region Logging
                    if (facility.bFacilityLogging)
                    {                        
                        string sDescription = "Reset to Default Field Location ID: " + facilitiesFieldMapTable.nFacilityLocationID;
                        adminModuleLogger = new AdminModuleLogger()
                        {
                            nAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                            sDescription = sDescription,
                            sModuleName = "Edit Facility Fields/Disclaimers",
                            sEventName = "Reset to Default Field",
                            nFacilityID = facilitiesFieldMapTable.nFacilityID
                        };
                    }
                    #endregion

                    switch (facilitiesFieldMapTable.sTableName)
                    {
                        case "lnkFacilityPrimaryReasons":
                            FacilityPrimaryReasonsRepository prFac = new FacilityPrimaryReasonsRepository(_info);

                            FacilityPrimaryReasons oldFacilityPrimaryReasons = new FacilityPrimaryReasons{
                                nFacilityPrimaryReasonID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nPrimaryReasonID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                sPrimaryReasonName = facilitiesFieldMapTable.sFieldName,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID                                
                            };
                            FacilityPrimaryReasons newFacilityPrimaryReasons = await prFac.SelectThreeWhereClause("nPrimaryReasonID", facilitiesFieldMapTable.nFieldID,"nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = prFac.Delete(oldFacilityPrimaryReasons.nFacilityPrimaryReasonID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilityPrimaryReasons, newFacilityPrimaryReasons, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilityPrimaryReasons.nFacilityPrimaryReasonID,
                                    nFieldID = newFacilityPrimaryReasons.nPrimaryReasonID,
                                    nFacilityID = newFacilityPrimaryReasons.nFacilityID,
                                    nFacilityLocationID = newFacilityPrimaryReasons.nFacilityLocationID,
                                    sFieldName = newFacilityPrimaryReasons.sPrimaryReasonName,
                                    nFieldOrder = newFacilityPrimaryReasons.nFieldOrder,
                                    bShow = newFacilityPrimaryReasons.bShow,
                                    nWizardID = newFacilityPrimaryReasons.nWizardID,
                                    sTableName = "lnkFacilityPrimaryReasons",
                                    dtCreated = newFacilityPrimaryReasons.dtCreated,
                                    nCreatedAdminUserID = newFacilityPrimaryReasons.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilityPrimaryReasons.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilityPrimaryReasons.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        case "lnkFacilityRecordTypes":
                            FacilityRecordTypesRepository rtFac = new FacilityRecordTypesRepository(_info);

                            FacilityRecordTypes oldFacilityRecordTypes = new FacilityRecordTypes
                            {
                                nFacilityRecordTypeID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nRecordTypeID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                sRecordTypeName = facilitiesFieldMapTable.sFieldName,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID
                            };
                            FacilityRecordTypes newFacilityRecordTypes = await rtFac.SelectThreeWhereClause("nRecordTypeID", facilitiesFieldMapTable.nFieldID, "nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = rtFac.Delete(oldFacilityRecordTypes.nFacilityRecordTypeID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilityRecordTypes, newFacilityRecordTypes, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilityRecordTypes.nFacilityRecordTypeID,
                                    nFieldID = newFacilityRecordTypes.nRecordTypeID,
                                    nFacilityID = newFacilityRecordTypes.nFacilityID,
                                    nFacilityLocationID = newFacilityRecordTypes.nFacilityLocationID,
                                    sFieldName = newFacilityRecordTypes.sRecordTypeName,
                                    nFieldOrder = newFacilityRecordTypes.nFieldOrder,
                                    bShow = newFacilityRecordTypes.bShow,
                                    nWizardID = newFacilityRecordTypes.nWizardID,
                                    sTableName = "lnkFacilityRecordTypes",
                                    dtCreated = newFacilityRecordTypes.dtCreated,
                                    nCreatedAdminUserID = newFacilityRecordTypes.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilityRecordTypes.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilityRecordTypes.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        case "lnkFacilitySensitiveInfo":
                            FacilitySensitiveInfoRepository siFac = new FacilitySensitiveInfoRepository(_info);

                            FacilitySensitiveInfo oldFacilitySensitiveInfo = new FacilitySensitiveInfo
                            {
                                nFacilitySensitiveInfoID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nSensitiveInfoID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                sSensitiveInfoName = facilitiesFieldMapTable.sFieldName,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID
                            };
                            FacilitySensitiveInfo newFacilitySensitiveInfo = await siFac.SelectThreeWhereClause("nSensitiveInfoID", facilitiesFieldMapTable.nFieldID, "nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = siFac.Delete(oldFacilitySensitiveInfo.nFacilitySensitiveInfoID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilitySensitiveInfo, newFacilitySensitiveInfo, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilitySensitiveInfo.nFacilitySensitiveInfoID,
                                    nFieldID = newFacilitySensitiveInfo.nSensitiveInfoID,
                                    nFacilityID = newFacilitySensitiveInfo.nFacilityID,
                                    nFacilityLocationID = newFacilitySensitiveInfo.nFacilityLocationID,
                                    sFieldName = newFacilitySensitiveInfo.sSensitiveInfoName,
                                    nFieldOrder = newFacilitySensitiveInfo.nFieldOrder,
                                    bShow = newFacilitySensitiveInfo.bShow,
                                    nWizardID = newFacilitySensitiveInfo.nWizardID,
                                    sTableName = "lnkFacilitySensitiveInfo",
                                    dtCreated = newFacilitySensitiveInfo.dtCreated,
                                    nCreatedAdminUserID = newFacilitySensitiveInfo.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilitySensitiveInfo.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilitySensitiveInfo.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        case "lnkFacilityShipmentTypes":
                            FacilityShipmentTypesRepository stFac = new FacilityShipmentTypesRepository(_info);

                            FacilityShipmentTypes oldFacilityShipmentTypes = new FacilityShipmentTypes
                            {
                                nFacilityShipmentTypeID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nShipmentTypeID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                sShipmentTypeName = facilitiesFieldMapTable.sFieldName,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID
                            };
                            FacilityShipmentTypes newFacilityShipmentTypes = await stFac.SelectThreeWhereClause("nShipmentTypeID", facilitiesFieldMapTable.nFieldID, "nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = stFac.Delete(oldFacilityShipmentTypes.nFacilityShipmentTypeID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilityShipmentTypes, newFacilityShipmentTypes, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilityShipmentTypes.nFacilityShipmentTypeID,
                                    nFieldID = newFacilityShipmentTypes.nShipmentTypeID,
                                    nFacilityID = newFacilityShipmentTypes.nFacilityID,
                                    nFacilityLocationID = newFacilityShipmentTypes.nFacilityLocationID,
                                    sFieldName = newFacilityShipmentTypes.sShipmentTypeName,
                                    nFieldOrder = newFacilityShipmentTypes.nFieldOrder,
                                    bShow = newFacilityShipmentTypes.bShow,
                                    nWizardID = newFacilityShipmentTypes.nWizardID,
                                    sTableName = "lnkFacilityShipmentTypes",
                                    dtCreated = newFacilityShipmentTypes.dtCreated,
                                    nCreatedAdminUserID = newFacilityShipmentTypes.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilityShipmentTypes.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilityShipmentTypes.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        case "lnkFacilityPatientRepresentatives":
                            FacilityPatientRepresentativesRepository prepFac = new FacilityPatientRepresentativesRepository(_info);

                            FacilityPatientRepresentatives oldFacilityPatientRepresentatives = new FacilityPatientRepresentatives
                            {
                                nFacilityPatientRepresentativeID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nPatientRepresentativeID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                sPatientRepresentativeName = facilitiesFieldMapTable.sFieldName,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID
                            };
                            FacilityPatientRepresentatives newFacilityPatientRepresentatives = await prepFac.SelectThreeWhereClause("nPatientRepresentativeID", facilitiesFieldMapTable.nFieldID, "nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = prepFac.Delete(oldFacilityPatientRepresentatives.nFacilityPatientRepresentativeID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilityPatientRepresentatives, newFacilityPatientRepresentatives, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilityPatientRepresentatives.nFacilityPatientRepresentativeID,
                                    nFieldID = newFacilityPatientRepresentatives.nPatientRepresentativeID,
                                    nFacilityID = newFacilityPatientRepresentatives.nFacilityID,
                                    nFacilityLocationID = newFacilityPatientRepresentatives.nFacilityLocationID,
                                    sFieldName = newFacilityPatientRepresentatives.sPatientRepresentativeName,
                                    nFieldOrder = newFacilityPatientRepresentatives.nFieldOrder,
                                    bShow = newFacilityPatientRepresentatives.bShow,
                                    nWizardID = newFacilityPatientRepresentatives.nWizardID,
                                    sTableName = "lnkFacilityPatientRepresentatives",
                                    dtCreated = newFacilityPatientRepresentatives.dtCreated,
                                    nCreatedAdminUserID = newFacilityPatientRepresentatives.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilityPatientRepresentatives.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilityPatientRepresentatives.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        case "lnkFacilityFieldMaps":
                            FacilityFieldMapsRepository ffFac = new FacilityFieldMapsRepository(_info);

                            FacilityFieldMaps oldFacilityFieldMaps = new FacilityFieldMaps
                            {
                                nFacilityFieldMapID = facilitiesFieldMapTable.nFacilityFieldMapID,
                                nFieldID = facilitiesFieldMapTable.nFieldID,
                                nFacilityID = facilitiesFieldMapTable.nFacilityID,
                                nFacilityLocationID = facilitiesFieldMapTable.nFacilityLocationID,
                                nFieldOrder = facilitiesFieldMapTable.nFieldOrder,
                                nWizardID = facilitiesFieldMapTable.nWizardID,
                                bShow = facilitiesFieldMapTable.bShow,
                                dtLastUpdate = facilitiesFieldMapTable.dtLastUpdate,
                                nUpdatedAdminUserID = facilitiesFieldMapTable.nUpdatedAdminUserID,
                                dtCreated = facilitiesFieldMapTable.dtCreated,
                                nCreatedAdminUserID = facilitiesFieldMapTable.nCreatedAdminUserID
                            };
                            FacilityFieldMaps newFacilityFieldMaps = await ffFac.SelectThreeWhereClause("nFieldID", facilitiesFieldMapTable.nFieldID, "nFacilityId", facilitiesFieldMapTable.nFacilityID, "nFacilityLocationId", 0);

                            deleteResult = ffFac.Delete(oldFacilityFieldMaps.nFacilityFieldMapID);
                            if (deleteResult)
                            {
                                logger.UpdateAuditSingle(oldFacilityFieldMaps, newFacilityFieldMaps, adminModuleLogger);
                                FacilitiesFieldMapTable returnfacilitiesFieldMap = new FacilitiesFieldMapTable()
                                {
                                    nFacilityFieldMapID = newFacilityFieldMaps.nFacilityFieldMapID,
                                    nFieldID = newFacilityFieldMaps.nFieldID,
                                    nFacilityID = newFacilityFieldMaps.nFacilityID,
                                    nFacilityLocationID = newFacilityFieldMaps.nFacilityLocationID,
                                    bShow = newFacilityFieldMaps.bShow,
                                    nWizardID = newFacilityFieldMaps.nWizardID,
                                    sTableName = "lnkFacilityFieldMaps",
                                    dtCreated = newFacilityFieldMaps.dtCreated,
                                    nCreatedAdminUserID = newFacilityFieldMaps.nCreatedAdminUserID,
                                    dtLastUpdate = newFacilityFieldMaps.dtLastUpdate,
                                    nUpdatedAdminUserID = newFacilityFieldMaps.nUpdatedAdminUserID
                                };
                                return Ok(returnfacilitiesFieldMap);
                            }
                            else
                            {
                                return BadRequest("Record not deleted");
                            }

                        default:
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
