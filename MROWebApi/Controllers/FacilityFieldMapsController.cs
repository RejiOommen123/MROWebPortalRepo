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


                    foreach (FacilitiesFieldMapTable row in fieldFacilityMapsTable)
                    {
                        row.sFieldName = row.sFieldName.Replace("Primary Reason - ", string.Empty);
                        row.sFieldName = row.sFieldName.Replace("Record Type - ", string.Empty);
                        row.sFieldName = row.sFieldName.Replace("Sensitive Info - ", string.Empty);
                        row.sFieldName = row.sFieldName.Replace("Shipment Type - ", string.Empty);
                    }

                    //Separating Table Data
                    var PRList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityPrimaryReasons");
                    var RTList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityRecordTypes");
                    var SIList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilitySensitiveInfo");
                    var STList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityShipmentTypes");
                    var FMList = fieldFacilityMapsTable.Where(map => map.sTableName == "lnkFacilityFieldMaps");


                    //Facility Shipment Types 
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
                                dtLastUpdate = DateTime.Now
                            }
                            );
                    }
                    shipmentTypeRepo.UpdateMany(shipmentTypeList);

                    //Facility PrimaryReasons
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
                                dtLastUpdate = DateTime.Now
                            }
                            ); 
                    }
                    primaryReasonRepo.UpdateMany(primaryReasonsList);

                    //Facility Sensitive Info
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
                                dtLastUpdate = DateTime.Now
                            }
                            );

                    }
                    sensitiveInfoRepo.UpdateMany(sensitiveInfoList);
                    

                    //Facility Record Types 
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
                                dtLastUpdate = DateTime.Now
                            }
                            );

                    }
                    recordTypeRepo.UpdateMany(recordTypeList);

                    //Facility Field Maps 
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
                                nCreatedAdminUserID  = fieldMaps.nCreatedAdminUserID,
                                dtCreated = DateTime.Now,
                                nUpdatedAdminUserID = fieldMaps.nUpdatedAdminUserID,
                                dtLastUpdate = DateTime.Now
                            }
                            );

                    }
                    facilityFieldMapsRepository.UpdateMany(fieldMapsList);

                    #region Logging
                    var mapTable = FMList.FirstOrDefault();
                    int nAdminUserID = mapTable.nUpdatedAdminUserID;
                    int nFacilityID = mapTable.nFacilityID;
                    FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                    Facilities facility = await facRepo.Select(nFacilityID);
                    if (facility.bFacilityLogging) {
                        MROLogger logger = new MROLogger(_info);
                        string sDescription = "Admin with ID: " + nAdminUserID + " called Edit Fields Method for Facility ID: " + nFacilityID + " and Updated Fields";
                        logger.LogAdminRecords(nAdminUserID, sDescription, "Edit Fields", "Edit Fields");
                    }
                    #endregion

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
