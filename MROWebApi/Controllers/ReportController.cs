using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [APIKeyAuth]
    public class ReportController : ControllerBase
    {
        #region Report Constructor
        private readonly DBConnectionInfo _info;
        public ReportController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Get Audit Report
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAuditReport(AuditFilterParameter auditFilterParameter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Data Addition ! from UI
                    AdminModuleLoggerRepository amFac = new AdminModuleLoggerRepository(_info);
                    if (!String.IsNullOrWhiteSpace(auditFilterParameter.sEnd)) { 
                        DateTime endDate = Convert.ToDateTime(auditFilterParameter.sEnd).AddHours(23).AddMinutes(59).AddSeconds(59);
                        auditFilterParameter.sEnd = endDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    IEnumerable<dynamic> records = await amFac.GetAuditData(auditFilterParameter.sStart, auditFilterParameter.sEnd, (String.IsNullOrWhiteSpace(auditFilterParameter.sFacilityName) ? null : auditFilterParameter.sFacilityName), (String.IsNullOrWhiteSpace(auditFilterParameter.sLocationName) ? null : auditFilterParameter.sLocationName), (String.IsNullOrWhiteSpace(auditFilterParameter.sAdminName) ? null : auditFilterParameter.sAdminName));
                    #endregion

                    return Ok(records );
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion

        #region Get Facility/Location Report
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetFacilityLocationReport(AuditFilterParameter auditFilterParameter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Data Addition ! from UI
                    AdminModuleLoggerRepository amFac = new AdminModuleLoggerRepository(_info);
                    if (!String.IsNullOrWhiteSpace(auditFilterParameter.sEnd))
                    {
                        DateTime endDate = Convert.ToDateTime(auditFilterParameter.sEnd).AddHours(23).AddMinutes(59).AddSeconds(59);
                        auditFilterParameter.sEnd = endDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    IEnumerable<dynamic> records = await amFac.GetFacilityLocationData(auditFilterParameter.sStart, auditFilterParameter.sEnd, (String.IsNullOrWhiteSpace(auditFilterParameter.sFacilityName) ? null : auditFilterParameter.sFacilityName), (String.IsNullOrWhiteSpace(auditFilterParameter.sLocationName) ? null : auditFilterParameter.sLocationName));
                    #endregion

                    return Ok(records);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        #endregion
    }
}
