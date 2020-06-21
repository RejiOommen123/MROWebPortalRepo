using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstMigration.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MROWebApi.Context;
using MRODBL.Entities;
using MRODBL.BaseClassRepositories;
using MRODBL.BaseClasses;
using Microsoft.Extensions.Logging.Abstractions;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityFieldMapsController : ControllerBase
    {
        private readonly DBConnectionInfo _info;

        #region Facility Constructor
        public FacilityFieldMapsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Facility Fields
        // GET: api/Facility
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IEnumerable<FacilityFieldMaps>> GetFacilityFieldMaps()
        {
            FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
            return await facilityFeldMapsRepository.GetAllASC(1000, "nFacilityFieldMapID");
        }

        // GET: api/Facility/5
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

        //
        // Get Facility Fields By FacilityID
        //
        [HttpGet("GetFieldsByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFieldsByFacilityID(int facilityID)
        {
            try
            {
                //FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
                //IEnumerable<FacilityFieldMaps> facilityFeldMapsList = await facilityFeldMapsRepository.GetAll(1000, "nFacilityFieldMapID");

                //FacilityPrimaryReasonsRepository facilityPrimaryReasonsRepository = new FacilityPrimaryReasonsRepository(_info);
                //IEnumerable<FacilityPrimaryReasons> primaryReasons = await facilityPrimaryReasonsRepository.GetAllASC(1000, "nFacilityPrimaryReasonID");
                //primaryReasons = primaryReasons.Where(c=>c.nFacilityID==facilityID);

                //FacilityRecordTypesRepository facilityRecordTypesRepository = new FacilityRecordTypesRepository(_info);
                //IEnumerable<FacilityRecordTypes> recordTypes = await facilityRecordTypesRepository.GetAllASC(1000, "nFacilityRecordTypeID");
                //recordTypes = recordTypes.Where(c => c.nFacilityID == facilityID);

                //FacilitySensitiveInfoRepository facilitySensitiveInfoRepository = new FacilitySensitiveInfoRepository(_info);
                //IEnumerable<FacilitySensitiveInfo> sensitiveInfo = await facilitySensitiveInfoRepository.GetAllASC(1000, "nFacilitySensitiveInfoID");
                //sensitiveInfo = sensitiveInfo.Where(c => c.nFacilityID == facilityID);

                //FacilityShipmentTypesRepository facilityShipmentTypesRepository = new FacilityShipmentTypesRepository(_info);
                //IEnumerable<FacilityShipmentTypes> shipmentTypes = await facilityShipmentTypesRepository.GetAllASC(1000, "nFacilityShipmentTypeID");
                //shipmentTypes = shipmentTypes.Where(c => c.nFacilityID == facilityID);

                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                //IEnumerable<Fields> fieldsList = await fieldsRepository.GetAll(1000, "nFieldID");
                IEnumerable<dynamic> fields = await fieldsRepository.EditFields(facilityID);

                //fields = fields.Where(c=>c.nFacilityID == facilityID).ToList();

                //var ie2 = fields.Select(x => new {primaryReasons,recordTypes,sensitiveInfo,shipmentTypes });
                //fields.ToList().Add(primaryReasons);
                //fields.ToList().Add(recordTypes);
                //fields.ToList().Add(sensitiveInfo);
                //fields.ToList().Add(shipmentTypes);
                //List<string> fetchFields = new List<string>();
                //fetchFields.Add("nFacilityFieldMapID");
                //fetchFields.Add("nFieldID");
                //fetchFields.Add("nFacilityID");
                //fetchFields.Add("bShow");
                //fetchFields.Add("sFieldName");
                //var fieldsPass = $"new ({string.Join(",", fetchFields)})";
                //fields = fields.AsQueryable().Select(returnFields);
                //var fields = (from fcm in facilityFeldMapsList
                //              join f in fieldsList
                //              on fcm.nFieldID.ToString() equals f.nFieldID.ToString()
                //              where fcm.nFacilityID == facilityID
                //              select new
                //              {
                //                  nFacilityFieldMapID = fcm.nFacilityFieldMapID,
                //                  nFieldID = fcm.nFieldID,
                //                  nFacilityID = fcm.nFacilityID,
                //                  bShow = fcm.bShow,
                //                  sFieldName = f.sFieldName,
                //              }).ToList();
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(facilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
                return Ok(new { fields, faciName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Facility Fields Edit

        // Edit Fields Fields Facility Maps
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityFields([FromBody]FacilityFieldMaps[] fieldFacilityMaps)
        {
            try
            {
                foreach (FacilityFieldMaps map in fieldFacilityMaps) {
                    map.dtCreated = DateTime.Now;
                    map.dtLastUpdate = DateTime.Now;
                }
                FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
                return await facilityFeldMapsRepository.UpdateMany(fieldFacilityMaps.ToList()) ? Ok() : (IActionResult)NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
