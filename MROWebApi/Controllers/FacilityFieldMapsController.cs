using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MRODBL.Entities;
using MRODBL.BaseClassRepositories;
using MRODBL.BaseClasses;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
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

        [HttpGet("GetFieldsByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFieldsByFacilityID(int facilityID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                IEnumerable<dynamic> fields = await fieldsRepository.EditFields(facilityID);
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(facilityID);
                if (facility == null)
                    return NotFound();
                var faciName = facility.sFacilityName;
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
        public async Task<IActionResult> EditFacilityFields([FromBody]FacilityFieldMaps[] fieldFacilityMaps)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    foreach (FacilityFieldMaps map in fieldFacilityMaps)
                    {
                        map.dtCreated = DateTime.Now;
                        map.dtLastUpdate = DateTime.Now;
                    }
                    FacilityFieldMapsRepository facilityFeldMapsRepository = new FacilityFieldMapsRepository(_info);
                    return await facilityFeldMapsRepository.UpdateMany(fieldFacilityMaps.ToList()) ? Ok() : (IActionResult)NoContent();
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
