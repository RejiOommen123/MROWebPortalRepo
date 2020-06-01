using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstMigration.Context;
using MROWebAPI.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MROWebApi.Context;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Facility
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<tblROIFacilities>>> GetFacility()
        {
            return await _context.tblROIFacilities.ToListAsync();
        }

        // GET: api/Facility/5
        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<tblROIFacilities>> GetFacility(string id)
        {
            var facility = await _context.tblROIFacilities.FindAsync(int.Parse(id));

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
        }

        // PUT: api/Facility/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFacility(int id, tblROIFacilities facility)
        {
            if (id != facility.nROIFacilityID)
            {
                return BadRequest();
            }

            _context.Entry(facility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Facility
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<tblROIFacilities>> AddFacility(tblROIFacilities facility)
        {
            _context.tblROIFacilities.Add(facility);
            await _context.SaveChangesAsync();
            int id = facility.nROIFacilityID;
            List<lnkROIFacilityFieldMaps> fieldFacilityMaps= new List<lnkROIFacilityFieldMaps>();
            foreach (lstFields field in _context.lstFields)
            {
                fieldFacilityMaps.Add(new lnkROIFacilityFieldMaps
                {
                    nFieldID = field.nFieldID,
                    nROIFacilityID = id,
                    bShow = true,
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                });
            }
            _context.UpdateRange(fieldFacilityMaps);

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
            _context.SaveChanges();
            return Ok("success");
        }

        // DELETE: api/Facility/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<tblROIFacilities>> DeleteFacility(int id)
        {
            var facility = await _context.tblROIFacilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            _context.tblROIFacilities.Remove(facility);
            await _context.SaveChangesAsync();

            return facility;
        }

        private bool FacilityExists(int id)
        {
            return _context.tblROIFacilities.Any(e => e.nROIFacilityID == id);
        }
        [HttpGet("EditFields/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields(int id)
        {
           var wizards= (from fcm in _context.lnkROIFacilityFieldMaps
                         join f in _context.lstFields
                         on fcm.nFieldID.ToString() equals f.nFieldID.ToString()
                         where fcm.nROIFacilityID == id
                         select new
                         {
                             FieldFacilityMapId = fcm.nROIFacilityFieldMapID,
                             FieldId = fcm.nFieldID,
                             FacilityId = fcm.nROIFacilityID,
                             IsEnable = fcm.bShow,
                             FieldName = f.sFieldName,                         
                         }).ToList();
            tblROIFacilities faci = _context.tblROIFacilities.Where(c => c.nROIFacilityID == id).FirstOrDefault();
            var faciName = faci.sFacilityName;
            return Ok(new { wizards, faciName });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields([FromBody]lnkROIFacilityFieldMaps[] fieldFacilityMaps)
        {
            _context.UpdateRange(fieldFacilityMaps);
            _context.SaveChanges();
            return Ok();
        }
    }
}
