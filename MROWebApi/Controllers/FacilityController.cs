using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MRODBL.Entities;
using MRODBL.BaseClassRepositories;
using MRODBL.BaseClasses;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityController : ControllerBase
    {
        private readonly DBConnectionInfo _info;

        #region Facility Constructor
        public FacilityController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility
        // GET: api/Facility
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacility()
        {
            #region Dapper Code Get 1000 Rows Descending Order
            try {
                //throw new Exception();
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                IEnumerable<Facilities> facilities = await rpFac.GetAllDESC(1000, "nROIFacilityID");
                FacilityLocationsRepository facilityLocationsRepository = new FacilityLocationsRepository(_info);
                //IList<int> nFacLocCount = new List<int>();
                foreach (Facilities fac in facilities)
                {
                    fac.nFacLocCount = await facilityLocationsRepository.CountWhere("nFacilityID", fac.nFacilityID);
                    //nFacLocCount.Add(await facilityLocationsRepository.CountWhere("nFacilityID", fac.nFacilityID));
                }
                //var facilitiesList = facilities.Select(x=>new {facilities,nFacLocCount });
                
                return Ok(facilities);
            }
            catch (Exception exp) {
                return Content(exp.Message);
            }
            #endregion
        }

        // GET: api/Facility/5
        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> GetFacility(string id)
        {
            #region Dapper Code Get Single Facility
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            bool result = int.TryParse(id, out int nId);
            Facilities facility = await rpFac.Select(nId);
            if (facility == null)
                return NotFound();
            return facility;
            #endregion
        }
        #endregion

        #region Add Facility
        // POST: api/Facility
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> AddFacility(Facilities facility)
        {
            try
            {
                #region Data Addition !UI
                //Data which not present in data coming from UI
                //Initially false
                facility.bActiveStatus = false;
                facility.nUpdatedAdminUserID = 1;
                facility.dtCreated = DateTime.Now;
                facility.nCreatedAdminUserID = 1;
                facility.nUpdatedAdminUserID = 1;
                facility.dtLastUpdate = DateTime.Now;
                facility.sConfigShowFields = "T Data";
                facility.sConfigShowWizards = "T Data";
                facility.nFacLocCount = 0;
                #endregion
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                int GeneratedID = (int)rpFac.Insert(facility);
                Facilities dbFacility = await rpFac.Select(GeneratedID);

                FieldsRepository fRep = new FieldsRepository(_info);
                IEnumerable<Fields> fieldList = await fRep.GetAllASC(1000, "nFieldID");
                FacilityFieldMapsRepository fmRepo = new FacilityFieldMapsRepository(_info);

                //Add data in FacilityFieldMap table for the current Facility
                List<FacilityFieldMaps> facilityFieldMaps = new List<FacilityFieldMaps>();
                foreach (Fields field in fieldList)
                {
                    facilityFieldMaps.Add(new FacilityFieldMaps
                    {
                        nFieldID = field.nFieldID,
                        nFacilityID = GeneratedID,
                        bShow = true,
                        nWizardID = field.nWizardID,
                        nCreatedAdminUserID = 1,
                        dtCreated = DateTime.Now,
                        nUpdatedAdminUserID = 1,
                        dtLastUpdate = DateTime.Now,
                        sTableName = "lstFields"
                        
                    });
                }
                //Primary Reason
                PrimaryReasonsRepository prRepo = new PrimaryReasonsRepository(_info);
                IEnumerable<PrimaryReasons> prList = await prRepo.GetAllASC(1000, "nPrimaryReasonID");
                foreach (PrimaryReasons primaryReason in prList)
                {
                    facilityFieldMaps.Add(new FacilityFieldMaps
                    {
                        nFieldID = primaryReason.nPrimaryReasonID,
                        nFacilityID = GeneratedID,
                        bShow = true,
                        nWizardID = primaryReason.nWizardID,
                        nCreatedAdminUserID = 1,
                        dtCreated = DateTime.Now,
                        nUpdatedAdminUserID = 1,
                        dtLastUpdate = DateTime.Now,
                        sTableName = "lstPrimaryReasons"
                    });
                }

                //Record Type
                RecordTypesRepository rTypeRepo = new RecordTypesRepository(_info);
                IEnumerable<RecordTypes> rTypeList = await rTypeRepo.GetAllASC(1000, "nRecordTypeID");
                foreach (RecordTypes recordType in rTypeList)
                {
                    facilityFieldMaps.Add(new FacilityFieldMaps
                    {
                        nFieldID = recordType.nRecordTypeID,
                        nFacilityID = GeneratedID,
                        bShow = true,
                        nWizardID = recordType.nWizardID,
                        nCreatedAdminUserID = 1,
                        dtCreated = DateTime.Now,
                        nUpdatedAdminUserID = 1,
                        dtLastUpdate = DateTime.Now,
                        sTableName = "lstRecordTypes"
                    });
                }

                //Sensitive Info
                SensitiveInfoRepository sIRepo = new SensitiveInfoRepository(_info);
                IEnumerable<SensitiveInfo> sInfoList = await sIRepo.GetAllASC(1000, "nSensitiveInfoID");
                foreach (SensitiveInfo sensitiveInfo in sInfoList)
                {
                    facilityFieldMaps.Add(new FacilityFieldMaps
                    {
                        nFieldID = sensitiveInfo.nSensitiveInfoID,
                        nFacilityID = GeneratedID,
                        bShow = true,
                        nWizardID = sensitiveInfo.nWizardID,
                        nCreatedAdminUserID = 1,
                        dtCreated = DateTime.Now,
                        nUpdatedAdminUserID = 1,
                        dtLastUpdate = DateTime.Now,
                        sTableName = "lstSensitiveInfo"
                    });
                }

                //Shipment Type

                ShipmentTypesRepository sTRepo = new ShipmentTypesRepository(_info);
                IEnumerable<ShipmentTypes> sTList = await sTRepo.GetAllASC(1000, "nShipmentTypeID");
                foreach (ShipmentTypes shipmentType in sTList)
                {
                    facilityFieldMaps.Add(new FacilityFieldMaps
                    {
                        nFieldID = shipmentType.nShipmentTypeID,
                        nFacilityID = GeneratedID,
                        bShow = true,
                        nWizardID = shipmentType.nWizardID,
                        nCreatedAdminUserID = 1,
                        dtCreated = DateTime.Now,
                        nUpdatedAdminUserID = 1,
                        dtLastUpdate = DateTime.Now,
                        sTableName = "lstShipmentTypes"
                    });
                }

                await fmRepo.InsertMany(facilityFieldMaps);

                //Add data in FacilityPrimaryReasons table for the current Facility
                //FacilityPrimaryReasonsRepository rPRRepo = new FacilityPrimaryReasonsRepository(_info);
                //List<FacilityFieldMaps> facilityPrimaryReasonsMap = new List<FacilityFieldMaps>();
                //foreach (PrimaryReasons pReason in prList)
                //{
                //    facilityPrimaryReasons.Add(new FacilityPrimaryReasons
                //    {
                //        nPrimaryReasonID = pReason.nPrimaryReasonID,
                //        nFacilityID = GeneratedID,
                //        sPrimaryReasonName = pReason.sPrimaryReasonName,
                //        dtLastUpdate = DateTime.Now
                //    });
                //}
                //await fmRepo.InsertMany(facilityPrimaryReasons);

                //Add data in FacilityRecordTypes table for the current Facility
                
                //FacilityRecordTypesRepository facilityRecordTypeRep = new FacilityRecordTypesRepository(_info);
                //List<FacilityRecordTypes> facilityRecordTypes = new List<FacilityRecordTypes>();

                //foreach (RecordTypes recordT in rTypeList)
                //{
                //    facilityRecordTypes.Add(new FacilityRecordTypes
                //    {
                //        nRecordTypeID = recordT.nRecordTypeID,
                //        nFacilityID = GeneratedID,
                //        sRecordTypeName = recordT.sRecordTypeName,
                //        dtLastUpdate = DateTime.Now
                //    });
                //}
                //await facilityRecordTypeRep.InsertMany(facilityRecordTypes);

                //SensitiveInfoRepository sIRepo = new SensitiveInfoRepository(_info);
                //IEnumerable<SensitiveInfo> sInfoList = await sIRepo.GetAllASC(1000, "nSensitiveInfoID");
                ////Add data in FacilitySensitiveInfo table for the current Facility
                //FacilitySensitiveInfoRepository facilitySensitiveInfoRepo = new FacilitySensitiveInfoRepository(_info);
                //List<FacilitySensitiveInfo> facilitySensitiveInfo = new List<FacilitySensitiveInfo>();
                //foreach (SensitiveInfo sInfo in sInfoList)
                //{
                //    facilitySensitiveInfo.Add(new FacilitySensitiveInfo
                //    {
                //        nSensitiveInfoID = sInfo.nSensitiveInfoID,
                //        nFacilityID = GeneratedID,
                //        sSensitiveInfoName = sInfo.sSensitiveInfoName,
                //        dtLastUpdate = DateTime.Now
                //    });
                //}
                //await facilitySensitiveInfoRepo.InsertMany(facilitySensitiveInfo);

                //ShipmentTypesRepository sTRepo = new ShipmentTypesRepository(_info);
                //IEnumerable<ShipmentTypes> sTList = await sTRepo.GetAllASC(1000, "nShipmentTypeID");
                //FacilityShipmentTypesRepository facilityShipmentTypesRepo = new FacilityShipmentTypesRepository(_info);
                //List<FacilityShipmentTypes> facilityShipmentTypes = new List<FacilityShipmentTypes>();
                //foreach (ShipmentTypes shipmentTypes in sTList) {
                //    facilityShipmentTypes.Add(new FacilityShipmentTypes
                //    {
                //        nShipmentTypeID = shipmentTypes.nShipmentTypeID,
                //        nFacilityID = GeneratedID,
                //        sShipmentTypeName = shipmentTypes.sShipmentTypeName,
                //        dtLastUpdate = DateTime.Now
                //    }); 
                    
                //}
                //await facilityShipmentTypesRepo.InsertMany(facilityShipmentTypes);
                //TODO: suceess should be an enum
                return dbFacility;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #region //TODO:commented for future use
            //List<WizardFacilityMap> wizardFacilityMaps = new List<WizardFacilityMap>();
            //foreach (Wizard wizard in _context.Wizard)
            //{
            //    wizardFacilityMaps.Add(new WizardFacilityMap
            //    {
            //        WizardId = wizard.WizardId,
            //        FacilityId = id,
            //        IsEnable = true
            //    });
            //}
            //_context.UpdateRange(wizardFacilityMaps);
            #endregion
        }
        #endregion

        #region Edit Facility
        // PUT: api/Facility/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<Facilities> EditFacility(int id, Facilities facility)
        {
            if (id != facility.nFacilityID)
            {
                return BadRequest();
            }
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            if (rpFac.Update(facility))
            { return NoContent(); }
            else
            { return NotFound(); }
        }
        #endregion

        #region Remove Facility
        // DELETE: api/Facility/
        [HttpPost("DeleteFacility")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facilities>> DeleteFacility([FromBody] int id)
        {

            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities facilityDB = await rpFac.Select(id);
            if (id != facilityDB.nFacilityID)
            {
                return BadRequest();
            }
            if (await rpFac.ToggleSoftDelete("bActiveStatus", id))
            { return NoContent(); }
            else
            { return NotFound(); }
        }
        #endregion

        #region Facility Exist
        //To check the Facility Exisit
        private async Task<bool> FacilityExistsAsync(int id)
        {
            ///EF Core Code
            //return _context.tblROIFacilities.Any(e => e.nROIFacilityID == id);
            ///Dapper Code
            FacilitiesRepository rpFac = new FacilitiesRepository(_info);
            Facilities f = await rpFac.Select(id);
            return f == null;
        }
        #endregion
    }
}
