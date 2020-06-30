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
    public class FacilityRecordTypesController : ControllerBase
    {
        #region Facility Record Type Constructor
        private readonly ApplicationDbContext _context;

        public FacilityRecordTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Record Types 
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<lnkROIFacilityRecordTypes>>> GetFacilityRecordTypes()
        {
            return await _context.lnkROIFacilityRecordTypes.ToListAsync();
        }

        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilityRecordTypes>> GetFacilityRecordTypes(string id)
        {
            var facilityRecordTypes = await _context.lnkROIFacilityRecordTypes.FindAsync(int.Parse(id));

            if (facilityRecordTypes == null)
            {
                return NotFound();
            }

            return facilityRecordTypes;
        }

        [HttpGet("GetFacilityRecordTypesByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityRecordTypesByFacilityID(int facilityID)
        {
            try
            {
                var primaryReasons = (from fpr in _context.lnkROIFacilityRecordTypes
                                      join f in _context.tblROIFacilities
                                      on fpr.nROIFacilityID.ToString() equals f.nROIFacilityID.ToString()
                                      where fpr.nROIFacilityID == facilityID
                                      select new
                                      {
                                          FacilityRecordTypeID = fpr.nROIFacilityRecordTypeID,
                                          RecordTypeID = fpr.nRecordTypeID,
                                          RecordTypeName = fpr.sRecordTypeName,
                                          FacilityId = fpr.nROIFacilityID,
                                          FacilityName = f.sFacilityName,
                                          FacilityDescription = f.sDescription,

                                      }).ToList();
                lnkROIFacilityRecordTypes falo = _context.lnkROIFacilityRecordTypes.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
                var faprName = falo.nROIFacility.sFacilityName;
                return Ok(new { primaryReasons, faprName });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
