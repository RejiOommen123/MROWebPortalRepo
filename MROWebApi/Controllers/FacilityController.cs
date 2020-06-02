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

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FacilityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        #region Facility Constructor
        public FacilityController(ApplicationDbContext context)
        {
            _context = context;
        } 
        #endregion

        #region Get Facility
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
        #endregion

        #region Add Facility
        // POST: api/Facility
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<tblROIFacilities>> AddFacility(tblROIFacilities facility)
        {
            try
            {
                //Data which not present in data coming from UI
                facility.bActiveStatus = true;
                facility.sCreatedBy = 1;
                facility.dtCreatedDate = DateTime.Now;
                facility.sUpdatedBy = 1;
                facility.dtUpdatedDate = DateTime.Now;
                //TODO: Adding sConfigShowFields, sConfigShowWizard and Logo Image is pending
                //facility.sConfigShowFields = "test data";
                _context.tblROIFacilities.Add(facility);

                await _context.SaveChangesAsync();
                int id = facility.nROIFacilityID;

                //Add data in FacilityFieldMap table for the current Facility
                List<lnkROIFacilityFieldMaps> facilityFieldMaps = new List<lnkROIFacilityFieldMaps>();
                foreach (lstFields field in _context.lstFields)
                {
                    facilityFieldMaps.Add(new lnkROIFacilityFieldMaps
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
                _context.AddRange(facilityFieldMaps);

                //Add data in FacilityPrimaryReasons table for the current Facility
                List<lnkROIFacilityPrimaryReasons> facilityPrimaryReasons = new List<lnkROIFacilityPrimaryReasons>();
                foreach (lstPrimaryReasons field in _context.lstPrimaryReasons)
                {
                    facilityPrimaryReasons.Add(new lnkROIFacilityPrimaryReasons
                    {
                        nPrimaryReasonID = field.nPrimaryReasonID,
                        nROIFacilityID = id,
                        sPrimaryReasonName = field.sPrimaryReasonName,
                        sCreatedBy = 1,
                        dtCreatedDate = DateTime.Now,
                        sUpdatedBy = 1,
                        dtUpdatedDate = DateTime.Now
                    });
                }
                _context.AddRange(facilityPrimaryReasons);


                //Add data in FacilityRecordTypes table for the current Facility
                List<lnkROIFacilityRecordTypes> facilityRecordTypes = new List<lnkROIFacilityRecordTypes>();
                foreach (lstRecordTypes field in _context.lstRecordTypes)
                {
                    facilityRecordTypes.Add(new lnkROIFacilityRecordTypes
                    {
                        nRecordTypeID = field.nRecordTypeID,
                        nROIFacilityID = id,
                        sRecordTypeName = field.sRecordTypeName,
                        sCreatedBy = 1,
                        dtCreatedDate = DateTime.Now,
                        sUpdatedBy = 1,
                        dtUpdatedDate = DateTime.Now
                    });
                }
                _context.AddRange(facilityRecordTypes);

                //Add data in FacilitySensitiveInfo table for the current Facility
                List<lnkROIFacilitySensitiveInfo> facilitySensitiveInfo = new List<lnkROIFacilitySensitiveInfo>();
                foreach (lstSensitiveInfo field in _context.lstSensitiveInfo)
                {
                    facilitySensitiveInfo.Add(new lnkROIFacilitySensitiveInfo
                    {
                        nSensitiveInfoID = field.nSensitiveInfoID,
                        nROIFacilityID = id,
                        sSensitiveInfoName = field.sSensitiveInfoName,
                        sCreatedBy = 1,
                        dtCreatedDate = DateTime.Now,
                        sUpdatedBy = 1,
                        dtUpdatedDate = DateTime.Now
                    });
                }
                _context.AddRange(facilitySensitiveInfo);
                _context.SaveChanges();
                //TODO: suceess should be an enum
                return Ok("success");
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
        public async Task<IActionResult> EditFacility(int id, tblROIFacilities facility)
        {
            try
            {
                if (id != facility.nROIFacilityID)
                {
                    return BadRequest();
                }

                _context.Entry(facility).State = EntityState.Modified;

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
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }
        #endregion

        #region Remove Facility
        // DELETE: api/Facility/
        [HttpPost("DeleteFacility")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<tblROIFacilities>> DeleteFacility([FromBody] int id)
        {
            try
            {
                tblROIFacilities facility = await _context.tblROIFacilities.FindAsync(id);
                if (id != facility.nROIFacilityID)
                {
                    return BadRequest();
                }
                facility.bActiveStatus = false;
                _context.Entry(facility).State = EntityState.Modified;


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FacilityExists(id))
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

        #region Facility Exisit
        //To check the Facility Exisit
        private bool FacilityExists(int id)
        {
            return _context.tblROIFacilities.Any(e => e.nROIFacilityID == id);
        }
        #endregion

       
    }
}
