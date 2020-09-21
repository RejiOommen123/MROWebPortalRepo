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
                    IEnumerable<RecordTypes> dbRecordTypes = await amFac.SelectWhere("sRecordTypeName", recordType.sRecordTypeName);
                    #endregion

                    return Ok();
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
