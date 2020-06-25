using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RequestorsController : ControllerBase
    {
        private readonly DBConnectionInfo _info;

        #region Requestors Constructor
        public RequestorsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Requestor Section

        #region Get Requestor Data
        // GET: api/Requestors
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{requestorID}")]
        public async Task<IActionResult> GetRequestor(int requestorID)
        {
            try
            {
                RequestorsRepository requestorsFac = new RequestorsRepository(_info);
                Requestors requestor = await requestorsFac.Select(requestorID);
                return Ok(requestor);
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion

        #region Add Requestor Data
        // POST: api/Requestors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Requestors>> AddRequestor(Requestors requestor)
        {
            try
            {
                #region Data Addition !UI
                //requestor.dtLastUpdate = DateTime.Now;
                #endregion

                RequestorsRepository requestorsFac = new RequestorsRepository(_info);
                int GeneratedID = (int)requestorsFac.Insert(requestor);
                Requestors dbRequestor = await requestorsFac.Select(GeneratedID);
                return dbRequestor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Edit Requestor Data
        // PUT: api/Requestor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditRequestor/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<Requestors> EditRequestor(int id, Requestors requestors)
        {
            if (id != requestors.nRequestorID)
            {
                return BadRequest();
            }
            RequestorsRepository requestorFac = new RequestorsRepository(_info);
            if (requestorFac.Update(requestors))
            { return NoContent(); }
            else
            { return NotFound(); }
        }
        #endregion

        #endregion

        #region TempRequestor Section

        #region Get TempRequestor Data
        // GET: api/Requestors
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{tempRequestorID}")]
        public async Task<IActionResult> GetTempRequestor(int tempRequestorID)
        {
            try
            {
                TempRequestorsRepository requestorsFac = new TempRequestorsRepository(_info);
                TempRequestors tempRequestor = await requestorsFac.Select(tempRequestorID);
                return Ok(tempRequestor);
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion

        #region Add TempRequestor Data
        // POST: api/Requestors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<TempRequestors>> AddTempRequestor(TempRequestors tempRequestor)
        {
            try
            {
                #region Data Addition !UI
                tempRequestor.dtLastUpdate = DateTime.Now;
                #endregion

                TempRequestorsRepository tempTequestorsFac = new TempRequestorsRepository(_info);
                int GeneratedID = (int)tempTequestorsFac.Insert(tempRequestor);
                TempRequestors dbTempRequestor = await tempTequestorsFac.Select(GeneratedID);
                return dbTempRequestor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Edit TempRequestor Data
        [HttpPost("EditTempRequestor/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<TempRequestors> EditTempRequestor(int id, TempRequestors tempRequestors)
        {
            if (id != tempRequestors.nRequestorID)
            {
                return BadRequest();
            }
            TempRequestorsRepository tempRequestorFac = new TempRequestorsRepository(_info);
            if (tempRequestorFac.Update(tempRequestors))
            { return NoContent(); }
            else
            { return NotFound(); }
        }
        #endregion

        #region Delete TempRequestor Data
        [HttpPost("DeleteTempRequestor")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<TempRequestors>> DeleteTempRequestor([FromBody] int id)
        {

            TempRequestorsRepository tempRequestorFac = new TempRequestorsRepository(_info);
            TempRequestors dbTemprequestor = await tempRequestorFac.Select(id);
            if (id != dbTemprequestor.nRequestorID)
            {
                return BadRequest();
            }
            if (tempRequestorFac.Delete(id))
            { return Ok(); }
            else
            { 
                return NotFound();
            }
        }
        #endregion

        #endregion
    }
}
