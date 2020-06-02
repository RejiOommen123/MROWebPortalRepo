using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class FacilityPrimaryReasonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        #region Facility Constructor
        public FacilityPrimaryReasonsController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Facility Primary Reasons
        // GET: api/FacilityPrimaryReasons
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<lnkROIFacilityPrimaryReasons>>> GetFacilityPrimaryReasons()
        {
            return await _context.lnkROIFacilityPrimaryReasons.ToListAsync();
        }

        // GET: api/Facility/5
        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilityPrimaryReasons>> GetFacilityPrimaryReasons(string id)
        {
            var facilityPrimaryReasons = await _context.lnkROIFacilityPrimaryReasons.FindAsync(int.Parse(id));

            if (facilityPrimaryReasons == null)
            {
                return NotFound();
            }

            return facilityPrimaryReasons;
        }

        [HttpGet("GetFacilityPrimaryReasonsByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityPrimaryReasonsByFacilityID(int facilityID)
        {
            try
            {
                var primaryReasons = (from fpr in _context.lnkROIFacilityPrimaryReasons
                                 join f in _context.tblROIFacilities
                                 on fpr.nROIFacilityID.ToString() equals f.nROIFacilityID.ToString()
                                 where fpr.nROIFacilityID == facilityID
                                 select new
                                 {
                                     FacilityPrimaryReasonID = fpr.nROIFacilityPrimaryReasonID,
                                     PrimaryReasonID = fpr.nPrimaryReasonID,
                                     PrimaryReasonName = fpr.sPrimaryReasonName,
                                     FacilityId = fpr.nROIFacilityID,
                                     FacilityName = f.sFacilityName,
                                     FacilityDescription = f.sDescription,

                                 }).ToList();
                lnkROIFacilityPrimaryReasons falo = _context.lnkROIFacilityPrimaryReasons.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
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
