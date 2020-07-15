using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
    public class RequestersController : ControllerBase
    {
        #region Requesters Constructor
        private readonly DBConnectionInfo _info;
        public RequestersController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Requester Section

        #region Get Requester Data
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{requesterID}")]
        public async Task<IActionResult> GetRequester(int requesterID)
        {
            try
            {
                RequestersRepository requestorsFac = new RequestersRepository(_info);
                Requesters requestor = await requestorsFac.Select(requesterID);
                return Ok(requestor);
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion

        #region Add Requester Data
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<int>> AddRequester(Requesters requester)
        {
            try
            {
                #region Data Addition ! From UI
                requester.dtLastUpdate = DateTime.Now;
                #endregion
                DBConnectionInfo _infoRequester = new DBConnectionInfo();
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                IEnumerable<FacilityConnections> facility = await connectionRepo.SelectWhere("nFacilityID", requester.nFacilityID);

                _infoRequester.ConnectionString = facility.FirstOrDefault().sConnectionString;

                RequestersRepository requestersFac = new RequestersRepository(_infoRequester);

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

                int nRequesterId = 0;
                if (requester.nRequesterID == 0)
                {
                    //Insert in Table
                    nRequesterId = (int)requestersFac.Insert(requester);
                }
                else
                {
                    //Update in table 
                    requestersFac.Update(requester);
                    nRequesterId = requester.nRequesterID;
                }

                #region Requestor Logger
                FacilitiesRepository facRepo = new FacilitiesRepository(_info);
                Facilities dbFacility = await facRepo.Select(requester.nFacilityID);
                if (dbFacility.bFacilityLogging)
                {
                    MROLogger logger = new MROLogger(_info);
                    string sDescrption = "Requester " + nRequesterId + " of Facility " + dbFacility.nFacilityID + " has filled data till Wizard: " + requester.sWizardName;
                    logger.LogRequesterRecords(nRequesterId, requester.nFacilityID, sDescrption, requester.sWizardName);
                }
                #endregion

                return nRequesterId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Edit Requester Data
        [HttpPost("EditRequester/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public ActionResult<Requesters> EditRequestor(int id, Requesters requesters)
        {
            if (id != requesters.nRequesterID)
            {
                return BadRequest();
            }
            RequestersRepository requestorFac = new RequestersRepository(_info);
            if (requestorFac.Update(requesters))
            { return NoContent(); }
            else
            { return NotFound(); }
        }
        #endregion

        #endregion

    }
}
