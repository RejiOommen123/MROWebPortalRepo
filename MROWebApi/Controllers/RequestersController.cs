using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("[action]/{requesterID}")]
        //public async Task<IActionResult> GetRequester(int requesterID)
        //{
        //    try
        //    {
        //        RequestersRepository requestorsFac = new RequestersRepository(_info);
        //        Requesters requestor = await requestorsFac.Select(requesterID);
        //        return Ok(requestor);
        //    }
        //    catch (Exception exp)
        //    {
        //        return Content(exp.Message);
        //    }
        //}
        #endregion

        #region Add Requester Data
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        [RequestSizeLimit(78643200)]
        public async Task<ActionResult<int>> AddRequester(Requesters requester)
        {
            int nRequesterId = requester.nRequesterID;
            bool isValidRequest = true;
            try
            {
                DBConnectionInfo _infoRequester = new DBConnectionInfo();
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                // IEnumerable<FacilityConnections> facility = await connectionRepo.SelectWhere("nFacilityID", requester.nFacilityID);

                //_infoRequester.ConnectionString = facility.FirstOrDefault().sConnectionString;
                _infoRequester.ConnectionString = await connectionRepo.GetConnectionStringByFacilityID(requester.nFacilityID);

                RequestersRepository requestersFac = new RequestersRepository(_infoRequester);

                #region Matching GUID with the GUID in DB
                if (requester.nRequesterID > 0)
                {
                    string GUIDfromDB = await requestersFac.SelectGUID(requester.nRequesterID);
                    if (!(!String.IsNullOrEmpty(GUIDfromDB) && ((GUIDfromDB).Equals(requester.sGUID))))
                    {
                        isValidRequest = false;
                    }
                }
                else
                {
                    isValidRequest = false;
                }
                #endregion

                if (isValidRequest)
                {
                    #region Data Addition ! From UI
                    requester.dtLastUpdate = DateTime.Now;
                    #endregion
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
                    //requester.sRelativeFileArray = new string[] { new LocationAuthorizationDocumentController().GeneratePDFForXML("", requester.sRelativeFileArray) };
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
                        if ((requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_25") || (requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_26"))
                        {
                            nRequesterId = await requestersFac.UpdateRequesterFeedback(requester.nRequesterID, requester.bRequestAnotherRecord, requester.nFeedbackRating, requester.sFeedbackComment, requester.sWizardName);
                            if (requester.bRequestorFormSubmitted && requester.sWizardName == "Wizard_26" && requester.nFeedbackRating > 0)
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
                                writer.WriteElementString("rating", requester.nFeedbackRating.ToString());
                                writer.WriteElementString("comment", requester.sFeedbackComment);
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
                                        MROLogger.LogExceptionRecords(requester.nRequesterID, ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in ftp.", ex.Message + " Stack Trace " + ex.StackTrace, _info);
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
                                        MROLogger.LogExceptionRecords(requester.nRequesterID, ExceptionStatus.Error.ToString(), "Feedback - XML Generation in sFTP.", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                                    }
                                }

                            }
                        }
                        else
                        {
                            //requestersFac.Update(requester);
                            requestersFac.UpdateSingleRequestor(requester);
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
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(requester.nRequesterID, ExceptionStatus.Error.ToString(), "Exception in Add Requester Method.", ex.Message + " Stack Trace " + ex.StackTrace, _info);
            }
            return nRequesterId;
        }
        #endregion

        //#region Edit Requester Data
        //[HttpPost("EditRequester/{id}")]
        //[AllowAnonymous]
        //[Route("[action]")]
        //public ActionResult<Requesters> EditRequestor(int id, Requesters requesters)
        //{
        //    if (id != requesters.nRequesterID)
        //    {
        //        return BadRequest();
        //    }
        //    RequestersRepository requestorFac = new RequestersRepository(_info);
        //    if (requestorFac.Update(requesters))
        //    { return NoContent(); }
        //    else
        //    { return NotFound(); }
        //}
        //#endregion
        #region Update supporting document
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        [RequestSizeLimit(78643200)]
        public async Task<ActionResult<int>> UpdateSupportDocs(SupportDocs supportDocs)
        {
            int nRequesterId = supportDocs.nRequesterID;
            try
            {
                DBConnectionInfo _infoRequester = new DBConnectionInfo();
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                _infoRequester.ConnectionString = await connectionRepo.GetConnectionStringByFacilityID(supportDocs.nFacilityID);
                RequestersRepository requestersFac = new RequestersRepository(_infoRequester);

                var relativeFileArray = supportDocs.sRelativeFileArray.Length != 0 ? string.Join("_", supportDocs.sRelativeFileArray) : "";
                var relativeFileNameArray = supportDocs.sRelativeFileNameArray.Length != 0 ? string.Join("/", supportDocs.sRelativeFileNameArray) : "";
                supportDocs.sRelativeFileArray = new string[] { relativeFileArray };
                relativeFileArray = new LocationAuthorizationDocumentController().GeneratePDFForXML("", supportDocs.sRelativeFileArray);

                nRequesterId =await requestersFac.UpdateRequesterSupportDocs(supportDocs.nRequesterID, relativeFileArray, relativeFileNameArray, supportDocs.sWizardName);

            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(supportDocs.nRequesterID, ExceptionStatus.Error.ToString(), "Exception in Update Support Doc Method.", ex.Message + " Stack Trace " + ex.StackTrace, _info);
            }
            return nRequesterId;
        }
        #endregion
        #region Update identity document
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        [RequestSizeLimit(78643200)]
        public async Task<ActionResult<int>> UpdateIdentityDoc(IdentityDoc identityDoc)
        {
            int nRequesterId = identityDoc.nRequesterID;
            try
            {
                DBConnectionInfo _infoRequester = new DBConnectionInfo();
                FacilityConnectionsRepository connectionRepo = new FacilityConnectionsRepository(_info);
                _infoRequester.ConnectionString = await connectionRepo.GetConnectionStringByFacilityID(identityDoc.nFacilityID);
                RequestersRepository requestersFac = new RequestersRepository(_infoRequester);

                var identityImageAsPdf = string.IsNullOrEmpty(identityDoc.sIdentityImage) ? "" : new LocationAuthorizationDocumentController().GeneratePDFForXML("", new string[] { identityDoc.sIdentityImage });

                nRequesterId = await requestersFac.UpdateRequesterIdentityDoc(identityDoc.nRequesterID, identityImageAsPdf, identityDoc.sWizardName);

            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(identityDoc.nRequesterID, ExceptionStatus.Error.ToString(), "Exception in Update Identity Doc Method.", ex.Message + " Stack Trace " + ex.StackTrace, _info);
            }
            return nRequesterId;
        }
        #endregion

        #region Session transfer
        [HttpPost]
        [Route("[action]")]
        [SessionAuth]
        [RequestSizeLimit(78643200)]
        public async Task<ActionResult> SessionSwitch(SessionTransferObject sessionTransferObject)
        {
            string returnURL = "";
            try
            {
                //JObject CompleteStateJson = JObject.Parse(sessionTransferObject.CompleteState);
                bool fileSaved = false;
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(sessionTransferObject.CompleteState);
                sessionTransferObject.CompleteState = JsonSerializer.Serialize(jsonElement, options);

                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(sessionTransferObject.nFacilityId);
                FacilityLocationsRepository lRep = new FacilityLocationsRepository(_info);
                FacilityLocations location = await lRep.Select(sessionTransferObject.nLocationId);
                SessionTransferRepository stRep = new SessionTransferRepository(_info);
                IEnumerable<SessionTransfer> sessionTransfers = await stRep.SelectWhere("nRequesterID", sessionTransferObject.nRequesterId);
                SessionTransfer sessionTransfer = sessionTransfers.FirstOrDefault();

                MROHelperRepository helperRepo = new MROHelperRepository(_info);
                MROHelper helper = await helperRepo.Select(1);
                string pattern = @"\bMROSTGuid\b";
                string replaceGUID = Guid.NewGuid().ToString();
                returnURL = Regex.Replace(helper.sSessionTransferURL, pattern, replaceGUID);
                //File Name
                string sJsonFileName = "CompleteState_" + replaceGUID + ".json";
                string oldGUID = "", oldJsonFileName ="";
                if (sessionTransfer != null) {
                    oldGUID = sessionTransfer.sSessionTranferGUID;
                    oldJsonFileName = "CompleteState_" + oldGUID + ".json";
                }

                #region Decrypt FTP Password
                MROLogger password = new MROLogger(_info);
                facility.sFTPPassword = password.DecryptString(facility.sFTPPassword);
                #endregion

                //Json File Genration                 

                if ((facility.sFTPUrl.ToLower().Contains("ftp://")
                    && !facility.sFTPUrl.ToLower().Contains("sftp://"))
                    || facility.sFTPUrl.ToLower().Contains("ftps://"))
                {
                    bool dirExist = IsDirExist(facility.sFTPUrl + "SessionTransferFiles/", facility.sFTPUsername, facility.sFTPPassword, sessionTransferObject.nRequesterId);
                    if (!dirExist)
                    {
                        createDir(facility.sFTPUrl + "SessionTransferFiles/", facility.sFTPUsername, facility.sFTPPassword, sessionTransferObject.nRequesterId);
                    }

                    if (sessionTransfer != null) {
                        bool fileExist = IsDirExist(facility.sFTPUrl + "SessionTransferFiles/" + oldJsonFileName, facility.sFTPUsername, facility.sFTPPassword, sessionTransferObject.nRequesterId);
                        if (fileExist)
                        {
                            deleteFile(facility.sFTPUrl + "SessionTransferFiles/" + oldJsonFileName, facility.sFTPUsername, facility.sFTPPassword, sessionTransferObject.nRequesterId);
                        }
                    }

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(facility.sFTPUrl + "SessionTransferFiles/" + sJsonFileName);
                    #region Request Params
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(facility.sFTPUsername, facility.sFTPPassword);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.KeepAlive = false;
                    request.EnableSsl = true;
                    #endregion

                    byte[] buffer = Encoding.ASCII.GetBytes(sessionTransferObject.CompleteState);

                    //Upload file
                    try
                    {
                        Stream reqStream = request.GetRequestStream();
                        reqStream.Write(buffer, 0, buffer.Length);
                        fileSaved = true;
                        reqStream.Close();
                    }
                    catch (Exception ex)
                    {
                        MROLogger.LogExceptionRecords(sessionTransferObject.nRequesterId, ExceptionStatus.Error.ToString(),"Set Switch Session",ex.Message+  "Stack Trace " + ex.StackTrace, _info);
                        return BadRequest(new { StatusCode = "FTP_Error" });
                    }                    
                }
                else
                {
                    try
                    {
                        string sFTPURL = facility.sFTPUrl.ToLower() + "SessionTransferFiles/";
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

                        byte[] buffer = Encoding.ASCII.GetBytes(sessionTransferObject.CompleteState);
                        Stream stream = new MemoryStream(buffer);

                        ////Passing the sftp host without the "sftp://"
                        var client = new SftpClient(sDomanName, 22, facility.sFTPUsername, facility.sFTPPassword);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            bool dirExist = client.Exists(sFolderPath);
                            if (!dirExist)
                            {
                                client.CreateDirectory(sFolderPath);
                            }
                            if (sessionTransfer != null)
                            {
                                bool fileExist = client.Exists(sFolderPath + oldJsonFileName);
                                if (fileExist)
                                {
                                    client.Delete(sFolderPath + oldJsonFileName);
                                }
                            }
                            client.UploadFile(stream, sFolderPath + sJsonFileName, null);
                            fileSaved = client.Exists(sFolderPath + sJsonFileName);
                            client.Disconnect();
                            client.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MROLogger.LogExceptionRecords(sessionTransferObject.nRequesterId, ExceptionStatus.Error.ToString(), "Set Switch Session", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                        return BadRequest(new { StatusCode = "SFTP_Error" });
                    }
                }

                if (fileSaved)
                {                    
                    if (sessionTransfer == null)
                    {
                        sessionTransfer = new SessionTransfer()
                        {
                            sSessionTranferGUID = replaceGUID,
                            nFacilityID = sessionTransferObject.nFacilityId,
                            nFacilityLocationID = sessionTransferObject.nLocationId,
                            nRequesterID = sessionTransferObject.nRequesterId,
                            dtCreated = DateTime.Now,
                            dtExpired = DateTime.Now.AddHours(1),
                        };
                        stRep.Insert(sessionTransfer);
                    }
                    else
                    {
                        sessionTransfer.sSessionTranferGUID = replaceGUID;
                        sessionTransfer.dtExpired = DateTime.Now.AddHours(1);
                        stRep.Update(sessionTransfer);
                    }
                    //Send email if verified
                    if (sessionTransferObject.bSendEmail)
                    {
                        SendEmail sendEmail = new SendEmail()
                        {
                            info = _info,
                            nFacilityID = sessionTransferObject.nFacilityId,
                            nLocationID = sessionTransferObject.nLocationId,
                            sToMailName = sessionTransferObject.sFirstName + " " + sessionTransferObject.sLastName,
                            sToMailAddress = sessionTransferObject.sEmailId,
                            sSubject = "Session Transfer Email",
                            sBody = $"<p>Your session is successfully transferred</p> <p>To request records from ({facility.sFacilityName} – {sessionTransferObject.sLocationName}), please follow this link: <a href='{returnURL}' target='_blank'>Click here</a></p>"
                        };
                        await Utilities.SendEmail(sendEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(sessionTransferObject.nRequesterId, ExceptionStatus.Error.ToString(), "Set Switch Session", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                return BadRequest(new { StatusCode = "Session_Switch_Error" });
            }
            return Ok(new { data = returnURL });
        }
        #endregion

        #region Get transfered Session
        [HttpGet("GetSwitchedSession/st={st}")]
        [Route("[action]")]
        [AllowAnonymous]
        [RequestSizeLimit(78643200)]
        public async Task<ActionResult> GetSwitchedSession(string st)
        {
            string returnString = "";
            SessionTransfer sessionTransfer = null;
            try
            {
                SessionTransferRepository sessionTransferRepository = new SessionTransferRepository(_info);
                IEnumerable<SessionTransfer> sessionTransfers = await sessionTransferRepository.SelectWhere("sSessionTranferGUID", st);
                sessionTransfer = sessionTransfers.FirstOrDefault();
                if (sessionTransfer != null)
                {
                    FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                    Facilities facility = await rpFac.Select(sessionTransfer.nFacilityID);

                    #region Decrypt FTP Password
                    MROLogger password = new MROLogger(_info);
                    facility.sFTPPassword = password.DecryptString(facility.sFTPPassword);
                    #endregion
                    //Json File Genration 
                    //File Name
                    string sJsonFileName = "CompleteState_" + sessionTransfer.sSessionTranferGUID + ".json";

                    if ((facility.sFTPUrl.ToLower().Contains("ftp://")
                        && !facility.sFTPUrl.ToLower().Contains("sftp://"))
                        || facility.sFTPUrl.ToLower().Contains("ftps://"))
                    {
                        try
                        {
                            bool fileExist = IsDirExist(facility.sFTPUrl + "SessionTransferFiles/" + sJsonFileName, facility.sFTPUsername, facility.sFTPPassword, sessionTransfer.nRequesterID);
                            if (fileExist)
                            {
                                if (DateTime.Now < sessionTransfer.dtExpired)
                                {
                                    //read file
                                    FtpWebRequest readrequest = (FtpWebRequest)WebRequest.Create(facility.sFTPUrl + "SessionTransferFiles/" + sJsonFileName);
                                    #region Request Params
                                    readrequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                    readrequest.Credentials = new NetworkCredential(facility.sFTPUsername, facility.sFTPPassword);
                                    readrequest.UsePassive = true;
                                    readrequest.UseBinary = true;
                                    readrequest.KeepAlive = false;
                                    readrequest.EnableSsl = true;
                                    #endregion
                                    FtpWebResponse readresponse = (FtpWebResponse)readrequest.GetResponse();
                                    Stream responseStream = readresponse.GetResponseStream();
                                    StreamReader reader = new StreamReader(responseStream);
                                    returnString = reader.ReadToEnd();
                                    responseStream.Close();
                                    readresponse.Close();
                                }

                                //Delete file
                                FtpWebRequest deleterequest = (FtpWebRequest)WebRequest.Create(facility.sFTPUrl + "SessionTransferFiles/" + sJsonFileName);
                                #region Request Params
                                deleterequest.Method = WebRequestMethods.Ftp.DeleteFile;
                                deleterequest.Credentials = new NetworkCredential(facility.sFTPUsername, facility.sFTPPassword);
                                deleterequest.UsePassive = true;
                                deleterequest.UseBinary = true;
                                deleterequest.KeepAlive = false;
                                deleterequest.EnableSsl = true;
                                #endregion
                                FtpWebResponse deleteresponse = (FtpWebResponse)deleterequest.GetResponse();
                                deleteresponse.Close();

                                if (sessionTransfer.dtExpired < DateTime.Now)
                                {
                                    MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "Link expired", "", _info);
                                    return BadRequest(new { StatusCode = "Link_Expired" });
                                }
                            }
                            else
                            {
                                MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "File not exist", "", _info);
                                return BadRequest(new { StatusCode = "File_Not_Found" });
                            }
                        }
                        catch (Exception ex)
                        {
                            MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "Get Switched Session", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                            return BadRequest(new { StatusCode = "FTP_Error" });
                        }
                    }
                    else
                    {
                        try
                        {
                            string sFTPURL = facility.sFTPUrl.ToLower() + "SessionTransferFiles/";
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


                            ////Passing the sftp host without the "sftp://"
                            var client = new SftpClient(sDomanName, 22, facility.sFTPUsername, facility.sFTPPassword);
                            client.Connect();
                            if (client.IsConnected)
                            {
                                //returnString = client.ReadAllText(sFolderPath + sJsonFileName);
                                bool fileExist = client.Exists(sFolderPath + sJsonFileName);
                                if (fileExist)
                                {
                                    if (DateTime.Now < sessionTransfer.dtExpired)
                                    {
                                        Stream responseStream = new MemoryStream();
                                        client.DownloadFile(sFolderPath + sJsonFileName, responseStream);
                                        responseStream.Seek(0, SeekOrigin.Begin);
                                        StreamReader reader = new StreamReader(responseStream);
                                        returnString = reader.ReadToEnd();
                                    }

                                    client.Delete(sFolderPath + sJsonFileName);

                                    if (sessionTransfer.dtExpired < DateTime.Now)
                                    {
                                        MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "Link expired","", _info);
                                        return BadRequest(new { StatusCode = "Link_Expired" });
                                    }
                                }
                                else
                                {
                                    MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "File not exist", "", _info);
                                    return BadRequest(new { StatusCode = "File_Not_Found" });
                                }
                                client.Disconnect();
                                client.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "Get Switched Session", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                            return BadRequest(new { StatusCode = "SFTP_Error" });
                        }
                    }

                }
                else {
                    MROLogger.LogExceptionRecords(null, ExceptionStatus.Error.ToString(), "Invalid url", "", _info);
                    return BadRequest(new { StatusCode = "Invalid_Url" });
                }
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(sessionTransfer.nRequesterID, ExceptionStatus.Error.ToString(), "Get Switched Session", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                return BadRequest(new { StatusCode = "Get_Session_Switch_Error" });
            }
            return Ok(new { data = returnString });
        }
        #endregion

        #region Check FTP directory exist.
        private bool IsDirExist(string directory, string username, string password, int nRequesterID)
        {
            try
            {
                FtpWebRequest requestDir = (FtpWebRequest)WebRequest.Create(directory);
                #region Request Params
                requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
                requestDir.Credentials = new NetworkCredential(username, password);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                requestDir.EnableSsl = true;
                #endregion
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (!(response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable))
                {
                    MROLogger.LogExceptionRecords(nRequesterID, ExceptionStatus.Error.ToString(), "Check Directory Exist", ex.Message + " Stack Trace " + ex.StackTrace, _info);
                }
                response.Close();                    
                return false;
            }
        }
        #endregion

        #region Create FTP directory.
        private bool createDir(string directory, string username, string password, int nRequesterID)
        {
            try
            {
                FtpWebRequest requestMKDir = (FtpWebRequest)WebRequest.Create(directory);
                #region Request Params
                requestMKDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestMKDir.Credentials = new NetworkCredential(username, password);
                requestMKDir.UsePassive = true;
                requestMKDir.UseBinary = true;
                requestMKDir.KeepAlive = false;
                requestMKDir.EnableSsl = true;
                #endregion
                FtpWebResponse responseMKDir = (FtpWebResponse)requestMKDir.GetResponse();
                responseMKDir.Close();

                return true;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(nRequesterID, ExceptionStatus.Error.ToString(), "Get Switched Session", ex.Message    +"StackTrace"+ex.StackTrace,_info);
                return false;                
            }
        }
        #endregion

        #region Delete FTP File.
        private bool deleteFile(string directory, string username, string password, int nRequesterID)
        {
            try
            {
                FtpWebRequest requestDelFile = (FtpWebRequest)WebRequest.Create(directory);
                #region Request Params
                requestDelFile.Method = WebRequestMethods.Ftp.DeleteFile;
                requestDelFile.Credentials = new NetworkCredential(username, password);
                requestDelFile.UsePassive = true;
                requestDelFile.UseBinary = true;
                requestDelFile.KeepAlive = false;
                requestDelFile.EnableSsl = true;
                #endregion
                FtpWebResponse responseDelFile = (FtpWebResponse)requestDelFile.GetResponse();
                responseDelFile.Close();

                return true;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(nRequesterID, ExceptionStatus.Error.ToString(), "Get Switched Session", ex.Message + "StackTrace" + ex.StackTrace, _info);
                return false;
            }
        }
        #endregion

        #endregion

    }
}
