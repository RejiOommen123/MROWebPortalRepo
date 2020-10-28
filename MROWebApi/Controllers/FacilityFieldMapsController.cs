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
    [APIKeyAuth]
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

        [HttpGet("GetFieldsByFacilityID/nFacilityID={nFacilityID}&nAdminUserID={nAdminUserID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFieldsByFacilityID(int nFacilityID,int nAdminUserID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                IEnumerable<dynamic> fields = await fieldsRepository.EditFields(nFacilityID, nAdminUserID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(nFacilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
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
                return Ok(new { fields, faciName });
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
        public async Task<IActionResult> EditFacilityFields([FromBody] FacilitiesFieldMapTable[] fieldFacilityMapsTable)
        {
            if (ModelState.IsValid) 
            {
                try
                {

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
                    var PRList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons");
                    var RTList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes");
                    var SIList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo");
                    var STList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes");
                    var FMList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps");
                    var PRepList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPatientRepresentatives");

                    var singleRecord = fieldFacilityMapsTable.FirstOrDefault();
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

                    //Facility Shipment Types 
                    if (STList.Count() != 0) 
                    { 
                        List<FacilityShipmentTypes> shipmentTypeList = new List<FacilityShipmentTypes>();
                        foreach (var shipmentType in STList)
                        {
                            if (shipmentType.nFieldOrder == null)
                            {
                                shipmentType.nFieldOrder = 1;
                            }
                            shipmentTypeList.Add(
                                new FacilityShipmentTypes()
                                {
                                    nShipmentTypeID = shipmentType.nFacilityFieldMapID,
                                    nFacilityID = shipmentType.nFacilityID,
                                    sShipmentTypeName = shipmentType.sFieldName,
                                    nFieldOrder = shipmentType.nFieldOrder,
                                    nWizardID = shipmentType.nWizardID,
                                    bShow = shipmentType.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = shipmentType.nUpdatedAdminUserID,
                                    dtCreated = shipmentType.dtCreated,
                                    nCreatedAdminUserID = shipmentType.nCreatedAdminUserID,
                                    sFieldToolTip=shipmentType.sFieldToolTip
                                }
                                );
                            idList.Add(shipmentType.nFacilityFieldMapID);
                        }
                        idArray = idList.ToArray();
                        var eShipmentlist = await shipmentTypeRepo.SelectListByInClause(idArray, "lnkFacilityShipmentTypes","nShipmentTypeID",singleRecord.nFacilityID);

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
                                sDescription = "Edited Shipment Type for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Shipment Type",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldShipmentTypes, shipmentTypeList, adminModuleLogger, "nShipmentTypeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }
                    //Facility PrimaryReasons
                    if (PRList.Count() != 0)
                    {
                        List<FacilityPrimaryReasons> primaryReasonsList = new List<FacilityPrimaryReasons>();
                        foreach (var primaryReason in PRList)
                        {
                            if (primaryReason.nFieldOrder == null)
                            {
                                primaryReason.nFieldOrder = 1;
                            }
                            primaryReasonsList.Add(
                                new FacilityPrimaryReasons()
                                {
                                    nFacilityID = primaryReason.nFacilityID,
                                    nPrimaryReasonID = primaryReason.nFacilityFieldMapID,
                                    sPrimaryReasonName = primaryReason.sFieldName,
                                    nFieldOrder = primaryReason.nFieldOrder,
                                    nWizardID = primaryReason.nWizardID,
                                    bShow = primaryReason.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = primaryReason.nUpdatedAdminUserID,
                                    dtCreated = primaryReason.dtCreated,
                                    nCreatedAdminUserID = primaryReason.nCreatedAdminUserID,
                                    sFieldToolTip = primaryReason.sFieldToolTip
                                }
                                );
                            idList.Add(primaryReason.nFacilityFieldMapID);
                        }
                        idArray = idList.ToArray();
                        var ePrimarylist = await primaryReasonRepo.SelectListByInClause(idArray, "lnkFacilityPrimaryReasons", "nPrimaryReasonID", singleRecord.nFacilityID);

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
                                sDescription = "Edited Primary Reason for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Primary Reason",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldPrimaryReasons, primaryReasonsList, adminModuleLogger, "nPrimaryReasonID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Sensitive Info
                    if (SIList.Count() != 0)
                    {
                        List<FacilitySensitiveInfo> sensitiveInfoList = new List<FacilitySensitiveInfo>();
                        foreach (var sensitiveInfo in SIList)
                        {
                            if (sensitiveInfo.nFieldOrder == null)
                            {
                                sensitiveInfo.nFieldOrder = 1;
                            }
                            sensitiveInfoList.Add(
                                new FacilitySensitiveInfo()
                                {
                                    nSensitiveInfoID = sensitiveInfo.nFacilityFieldMapID,
                                    nFacilityID = sensitiveInfo.nFacilityID,
                                    sSensitiveInfoName = sensitiveInfo.sFieldName,
                                    nFieldOrder = sensitiveInfo.nFieldOrder,
                                    nWizardID = sensitiveInfo.nWizardID,
                                    bShow = sensitiveInfo.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = sensitiveInfo.nUpdatedAdminUserID,
                                    dtCreated = sensitiveInfo.dtCreated,
                                    nCreatedAdminUserID = sensitiveInfo.nCreatedAdminUserID,
                                    sFieldToolTip = sensitiveInfo.sFieldToolTip
                                }
                                );
                            idList.Add(sensitiveInfo.nFacilityFieldMapID);
                        }
                        idArray = idList.ToArray();
                        var eSensitivelist = await sensitiveInfoRepo.SelectListByInClause(idArray, "lnkFacilitySensitiveInfo", "nSensitiveInfoID", singleRecord.nFacilityID);

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
                                sDescription = "Edited Sensitive Info for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Sensitive Info",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldSensitiveInfos, sensitiveInfoList, adminModuleLogger, "nSensitiveInfoID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Record Types 
                    if (RTList.Count() != 0)
                    {
                        List<FacilityRecordTypes> recordTypeList = new List<FacilityRecordTypes>();
                        foreach (var recordType in RTList)
                        {
                            if (recordType.nFieldOrder == null)
                            {
                                recordType.nFieldOrder = 1;
                            }
                            recordTypeList.Add(
                                new FacilityRecordTypes()
                                {
                                    nFacilityID = recordType.nFacilityID,
                                    nRecordTypeID = recordType.nFacilityFieldMapID,
                                    sRecordTypeName = recordType.sFieldName,
                                    nFieldOrder = recordType.nFieldOrder,
                                    nWizardID = recordType.nWizardID,
                                    bShow = recordType.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = recordType.nUpdatedAdminUserID,
                                    dtCreated = recordType.dtCreated,
                                    nCreatedAdminUserID = recordType.nCreatedAdminUserID,
                                    sFieldToolTip = recordType.sFieldToolTip
                                }
                                );
                            idList.Add(recordType.nFacilityFieldMapID);
                        }
                        idArray = idList.ToArray();
                        var eRecordlist = await recordTypeRepo.SelectListByInClause(idArray, "lnkFacilityRecordTypes", "nRecordTypeID", singleRecord.nFacilityID);

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
                                sDescription = "Edited Record Type for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Record Type",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldRecordsTypes, recordTypeList, adminModuleLogger, "nRecordTypeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Field Maps 
                    if (FMList.Count() != 0)
                    {
                        List<FacilityFieldMaps> fieldMapsList = new List<FacilityFieldMaps>();
                        foreach (var fieldMaps in FMList)
                        {
                            fieldMapsList.Add(
                                new FacilityFieldMaps()
                                {
                                    nFacilityID = fieldMaps.nFacilityID,
                                    nFieldID = fieldMaps.nFieldID,
                                    nWizardID = fieldMaps.nWizardID,
                                    bShow = fieldMaps.bShow,
                                    nFieldOrder = fieldMaps.nFieldOrder,
                                    nUpdatedAdminUserID = fieldMaps.nUpdatedAdminUserID,
                                    dtLastUpdate = DateTime.Now,
                                    dtCreated = fieldMaps.dtCreated,
                                    nCreatedAdminUserID = fieldMaps.nCreatedAdminUserID,
                                    sFieldToolTip = fieldMaps.sFieldToolTip
                                }
                                );
                            idList.Add(fieldMaps.nFieldID);
                        }
                        idArray = idList.ToArray();
                        var eFieldlist = await facilityFieldMapsRepository.SelectListByInClause(idArray, "lnkFacilityFieldMaps", "nFieldID", singleRecord.nFacilityID);

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
                                sDescription = "Edited Field for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Field",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldFieldMaps, fieldMapsList, adminModuleLogger, "nFieldID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    //Facility Patient Representative 
                    if (PRepList.Count() != 0)
                    {
                        List<FacilityPatientRepresentatives> patientRepresentativeList = new List<FacilityPatientRepresentatives>();
                        foreach (var patientRepresentative in PRepList)
                        {
                            if (patientRepresentative.nFieldOrder == null)
                            {
                                patientRepresentative.nFieldOrder = 1;
                            }
                            patientRepresentativeList.Add(
                                new FacilityPatientRepresentatives()
                                {
                                    nPatientRepresentativeID = patientRepresentative.nFacilityFieldMapID,
                                    nFacilityID = patientRepresentative.nFacilityID,
                                    sPatientRepresentativeName = patientRepresentative.sFieldName,
                                    nFieldOrder = patientRepresentative.nFieldOrder,
                                    nWizardID = patientRepresentative.nWizardID,
                                    bShow = patientRepresentative.bShow,
                                    dtLastUpdate = DateTime.Now,
                                    nUpdatedAdminUserID = patientRepresentative.nUpdatedAdminUserID,
                                    dtCreated = patientRepresentative.dtCreated,
                                    nCreatedAdminUserID = patientRepresentative.nCreatedAdminUserID,
                                    sFieldToolTip = patientRepresentative.sFieldToolTip
                                }
                                );
                            idList.Add(patientRepresentative.nFacilityFieldMapID);
                        }
                        idArray = idList.ToArray();
                        var ePatientReplist = await patientRepresentativeRepo.SelectListByInClause(idArray, "lnkFacilityPatientRepresentatives", "nPatientRepresentativeID", singleRecord.nFacilityID);

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
                                sDescription = "Admin with ID: " + singleRecord.nUpdatedAdminUserID + " Edited Patient Representative for Facility ID: " + singleRecord.nFacilityID,
                                sModuleName = "Edit Facility Fields",
                                sEventName = "Edit Facility Patient Representative",
                                nFacilityID = singleRecord.nFacilityID
                            };
                            logger.UpdateAuditMany(oldPatientRepresentatives, patientRepresentativeList, adminModuleLogger, "nPatientRepresentativeID");
                            idList.Clear();
                            idArray = Array.Empty<int>();
                            adminModuleLogger = null;
                        }
                    }

                    return Ok("Success");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            else {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion
    }
}
