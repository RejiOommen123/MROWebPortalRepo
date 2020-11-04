using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UAParser;

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
        [SessionAuth]
        public async Task<ActionResult<int>> AddRequester(Requesters requester)
        {
            int nRequesterId = requester.nRequesterID;
            try
            {
                #region Data Addition ! From UI
                requester.dtLastUpdate = DateTime.Now;
                #endregion
                DBConnectionInfo _infoRequester = new DBConnectionInfo();
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                // IEnumerable<FacilityConnections> facility = await connectionRepo.SelectWhere("nFacilityID", requester.nFacilityID);

                //_infoRequester.ConnectionString = facility.FirstOrDefault().sConnectionString;
                _infoRequester.ConnectionString = await connectionRepo.GetConnectionStringByFacilityID(requester.nFacilityID);

                    RequestersRepository requestersFac = new RequestersRepository(_infoRequester);

                    #region Array Processing
                    var PRArray = requester.sSelectedPrimaryReasons.Length != 0 ? string.Join(",", requester.sSelectedPrimaryReasons) : "";
                    var SRArray = requester.sSelectedRecordTypes.Length != 0 ? string.Join(",", requester.sSelectedRecordTypes) : "";
                    var STArray = requester.sSelectedShipmentTypes.Length != 0 ? string.Join(",", requester.sSelectedShipmentTypes) : "";
                    var SIArray = requester.sSelectedSensitiveInfo.Length != 0 ? string.Join(",", requester.sSelectedSensitiveInfo) : "";
                    var relativeFileArray = requester.sRelativeFileArray.Length != 0 ? string.Join("_", requester.sRelativeFileArray) : "";
                    var relativeFileNameArray = requester.sRelativeFileNameArray.Length != 0 ? string.Join("/", requester.sRelativeFileNameArray) : "";
                    requester.sSelectedPrimaryReasons = new string[] { PRArray };
                    requester.sSelectedRecordTypes = new string[] { SRArray };
                    requester.sSelectedShipmentTypes = new string[] { STArray };
                    requester.sSelectedSensitiveInfo = new string[] { SIArray };
                    requester.sRelativeFileArray = new string[] { relativeFileArray };
                    requester.sRelativeFileNameArray = new string[] { relativeFileNameArray };
                    #endregion

                    #region Get requester OS and Browser Details
                    if (String.IsNullOrEmpty(requester.sOSInfo) || String.IsNullOrEmpty(requester.sBrowserInfo))
                    {
                        var userAgent = HttpContext.Request.Headers["User-Agent"];
                        string uaString = Convert.ToString(userAgent[0]);
                        var uaParser = Parser.GetDefault();
                        ClientInfo c = uaParser.Parse(uaString);
                        requester.sOSInfo = c.OS.ToString();
                        requester.sBrowserInfo = c.UserAgent.ToString();
                    }
                    #endregion

                    if (requester.nRequesterID == 0)
                    {
                        //Insert in Table
                        if (!String.IsNullOrEmpty(_infoRequester.ConnectionString))
                        {
                            
                            nRequesterId = (int)requestersFac.Insert(requester);
                        }
                    }
                    else
                    {
                        //Update in table 
                        if ((requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_24")|| (requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_25"))
                        {
                            nRequesterId = await requestersFac.UpdateRequesterFeedback(requester.nRequesterID,requester.bRequestAnotherRecord,requester.nFeedbackRating,requester.sFeedbackComment,requester.sWizardName);
                            if (requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_25" && requester.nFeedbackRating > 0)
                            {
                                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                                Facilities facility = await rpFac.Select(requester.nFacilityID);
                                XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
                                //{
                                //    OmitXmlDeclaration = false,
                                //    Encoding = Encoding.UTF8,
                                //    ConformanceLevel = ConformanceLevel.Document
                                //};

                                //XmlWriterSettings settings = new XmlWriterSettings(); 
                                xmlWriterSetting.Indent = true;
                                xmlWriterSetting.Encoding = Encoding.UTF8;

                                var xmlString = new StringWriterWithEncoding(Encoding.UTF8);

                                //StringBuilder xmlString = new StringBuilder();
                                using XmlWriter writer = XmlWriter.Create(xmlString, xmlWriterSetting);
                                writer.WriteStartElement("request");
                                writer.WriteStartElement("detail");
                                writer.WriteStartElement("facilityrequester");
                                writer.WriteElementString("id", requester.nRequesterID.ToString());
                                writer.WriteEndElement();
                                writer.WriteStartElement("facility");
                                writer.WriteElementString("value", facility.nROIFacilityID.ToString());
                                writer.WriteElementString("name", facility.sFacilityName.ToString());
                                writer.WriteElementString("id", facility.nFacilityID.ToString());
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                                writer.WriteStartElement("feedback");
                                writer.WriteElementString("rating",requester.nFeedbackRating.ToString());
                                writer.WriteElementString("comment",requester.sFeedbackComment);
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                                writer.Flush();

                                #region Decrypt FTP Password
                                MROLogger password = new MROLogger(_info);
                                facility.sFTPPassword = password.DecryptString(facility.sFTPPassword);
                                #endregion

                                //XML File Genration 
                                //File Name
                                string sXMLFileName = facility.sFacilityName + "_" + requester.sPatientFirstName + requester.sPatientLastName + "_" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + Guid.NewGuid().ToString() + ".xml";

                                if ((facility.sFTPUrl.ToLower().Contains("ftp://")
                                    && !facility.sFTPUrl.ToLower().Contains("sftp://"))
                                    || facility.sFTPUrl.ToLower().Contains("ftps://"))
                                {
                                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(facility.sFTPUrl + sXMLFileName);

                                    #region Request Params
                                    request.Method = WebRequestMethods.Ftp.UploadFile;
                                    request.Credentials = new NetworkCredential(facility.sFTPUsername, facility.sFTPPassword);
                                    request.UsePassive = true;
                                    request.UseBinary = true;
                                    request.KeepAlive = false;
                                    request.EnableSsl = true;
                                    #endregion

                                    byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());

                                    //Upload file
                                    try
                                    {
                                        Stream reqStream = request.GetRequestStream();
                                        reqStream.Write(buffer, 0, buffer.Length);
                                        reqStream.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in ftp.(RequesterID - "+requester.nRequesterID+")" , ex.Message + " Stack Trace " + ex.StackTrace, _info);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        string sFTPURL = facility.sFTPUrl.ToLower();
                                        if (sFTPURL.Contains("sftp://"))
                                        {
                                            sFTPURL = sFTPURL.Replace("sftp://", "");
                                        }
                                        string sDomanName, sFolderPath = string.Empty;
                                        string[] sFtpFolderPath = sFTPURL.Split(".com");
                                        if (sFtpFolderPath.Length == 2)
                                        {
                                            sDomanName = sFtpFolderPath[0] + ".com";
                                            sFolderPath = sFtpFolderPath[1];
                                        }
                                        else
                                        {
                                            sDomanName = sFtpFolderPath[0] + ".com";
                                            sFolderPath = "/";
                                        }

                                        byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());
                                        Stream stream = new MemoryStream(buffer);

                                        ////Passing the sftp host without the "sftp://"
                                        var client = new SftpClient(sDomanName, 22, facility.sFTPUsername, facility.sFTPPassword);
                                        client.Connect();
                                        if (client.IsConnected)
                                        {
                                            client.UploadFile(stream, sFolderPath + sXMLFileName, null);
                                            client.Disconnect();
                                            client.Dispose();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Feedback - XML Generation in sFTP.(RequesterID - " + requester.nRequesterID + ")", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                                }
                                }

                            }
                        }
                        else {
                            requestersFac.Update(requester);
                            nRequesterId = requester.nRequesterID;
                        }
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
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Exception in Add Requester Method.(RequesterID - " + requester.nRequesterID + ")", ex.Message + " Stack Trace " + ex.StackTrace, _info);
            }
            return nRequesterId;
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
