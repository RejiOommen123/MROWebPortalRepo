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

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityFieldMapsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        #region Facility Constructor
        public FacilityFieldMapsController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Facility Fields
        // GET: api/Facility
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<lnkROIFacilityFieldMaps>>> GetFacilityFieldMaps()
        {
            return await _context.lnkROIFacilityFieldMaps.ToListAsync();
        }

        // GET: api/Facility/5
        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<tblROIFacilities>> GetFacilityFieldMaps(string id)
        {
            var facility = await _context.tblROIFacilities.FindAsync(int.Parse(id));

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
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
                var fields = (from fcm in _context.lnkROIFacilityFieldMaps
                               join f in _context.lstFields
                               on fcm.nFieldID.ToString() equals f.nFieldID.ToString()
                               where fcm.nROIFacilityID == facilityID
                               select new
                               {
                                   FieldFacilityMapId = fcm.nROIFacilityFieldMapID,
                                   FieldId = fcm.nFieldID,
                                   FacilityId = fcm.nROIFacilityID,
                                   IsEnable = fcm.bShow,
                                   FieldName = f.sFieldName,
                               }).ToList();
                tblROIFacilities faci = _context.tblROIFacilities.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
                var faciName = faci.sFacilityName;
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
        public async Task<IActionResult> EditFacilityFields([FromBody]lnkROIFacilityFieldMaps[] fieldFacilityMaps)
        {
            try
            {
                _context.UpdateRange(fieldFacilityMaps);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




    }
}
