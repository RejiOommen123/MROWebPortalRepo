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
    public class FacilitySensitiveInfoController : ControllerBase
    {
        #region Facility Sensitive Info Constructor
        private readonly ApplicationDbContext _context;
        public FacilitySensitiveInfoController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Sensitive Info
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<lnkROIFacilitySensitiveInfo>>> GetFacilitySensitiveInfo()
        {
            return await _context.lnkROIFacilitySensitiveInfo.ToListAsync();
        }

        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilitySensitiveInfo>> GetFacilitySensitiveInfo(string id)
        {
            var facilitySensitiveInfo = await _context.lnkROIFacilitySensitiveInfo.FindAsync(int.Parse(id));

            if (facilitySensitiveInfo == null)
            {
                return NotFound();
            }

            return facilitySensitiveInfo;
        }

        [HttpGet("GetFacilitySensitiveInfoByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilitySensitiveInfoByFacilityID(int facilityID)
        {
            try
            {
                var primaryReasons = (from fpr in _context.lnkROIFacilitySensitiveInfo
                                      join f in _context.tblROIFacilities
                                      on fpr.nROIFacilityID.ToString() equals f.nROIFacilityID.ToString()
                                      where fpr.nROIFacilityID == facilityID
                                      select new
                                      {
                                          FacilitySensitiveInfoID = fpr.nROIFacilitySensitiveInfoID,
                                          SensitiveInfoID = fpr.nSensitiveInfoID,
                                          SensitiveInfoName = fpr.sSensitiveInfoName,
                                          FacilityId = fpr.nROIFacilityID,
                                          FacilityName = f.sFacilityName,
                                          FacilityDescription = f.sDescription,

                                      }).ToList();
                lnkROIFacilitySensitiveInfo falo = _context.lnkROIFacilitySensitiveInfo.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
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
