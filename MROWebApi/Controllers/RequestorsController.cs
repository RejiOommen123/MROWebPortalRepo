using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        #region Requestors Constructor
        private readonly DBConnectionInfo _info;
        public RequestorsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Requestor Section

        #region Get Requestor Data
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{requestorID}")]
        public async Task<IActionResult> GetRequestor(int requestorID)
        {
            try
            {
                RequestorsRepository requestorsFac = new RequestorsRepository(_info);
                Requesters requestor = await requestorsFac.Select(requestorID);
                return Ok(requestor);
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion

        #region Add Requestor Data
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public  ActionResult<int> AddRequestor(Requesters requester)
        {
            try
            {
                #region Data Addition ! From UI
                requester.dtLastUpdate = DateTime.Now;
                #endregion

                RequestorsRepository requestersFac = new RequestorsRepository(_info);

                #region Array Processing
                var PRArray = requester.sSelectedPrimaryReasons.Length != 0 ? string.Join(",", requester.sSelectedPrimaryReasons) : "";
                var SRArray = requester.sSelectedRecordTypes.Length != 0 ? string.Join(",", requester.sSelectedRecordTypes) : "";
                var STArray = requester.sSelectedShipmentTypes.Length != 0 ? string.Join(",", requester.sSelectedShipmentTypes) : "";
                var SIArray = requester.selectedSensitiveInfo.Length != 0 ? string.Join(",", requester.selectedSensitiveInfo) : "";
                var relativeFileArray = requester.sRelativeFileArray.Length != 0 ? string.Join(",", requester.sRelativeFileArray) : "";
                requester.sSelectedPrimaryReasons = new string[] { PRArray };
                requester.sSelectedRecordTypes = new string[] { SRArray };
                requester.sSelectedShipmentTypes = new string[] { STArray };
                requester.selectedSensitiveInfo = new string[] { SIArray };
                requester.sRelativeFileArray = new string[] { relativeFileArray };
                #endregion

                if (requester.nRequesterID==0) {
                    //Insert in Table
                    int GeneratedID = (int)requestersFac.Insert(requester);
                    return GeneratedID;
                }
                else {
                    //update in table 
                    requestersFac.Update(requester);
                    return requester.nRequesterID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Edit Requestor Data
        [HttpPost("EditRequestor/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<Requesters> EditRequestor(int id, Requesters requestors)
        {
            if (id != requestors.nRequesterID)
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
            if (id != tempRequestors.nRequesterID)
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
            if (id != dbTemprequestor.nRequesterID)
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
