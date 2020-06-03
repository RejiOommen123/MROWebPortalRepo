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
    public class FacilityLocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        #region Facility Location Constructor
        public FacilityLocationController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Facility Location
        // GET: api/Facility Location
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<lnkROIFacilityLocations>>> GetFacilityLocation()
        {
            return await _context.lnkROIFacilityLocations.ToListAsync();
        }

        // GET: api/FacilityLocation/5
        [HttpGet("GetFacilityLocation/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilityLocations>> GetFacilityLocation(string id)
        {
            var facilityLocation = await _context.lnkROIFacilityLocations.FindAsync(int.Parse(id));

            if (facilityLocation == null)
            {
                return NotFound();
            }

            return facilityLocation;
        }


        [HttpGet("GetFacilityLocationByFacilityID/{facilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityLocationByFacilityID(int facilityID)
        {
            try
            {
                var locations = (from fln in _context.lnkROIFacilityLocations
                                 join f in _context.tblROIFacilities
                               on fln.nROIFacilityID.ToString() equals f.nROIFacilityID.ToString()
                               where fln.nROIFacilityID == facilityID
                               select new
                               {
                                   LocationID = fln.nLocationID,
                                   LocationCode = fln.sLocationCode,
                                   LocationName = fln.sLocationName,
                                   LocationAddress = fln.sLocationAddress,
                                   PhoneNo = fln.nPhoneNo,
                                   FaxNo = fln.nFaxNo,
                                   FacilityId = fln.nROIFacilityID,
                                   FacilityName = f.sFacilityName,
                                   FacilityDescription = f.sDescription,

                               }).ToList();
                lnkROIFacilityLocations falo = _context.lnkROIFacilityLocations.Where(c => c.nROIFacilityID == facilityID).FirstOrDefault();
                var faloName = falo.nROIFacility.sFacilityName;
                return Ok(new { locations, faloName });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Add Facility Location
        // POST: api/FacilityLocation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilityLocations>> AddFacilityLocation(lnkROIFacilityLocations facilityLocation)
        {
            try
            {
                //Data which not present in data coming from UI
                facilityLocation.sCreatedBy = 1;
                facilityLocation.dtCreatedDate = DateTime.Now;
                facilityLocation.sUpdatedBy = 1;
                facilityLocation.dtUpdatedDate = DateTime.Now;

                _context.lnkROIFacilityLocations.Add(facilityLocation);

                await _context.SaveChangesAsync();
                int id = facilityLocation.nLocationID;

                _context.SaveChanges();
                //TODO: suceess should be an enum
                return Ok("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Edit Facility Location
        // PUT: api/FacilityLocation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditFacilityLocation/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacilityLocation(int id, lnkROIFacilityLocations facilityLocation)
        {
            try
            {
                if (id != facilityLocation.nLocationID)
                {
                    return BadRequest();
                }
                facilityLocation.sUpdatedBy = 1;
                facilityLocation.dtUpdatedDate = DateTime.Now;
                _context.Entry(facilityLocation).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FacilityLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }
        #endregion

        #region Remove Facility Location
        // DELETE: api/FacilityLocation/
        [HttpPost("DeleteFacilityLocation")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<lnkROIFacilityLocations>> DeleteFacilityLocation([FromBody] int id)
        {
            try
            {
                lnkROIFacilityLocations facilityLocation = await _context.lnkROIFacilityLocations.FindAsync(id);
                if (id != facilityLocation.nROIFacilityID)
                {
                    return BadRequest();
                }
                _context.Remove(facilityLocation);


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FacilityLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return NoContent();
        }
        #endregion

        #region Facility Location Exisit
        //To check the Facility Location Exisit
        private bool FacilityLocationExists(int id)
        {
            return _context.lnkROIFacilityLocations.Any(e => e.nLocationID == id);
        }
        #endregion

    }
}
