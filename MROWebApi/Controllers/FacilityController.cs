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
        public async Task<ActionResult<IEnumerable<Facility>>> GetFacility()
        {
            return await _context.Facility.ToListAsync();
        }

        // GET: api/Facility/5
        [HttpGet("GetFacility/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Facility>> GetFacility(string id)
        {
            var facility = await _context.Facility.FindAsync(int.Parse(id));

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
        public async Task<IActionResult> EditFacility(int id, Facility facility)
        {
            if (id != facility.FacilityId)
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
        public async Task<ActionResult<Facility>> AddFacility(Facility facility)
        {
            _context.Facility.Add(facility);
            await _context.SaveChangesAsync();
            int id = facility.FacilityId;
            List<FieldFacilityMap> fieldFacilityMaps= new List<FieldFacilityMap>();
            foreach (Field field in _context.Field)
            {
                fieldFacilityMaps.Add(new FieldFacilityMap
                {
                    FieldId = field.FieldId,
                    WizardId = field.WizardId,
                    FacilityId = id,
                    IsEnable = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedDate = DateTime.Now
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
        public async Task<ActionResult<Facility>> DeleteFacility(int id)
        {
            var facility = await _context.Facility.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            _context.Facility.Remove(facility);
            await _context.SaveChangesAsync();

            return facility;
        }

        private bool FacilityExists(int id)
        {
            return _context.Facility.Any(e => e.FacilityId == id);
        }
        [HttpGet("EditFields/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields(int id)
        {
           var wizards= (from fcm in _context.FieldFacilityMap
                         join f in _context.Field
                         on fcm.FieldId.ToString() equals f.FieldId.ToString()
                         where fcm.FacilityId == id
                         select new
                         {
                             FieldFacilityMapId = fcm.FieldFacilityMapId,
                             FieldId = fcm.FieldId,
                             FacilityId = fcm.FacilityId,
                             IsEnable = fcm.IsEnable,
                             FieldName = f.FieldName,
                             WizardId = f.WizardId                            
                         }).ToList();
            Facility faci = _context.Facility.Where(c => c.FacilityId == id).FirstOrDefault();
            var faciName = faci.FacilityName;
            return Ok(new { wizards, faciName });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields([FromBody]FieldFacilityMap[] fieldFacilityMaps)
        {
            _context.UpdateRange(fieldFacilityMaps);
            _context.SaveChanges();
            return Ok();
        }
    }
}
