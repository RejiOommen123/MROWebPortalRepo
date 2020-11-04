using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Utils;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WebSupergoo.ABCpdf11;
using UAParser;
using Microsoft.AspNetCore.Http;

namespace MROWebApi.Controllers
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding() { }

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class WizardsController : ControllerBase
    {
        #region Wizards Constructor
        private readonly DBConnectionInfo _info;
        public WizardsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion  

        #region Get Wizard Config
        [HttpGet("GetWizardConfig/fID={nFacilityID:int}&lID={nFacilityLocationID:int}")]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        public async Task<object> GetWizardConfigurationAsync(int nFacilityID, int nFacilityLocationID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                object Wizard_Config = await fieldsRepository.GetWizardConfigurationAsync(nFacilityID, nFacilityLocationID);
                return Wizard_Config;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard Location Details - By FacilityID - "+nFacilityID+" and LocationID - "+nFacilityLocationID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                return Content(ex.Message);
            }
        }
        #endregion

        #region Logo & BG for Facility using Facility GUID
        [HttpPost("GetFacilityDatafromFacilityGUID")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<object> GetFacilityDatafromFacilityGUID(GuidParameters guidParameters)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                dynamic LogoAndBackgroundImageforFacility = await fieldsRepository.GetLogoBackGroundforFacilityByGUIDAsync(guidParameters.guid, guidParameters.locationguid);
                return LogoAndBackgroundImageforFacility;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard From GUID - By Facility GUID - "+ guidParameters.guid + " LocationGUID - "+ guidParameters.locationguid, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                return Content(ex.Message);
            }
        }
        #endregion

        #region Logo & BG for Location using Location ID
        [HttpGet("GetLogoAndBackgroundImageforLocation/{nLocationID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<dynamic> GetLogoAndBackgroundImageforLocation(int nLocationID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                dynamic LogoAndBackgroundImageforLocation = await fieldsRepository.GetLogoBackGroundforLocationsync(nLocationID);
                return LogoAndBackgroundImageforLocation;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard Location - By LocationId - "+nLocationID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                return Content(ex.Message);
            }
        }
        #endregion

        #region  Add Requester to DB, Generate XML, Send Attachment Email and ROI Email
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        public async Task<IActionResult> GenerateXML([FromBody] Requesters requester)
        {
            if (ModelState.IsValid)
            {
                try 
                { 
                    FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                    Facilities facility = await rpFac.Select(requester.nFacilityID);
                    FacilityLocationsRepository locaFac = new FacilityLocationsRepository(_info);
                    FacilityLocations location = await locaFac.Select(requester.nLocationID);

                    byte[] signedPDF = await GetSignedPDF(requester,facility,location);

                    requester.sPDF = Convert.ToBase64String(signedPDF);
                    requester.sPDF = "data:application/pdf;base64," + requester.sPDF;
                    var sMiddleName = string.IsNullOrEmpty(requester.sPatientMiddleName) ? "" : requester.sPatientMiddleName;
                    var sAreYouPatient = requester.bAreYouPatient ? "No" : "Yes";
                    var sRelativeName = string.IsNullOrEmpty(requester.sRelativeFirstName) || string.IsNullOrEmpty(requester.sRelativeLastName) ? "" : requester.sRelativeFirstName + " " + requester.sRelativeLastName;
                    var sRelationToPatient = string.IsNullOrEmpty(requester.sSelectedRelationName) ? "" : requester.sSelectedRelationName;
                    var sConfirmReport = requester.bConfirmReport ? "Opted to be mailed to registered Email ID" : "Not Opted";
                    var sSelectedPrimaryReasonsName = string.IsNullOrEmpty(requester.sSelectedPrimaryReasonsName) ? "" : requester.sSelectedPrimaryReasonsName;
                    //var sComments = string.IsNullOrEmpty(requester.sFeedbackComment) ? "" : requester.sFeedbackComment;

                    string[] sSelectedRecordTypesForXML = requester.sSelectedRecordTypes;
                    string[] sSelectedSensitiveInfoForXML = requester.sSelectedSensitiveInfo;
                    //DB Storing
                    RequestersController requestersController = new RequestersController(_info);

                    #region Get requester OS and Browser Details
                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                    string uaString = Convert.ToString(userAgent[0]);
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(uaString);
                    requester.sOSInfo = c.OS.ToString();
                    requester.sBrowserInfo = c.UserAgent.ToString();
                    #endregion

                    await requestersController.AddRequester(requester);

                    //Send Email to Patient
                    MROLogger passwordDecrypt = new MROLogger(_info);
                    if (facility.bRequestorEmailConfirm)
                    {
                        if (await SendEmail(requester, signedPDF, _info, passwordDecrypt))
                        {
                            //await SendROIEmail(requester, signedPDF, _info, passwordDecrypt);

                        }
                        else
                        {
                            MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Email Error: Email Not send", "Email Not send for RequesterID: " + requester.nRequesterID + "Requester Email ID: " + requester.sRequesterEmailId, _info);
                            //return Content("Email Not Sent");
                        }
                    }
                    else {
                        MROLogger reqLogger = new MROLogger(_info);
                        reqLogger.LogRequesterRecords(requester.nRequesterID,requester.nFacilityID,"Confirmation email not send because Sending email is switched of for this facility",requester.sWizardName);
                        //public void LogRequesterRecords(int nRequesterID, int nFacilityID, string sDescription, string sWizardName)
                    }
                    //to get the Record Type for this facility
                    SensitiveInfoRepository SIFac = new SensitiveInfoRepository(_info);
                    IEnumerable<SensitiveInfo> facilitySensitiveInfo = await SIFac.SelectSensitiveInfoBynFacilityID(requester.nFacilityID);

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
                    writer.WriteElementString("name", facility.sFacilityName);
                    writer.WriteElementString("id", facility.nFacilityID.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("locations");
                    writer.WriteStartElement("item");
                    //For Location Code,Name,ID
                    writer.WriteElementString("code", location.sLocationCode);
                    writer.WriteElementString("value", location.nROILocationID.ToString());
                    writer.WriteElementString("name", requester.sSelectedLocationName);
                    writer.WriteElementString("id", location.nFacilityLocationID.ToString());
                    writer.WriteEndElement();
                    writer.WriteElementString("submitted", requester.bRequestorFormSubmitted.ToString());
                    writer.WriteEndElement();
                    //For Date, Reason,Comments
                    writer.WriteElementString("date", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss") );
                    writer.WriteElementString("date_required_by", requester.dtDeadline != null ? requester.dtDeadline.Value.ToString("yyyy-MM-dd") : ""); 
                    //writer.WriteElementString("reason", sSelectedPrimaryReasonsName);
                    //writer.WriteElementString("reasoncode", requester.sSelectedPrimaryReasons.Count() > 0 ? requester.sSelectedPrimaryReasons[0].ToString() : "");
                    writer.WriteStartElement("reason");
                    writer.WriteElementString("code", requester.sSelectedPrimaryReasons.Count() > 0 ? requester.sSelectedPrimaryReasons[0].ToString() : "");
                    writer.WriteElementString("name", sSelectedPrimaryReasonsName);
                    writer.WriteEndElement();

                    //writer.WriteElementString("comments", sComments);
                    writer.WriteElementString("expiration", requester.dtAuthExpire != null ? requester.dtAuthExpire.Value.ToString("yyyy-MM-dd") : "");
                    writer.WriteElementString("confirmwithdata", requester.bConfirmReport.ToString());
                    writer.WriteElementString("authevent", requester.sAuthSpecificEvent);
                    writer.WriteElementString("date_required", requester.bDeadlineStatus.ToString());
                    writer.WriteStartElement("deliverymethod");
                    writer.WriteElementString("code", requester.sReleaseTo);
                    writer.WriteElementString("name", requester.sReleaseToName);
                    writer.WriteEndElement();
                    writer.WriteStartElement("sensitive");
                    //Sensational Info
                    foreach (SensitiveInfo singleSensitiveInfo in facilitySensitiveInfo)
                    {
                        if (sSelectedSensitiveInfoForXML.Contains(singleSensitiveInfo.sNormalizedSensitiveInfoName))
                        {
                            writer.WriteStartElement("item");
                            writer.WriteElementString("code", singleSensitiveInfo.sNormalizedSensitiveInfoName);
                            writer.WriteElementString("name", singleSensitiveInfo.sSensitiveInfoName);
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("additionalcomments", requester.sAdditionalData);
                    writer.WriteElementString("os", requester.sOSInfo);
                    writer.WriteElementString("browser", requester.sBrowserInfo);

                    writer.WriteEndElement();
                    writer.WriteStartElement("patient");
                    writer.WriteElementString("firstname", requester.sPatientFirstName);
                    writer.WriteElementString("lastname", requester.sPatientLastName);
                    writer.WriteElementString("middle_name", sMiddleName);
                    writer.WriteElementString("previousfirstname", requester.sPatientPreviousFirstName);
                    writer.WriteElementString("previouslastname", requester.sPatientPreviousLastName);
                    writer.WriteElementString("previousmiddlename", requester.sPatientPreviousMiddleName);
                    writer.WriteElementString("haspreviousname", requester.bPatientNameChanged.ToString());

                    writer.WriteElementString("dob", requester.dtPatientDOB.Value.ToString("yyyy-MM-dd"));
                    writer.WriteStartElement("address");
                    //For Appartment,Street,City,State,ZipCode
                    writer.WriteElementString("apartment", requester.sAddApartment);
                    writer.WriteElementString("street", requester.sAddStreetAddress);
                    writer.WriteElementString("city", requester.sAddCity);
                    writer.WriteElementString("state", requester.sAddState);
                    writer.WriteElementString("zipcode", requester.sAddZipCode);
                    writer.WriteEndElement();
                    writer.WriteElementString("phone_number", requester.sPhoneNo);
                    writer.WriteElementString("phoneverified", requester.bPhoneNoVerified.ToString());
                    writer.WriteEndElement();

                    //Requester - Part
                    #region Requester Part
                    writer.WriteStartElement("requester");
                    writer.WriteElementString("firstname", requester.sRelativeFirstName);
                    writer.WriteElementString("lastname", requester.sRelativeLastName);
                    writer.WriteElementString("organization", requester.sRecipientOrganizationName);
                    writer.WriteElementString("is_patient", requester.bAreYouPatient.ToString());
                    writer.WriteElementString("relation", sRelationToPatient);
                    writer.WriteElementString("relationcode", requester.sSelectedRelation);
                    writer.WriteElementString("email", requester.sRequesterEmailId);
                    writer.WriteStartElement("address");
                    //For Street,City,State,ZipCode
                    writer.WriteElementString("apartment", requester.sAddApartment);
                    writer.WriteElementString("street", requester.sAddStreetAddress);
                    writer.WriteElementString("city", requester.sAddCity);
                    writer.WriteElementString("state", requester.sAddState);
                    writer.WriteElementString("zipcode", requester.sAddZipCode);
                    writer.WriteEndElement();
                    writer.WriteElementString("phone_number", requester.sPhoneNo);
                    writer.WriteElementString("fax_number", requester.sSTFaxNumber);
                    writer.WriteEndElement();

                    //Requester Part Ends Here
                    #endregion

                    writer.WriteStartElement("requested_information");
                    writer.WriteStartElement("dates_of_service");
                    if ((requester.dtRecordRangeStart != null) && (requester.dtRecordRangeEnd != null))
                    {
                        writer.WriteElementString("start", requester.dtRecordRangeStart.Value.ToString("yyyy-MM-dd"));
                        writer.WriteElementString("end", requester.dtRecordRangeEnd.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        writer.WriteElementString("start", string.Empty);
                        writer.WriteElementString("end", string.Empty);
                    }
                    writer.WriteElementString("mostrecent", requester.bRecordMostRecentVisit.ToString());
                    writer.WriteElementString("IsSpecifyVisit", requester.bSpecifyVisit.ToString());
                    writer.WriteElementString("SpecifyVisitText", requester.sSpecifyVisitText);
                    //Specific Event
                    writer.WriteEndElement();
                    writer.WriteStartElement("types");
                    if (!requester.bRTManualSelection)
                    {
                        writer.WriteStartElement("item");
                        writer.WriteElementString("code", "MRORecordTypeAbstract");
                        writer.WriteElementString("name", "Abstract");
                        writer.WriteEndElement();
                    }

                    //to get the Record Type for this facility
                    RecordTypesRepository rtFac = new RecordTypesRepository(_info);
                    IEnumerable<RecordTypes> facilityRecordTypes = await rtFac.SelectRecordTypeBynFacilityID(requester.nFacilityID);
             
                    foreach (RecordTypes singleRecordType in facilityRecordTypes)
                    {
                        if (sSelectedRecordTypesForXML.Contains(singleRecordType.sNormalizedRecordTypeName))
                        {
                            writer.WriteStartElement("item");
                            writer.WriteElementString("code", singleRecordType.sNormalizedRecordTypeName);
                            writer.WriteElementString("name", singleRecordType.sRecordTypeName);
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("otherrt", requester.sOtherRTText);
                    writer.WriteEndElement();
                    writer.WriteStartElement("shipment");
                    writer.WriteStartElement("types");
                    writer.WriteElementString("code", requester.sSelectedShipmentTypes.Count() > 0 ? requester.sSelectedShipmentTypes[0].ToString() : "");
                    writer.WriteElementString("name", requester.sSelectedShipmentTypesName);
                    writer.WriteEndElement();
                    writer.WriteElementString("email", requester.sSTEmailAddress);
                    writer.WriteStartElement("address");
                    writer.WriteElementString("apartment", requester.sSTAddApartment);
                    writer.WriteElementString("street", requester.sSTAddStreetAddress);
                    writer.WriteElementString("city", requester.sSTAddCity);
                    writer.WriteElementString("state", requester.sSTAddState);
                    writer.WriteElementString("zipcode", requester.sSTAddZipCode);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteStartElement("recipient");
                    writer.WriteElementString("firstname", requester.sRecipientFirstName);
                    writer.WriteElementString("lastname", requester.sRecipientLastName);
                    writer.WriteElementString("organization", requester.sRecipientOrganizationName);
                    writer.WriteStartElement("address");
                    writer.WriteElementString("apartment", requester.sRecipientAddApartment);
                    writer.WriteElementString("street", requester.sRecipientAddStreetAddress);
                    writer.WriteElementString("city", requester.sRecipientAddCity);
                    writer.WriteElementString("state", requester.sRecipientAddState);
                    writer.WriteElementString("zipcode", requester.sRecipientAddZipCode);
                    writer.WriteEndElement();
                    writer.WriteEndElement();

                    //PDF in Base 64 Encoding
                    requester.sPDF = new LocationAuthorizationDocumentController().GeneratePDFForXML(requester.sPDF, requester.sRelativeFileArray);
                    writer.WriteElementString("pdf", requester.sPDF);         
             
                    //writer.WriteEndElement();
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
                            MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in ftp. RequesterID - "+requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
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
                            MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in sFTP. RequesterID - " + requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Generate XML. RequesterID - " + requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                    throw;
                }
            //return Ok(xmlString.ToString());
            return Ok();

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }

        private static async Task<bool> SendEmail(Requesters requester, byte[] signedPDF, DBConnectionInfo _info, MROLogger passwordDecrypt)
        {
            try 
            { 
                FacilitiesRepository fRep = new FacilitiesRepository(_info);
                FacilityLocationsRepository lRep = new FacilityLocationsRepository(_info);
                Facilities dbFacility = await fRep.Select(requester.nFacilityID);
                FacilityLocations dbLocation = await lRep.Select(requester.nLocationID);
                //Check if Facility is Allowed to Send Mail - Not Required to Check CR005
                //if (dbFacility.bRequestorEmailConfirm)
                //{

                #region Decrypt SMTP Password
                dbFacility.sSMTPPassword = passwordDecrypt.DecryptString(dbFacility.sSMTPPassword);
                #endregion

                //From 
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                message.From.Add(from);

                //To
                MailboxAddress to = new MailboxAddress(requester.sPatientFirstName + " " + requester.sPatientLastName, requester.sRequesterEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Your Request for Medicals Records has been submitted successfully";
                BodyBuilder bodyBuilder = new BodyBuilder();

                dbLocation.sConfigLogoData = Regex.Replace(dbLocation.sConfigLogoData, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                byte[] locationLogo = Convert.FromBase64String(dbLocation.sConfigLogoData);
                var image = bodyBuilder.LinkedResources.Add("locationlogo", locationLogo);
                image.ContentId = MimeUtils.GenerateMessageId();
                string htmlText = string.Format(@"<div style='border:1px solid black;padding: 25px;'>
                <img src=""cid:{0}""><br/><br/>
                <div style='margin-left: 25px;margin-right: 25px;text-align:justify;text-justify: inter-word;'>
                <p>Thank you!</p>
                <p>You have successfully submitted your request.</p>
                <p>Within 24 hours you will receive an email from MROeXpress@mrocorp.com containing your request
                confirmation that will include your Request ID and Tracking ID. If you have not received your
                confirmation email within 24 hours, please call us at 610-994-7500 to speak with a Customer Service
                Expert, who will be able to assist you further.
                </p>
                <p>Our Experts are available Monday – Friday 8:30AM – 8:00PM EST.
                </p>
                </div>
                <div style='margin: 20px;'>
                <p>
                <h4  style='text-align:center;'>CONFIDENTIALITY NOTICE</p>
                </h4>
                <p style='text-align:justify;text-justify: inter-word;'>This communication is confidential property and privileged communication of the sender intended only
                for the person/entity to which it is addressed. If you are not the intended recipient, you are notified
                that any use, review, disclosure, distribution, or taking of any other action relevant to the
                contents of this message is strictly prohibited. If this message was received in error, please notify
                privacy@mrocorp.com immediately.</p>
                </div>
                </div>", image.ContentId);
                bodyBuilder.HtmlBody = htmlText;
                //Check if the attachment is required or not
                if (requester.bConfirmReport)
                {
                    bodyBuilder.Attachments.Add(requester.sPatientFirstName + " " + requester.sPatientLastName + " request.pdf", signedPDF);
                }
                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                //TODO:
                //Get Port number
                //Make SSL true
                client.Connect(dbFacility.sSMTPUrl, 25, false);
                try
                {
                    client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Sending Email. RequesterID - " + requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                    return false;
                }
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Send Email. RequesterID - " + requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                throw;
            }
            return true;
            //}
            //return false;
        }
        #endregion

        #region Email Verification
        public int GenerateRandomNo()
        {
            int _min = 1000;
            //10,000 Added for 9999 to be a part of Generated random Number
            int _max = 10000;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<string>> VerfiyRequestorEmail(EmailConfirmation requester)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(_info);
            Facilities dbFacility = await fRep.Select(requester.nFacilityID);

            if (dbFacility.bRequestorEmailVerify)
            {

                var sOTP = GenerateRandomNo().ToString();

                #region Decrypt SMTP Password
                MROLogger password = new MROLogger(_info);
                dbFacility.sSMTPPassword = password.DecryptString(dbFacility.sSMTPPassword);
                #endregion

                //From 
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                message.From.Add(from);

                //To
                string firstLastName, firstName;
                if (requester.bAreYouPatient) {
                    firstLastName = requester.sPatientFirstName + " " + requester.sPatientLastName;
                    firstName = requester.sPatientFirstName;
                }
                else {
                    firstLastName = requester.sRelativeFirstName + " " + requester.sRelativeLastName;
                    firstName = requester.sRelativeFirstName;
                }
                MailboxAddress to = new MailboxAddress(firstLastName, requester.sRequesterEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Here's Your 4 Digit Verification Code " + sOTP;
                BodyBuilder bodyBuilder = new BodyBuilder();
                string bodyText = "<h1>Hello " + firstName + "!</h1><br/> Here's Your 4 Digit Email Verification Code " + sOTP;
                bodyBuilder.HtmlBody = bodyText;
                message.Body = bodyBuilder.ToMessageBody();
                //GET Port number
                //Make SSL true
                try
                {
                    if (dbFacility.sSMTPUrl.Contains("protection"))
                    {
                        SmtpClient client = new SmtpClient();
                        client.Connect(dbFacility.sSMTPUrl, 25, false);
                        client.Capabilities &= ~SmtpCapabilities.Pipelining;
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();
                    }
                    else
                    {
                        SmtpClient client = new SmtpClient();
                        client.Connect(dbFacility.sSMTPUrl, 25, false);
                        client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();
                    }
                    //client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Email Verification. Requester EmailId - " + requester.sRequesterEmailId, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                    return Content(ex.Message);
                }
                return Ok(sOTP);
            }
            return "false";
        }
        #endregion

        #region Generate PDF
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [SessionAuth]
        public async Task<IActionResult> GeneratePDF(TestPdf testPdf)
        {
            if (ModelState.IsValid)
            {
                byte[] pdfBytes;
                if (testPdf.bIsTestRequest)
                {
                    Facilities facility = new Facilities()
                    {
                        sFacilityName = "Test Facility"
                    };
                    FacilityLocations location = new FacilityLocations()
                    {
                        sAuthTemplate = testPdf.sAuthTemplate
                    };

                    pdfBytes = await GetSignedPDF(testPdf.oRequester,facility,location);
                }
                else {

                    FacilitiesRepository fRep = new FacilitiesRepository(_info);
                    Facilities dbFacility = await fRep.Select(testPdf.oRequester.nFacilityID);


                    FacilityLocationsRepository locRepo = new FacilityLocationsRepository(_info);
                    FacilityLocations location = await locRepo.Select(testPdf.oRequester.nLocationID);

                    pdfBytes = await GetSignedPDF(testPdf.oRequester, dbFacility, location);
                }
                return File(pdfBytes, "application/pdf");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        private async Task<byte[]> GetSignedPDF(Requesters requester, Facilities dbFacility, FacilityLocations location)
        {
            try
            {
                //to get the Record Type for this facility
                RecordTypesRepository rtFac = new RecordTypesRepository(_info);
                IEnumerable<RecordTypes> facilityRecordTypes = await rtFac.SelectRecordTypeBynFacilityID(requester.nFacilityID);
                SensitiveInfoRepository SIFac = new SensitiveInfoRepository(_info);
                IEnumerable<SensitiveInfo> facilitySensitiveInfo = await SIFac.SelectSensitiveInfoBynFacilityID(requester.nFacilityID);

                Dictionary<string, string> allFields = new Dictionary<string, string>();
                allFields.Add("MROFacilityName", dbFacility.sFacilityName);
                allFields.Add("MROLocationName", requester.sSelectedLocationName);
                if (requester.bAreYouPatient)
                {
                    allFields.Add("MROAreYouPatient=1", "On");
                }
                if (!string.IsNullOrEmpty(requester.sPatientFirstName))
                {
                    allFields.Add("MROPatientFullName", requester.sPatientFirstName + " " + requester.sPatientMiddleName + " " +requester.sPatientLastName);
                }
                else
                {
                    allFields.Add("MROPatientFullName", string.Empty);
                }                
                allFields.Add("MROPatientFirstName", requester.sPatientFirstName);
                allFields.Add("MROPatientMiddleName", requester.sPatientMiddleName);
                allFields.Add("MROPatientLastName", requester.sPatientLastName);
                allFields.Add("MROPatientBirthDate", requester.dtPatientDOB.Value.ToShortDateString());
                allFields.Add("MROPatientDOBDAY", requester.dtPatientDOB.Value.Day.ToString());
                allFields.Add("MROPatientDOBMONTH", requester.dtPatientDOB.Value.Month.ToString());
                allFields.Add("MROPatientDOBYEAR", requester.dtPatientDOB.Value.Year.ToString());

                allFields.Add("MRORequesterEmailId", requester.sRequesterEmailId);
                //Confirm email send report
                if (requester.bConfirmReport)
                {
                    allFields.Add("MROConfirmReport=1", "On");
                }
                //Previous Patient Name
                if (!string.IsNullOrEmpty(requester.sPatientPreviousFirstName))
                {
                    allFields.Add("MROPatientPreviousFullName", requester.sPatientPreviousFirstName + " " + requester.sPatientPreviousMiddleName + " " + requester.sPatientPreviousLastName);
                }
                else
                {
                    allFields.Add("MROPatientPreviousFullName", string.Empty);
                }               
                allFields.Add("MROPatientPreviousFirstName", requester.sPatientPreviousFirstName);
                allFields.Add("MROPatientPreviousLastName", requester.sPatientPreviousLastName);
                allFields.Add("MROPatientPreviousMiddleName", requester.sPatientPreviousMiddleName);
                if (requester.bPatientNameChanged)
                {
                    allFields.Add("MROPatientNameChanged=1", "On");
                }

                allFields.Add("MROAddZipCode", requester.sAddZipCode);
                allFields.Add("MROAddCity", requester.sAddCity);
                allFields.Add("MROAddState", requester.sAddState);
                allFields.Add("MROAddStreetAddress", requester.sAddStreetAddress);
                allFields.Add("MROAddAppartment", requester.sAddApartment);
                allFields.Add("MROAddCompleteAddress", requester.sAddStreetAddress + ", "
                                                    + requester.sAddApartment + ", "
                                                    + requester.sAddCity + ", "
                                                    + requester.sAddState + ", "
                                                    + requester.sAddZipCode);
                //Relationship
                if (requester.sSelectedRelation != "")
                {
                    allFields.Add(requester.sSelectedRelation + "=1", "On");
                }
                allFields.Add("MRORelativeFirstName", requester.sRelativeFirstName);
                allFields.Add("MRORelativeLastName", requester.sRelativeLastName);
                allFields.Add("MRORelationshipToPatientText", requester.sSelectedRelationName);


                //Date Range
                if ((requester.dtRecordRangeStart != null) && (requester.dtRecordRangeEnd != null))
                {
                    allFields.Add("MRODateRangeStart", requester.dtRecordRangeStart.Value.ToShortDateString());
                    allFields.Add("MRODateRangeEnd", requester.dtRecordRangeEnd.Value.ToShortDateString());
                }
                else
                {
                    allFields.Add("MRODateRangeStart", string.Empty);
                    allFields.Add("MRODateRangeEnd", string.Empty);
                }
                if (requester.bRecordMostRecentVisit)
                {
                    allFields.Add("MRORecordsMostRecentVisit=1", "On");
                }
                if (requester.bSpecifyVisit)
                {
                    allFields.Add("MROSpecifyVisit=1", "On");
                    allFields.Add("MROSpecifyVisitText", requester.sSpecifyVisitText);
                }
                else
                {
                    allFields.Add("MROSpecifyVisitText", "");
                }

                //Record Types
                if (requester.bRTManualSelection)
                {
                    for (int counter = 0; counter < requester.sSelectedRecordTypes.Length; counter++)
                    {
                        if (requester.sSelectedRecordTypes[counter] != "")
                        {
                            allFields.Add(requester.sSelectedRecordTypes[counter] + "=1", "On");
                            RecordTypes recordType = facilityRecordTypes.FirstOrDefault(q => q.sNormalizedRecordTypeName == requester.sSelectedRecordTypes[counter]);
                            if (recordType != null && requester.sSelectedRecordTypes[counter] != "MROOtherRT") { 
                                allFields.Add(requester.sSelectedRecordTypes[counter] + "Text", recordType.sRecordTypeName);
                            }
                        }                       
                    }
                }
                else
                {
                    allFields.Add("MRORecordTypeAbstract=1", "On");
                }
                if (!string.IsNullOrEmpty(requester.sOtherRTText))
                {
                    allFields.Add("MROOtherRTText", requester.sOtherRTText);
                }
                else
                {
                    allFields.Add("MROOtherRTText", "");
                }


                //Primary Reasons
                if (requester.sSelectedPrimaryReasons.Length == 0)
                {
                    allFields.Add("MROOtherPrimaryReasonText", "");
                }
                else
                {
                    for (int counter = 0; counter < requester.sSelectedPrimaryReasons.Length; counter++)
                    {
                        if (requester.sSelectedPrimaryReasons[counter] != "")
                        {
                            allFields.Add(requester.sSelectedPrimaryReasons[counter] + "=1", "On");
                            allFields.Add("MROSelectedPrimaryReasonText", requester.sSelectedPrimaryReasonsName);
                            if ("MROPatientRequest" != requester.sSelectedPrimaryReasons[counter])
                            {
                                allFields.Add("MROOtherPrimaryReasonText", requester.sSelectedPrimaryReasonsName);
                            }
                            else
                            {
                                //Clearing this label from PDf if request is "Patient Request"
                                allFields.Add("MROOtherPrimaryReasonText", "");
                            }
                        }

                    }
                }




                //Sensitive Info
                for (int counter = 0; counter < requester.sSelectedSensitiveInfo.Length; counter++)
                {
                    if (requester.sSelectedSensitiveInfo[counter] != "")
                    {
                        allFields.Add(requester.sSelectedSensitiveInfo[counter] + "=1", "On");
                        SensitiveInfo sensitiveInfo = facilitySensitiveInfo.FirstOrDefault(q => q.sNormalizedSensitiveInfoName == requester.sSelectedSensitiveInfo[counter]);
                        if (sensitiveInfo != null)
                        {
                            allFields.Add(requester.sSelectedSensitiveInfo[counter] + "Text", sensitiveInfo.sSensitiveInfoName);
                        }
                    }
                }

                //Shipment Types 
                for (int counter = 0; counter < requester.sSelectedShipmentTypes.Length; counter++)
                {
                    if (requester.sSelectedShipmentTypes[counter] != "")
                    {
                        allFields.Add(requester.sSelectedShipmentTypes[counter] + "=1", "On");
                        allFields.Add("MROSelectedShipmentTypeText", requester.sSelectedShipmentTypesName);
                    }
                }

                //Shipment Type Related Fields
                allFields.Add("MROSTAddZipCode", requester.sSTAddZipCode);
                allFields.Add("MROSTAddState", requester.sSTAddState);
                allFields.Add("MROSTAddCity", requester.sSTAddCity);
                allFields.Add("MROSTAddStreetAddress", requester.sSTAddStreetAddress);
                allFields.Add("MROSTAddApartment", requester.sSTAddApartment);
                if (!string.IsNullOrEmpty(requester.sSTAddCity))
                {
                    allFields.Add("MROSTCompleteAddress", requester.sSTAddStreetAddress + ", "
                                                   + requester.sSTAddApartment + ", "
                                                   + requester.sSTAddCity + ", "
                                                   + requester.sSTAddState + ", "
                                                   + requester.sSTAddZipCode);
                }
                else
                {
                    allFields.Add("MROSTCompleteAddress", string.Empty);
                }
               
                allFields.Add("MROSTEmailAddress", requester.sSTEmailAddress);
                allFields.Add("MROSTFaxNumber", requester.sSTFaxNumber);




                //Release To 'MROReleaseToMyself' 'MROReleaseToFamilyCaregiver' 'MROReleaseToDoctor' 'MROReleaseToThirdParty'
                if (requester.sReleaseTo != "")
                {
                    allFields.Add(requester.sReleaseTo + "=1", "On");
                }

                //Recipient 
                allFields.Add("MRORecipientAddZipCode", requester.sRecipientAddZipCode);
                allFields.Add("MRORecipientAddState", requester.sRecipientAddState);
                allFields.Add("MRORecipientAddCity", requester.sRecipientAddCity);
                allFields.Add("MRORecipientAddStreetAddress", requester.sRecipientAddStreetAddress);
                allFields.Add("MRORecipientAddApartment", requester.sRecipientAddApartment);              
                if (!string.IsNullOrEmpty(requester.sRecipientAddCity))
                {
                    allFields.Add("MRORecipientCompleteAddress", requester.sRecipientAddStreetAddress + ", "
                                                  + requester.sRecipientAddApartment + ", "
                                                  + requester.sRecipientAddCity + ", "
                                                  + requester.sRecipientAddState + ", "
                                                  + requester.sRecipientAddZipCode);
                }
                else
                {
                    allFields.Add("MRORecipientCompleteAddress", string.Empty);
                }

                allFields.Add("MRORecipientFirstName", requester.sRecipientFirstName);
                allFields.Add("MRORecipientLastName", requester.sRecipientLastName);
                if (!string.IsNullOrEmpty(requester.sRecipientFirstName))
                {
                    allFields.Add("MRORecipientFullName", requester.sRecipientFirstName + " " + requester.sRecipientLastName);
                }
                else
                {
                    allFields.Add("MRORecipientFullName", string.Empty);
                }                
                allFields.Add("MRORecipientOrganizationName", requester.sRecipientOrganizationName);


                //Auth exipry date
                if ((requester.dtAuthExpire != null))
                {
                    allFields.Add("MROAuthExpireDateSpecificDate", requester.dtAuthExpire.Value.ToShortDateString());
                }
                else
                {
                    allFields.Add("MROAuthExpireDateSpecificDate", string.Empty);
                }
                allFields.Add("MROAuthExpireDateEventOccurs", requester.sAuthSpecificEvent);

                //Is Deadline - bDeadlineStatus
                allFields.Add("MRORequestDeadline=1", requester.bDeadlineStatus ? "On" : "");

                //Deadline Date
                if ((requester.dtDeadline != null))
                {
                    allFields.Add("MRORequestDeadlineDate", requester.dtDeadline.Value.ToShortDateString());
                }
                else
                {
                    allFields.Add("MRORequestDeadlineDate", string.Empty);
                }

                //Additional Data - sAdditionalData
                allFields.Add("MROPatientAdditionalDetails", requester.sAdditionalData);

                //Phone number - sPhoneNo
                allFields.Add("MRORequesterPhoneNumber", requester.sPhoneNo);
                //Is Phone Number Verified - 
                allFields.Add("MRORequesterPhoneVerified=1", requester.bPhoneNoVerified ? "On" : "");

                // Is identity is DL or Other
                if (!string.IsNullOrEmpty(requester.sIdentityIdName)) { 
                    if (requester.sIdentityIdName == "MRODLIdentity")
                    {
                        allFields.Add("MRODLIdentity=1", "On");
                    }
                    else
                    {
                        allFields.Add("MROOtherGovIdentity=1", "On");
                    }
                }
                //MROToday's Date
                allFields.Add("MROTodaysDate", DateTime.Now.ToString("MM-dd-yyyy"));


                location.sAuthTemplate = location.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
                byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);

                byte[] byteArrayToReturn = new LocationAuthorizationDocumentController().ReplaceFieldKeywordsWithValue(pdfByteArray, allFields, requester, out string sReplaceFieldsList);

                allFields.Clear();

                if (!string.IsNullOrEmpty(requester.sSignatureData))
                {
                    //Signing the Document
                    Doc theDoc = new Doc();

                    theDoc.Read(byteArrayToReturn);
                    if (theDoc.Form["MROSignature01"] != null)
                    {
                        string removeDataTag = requester.sSignatureData.Replace("data:image/png;base64,", string.Empty);
                        byte[] signatureByteArray = Convert.FromBase64String(removeDataTag);
                        Image image2 = Image.FromStream(new MemoryStream(signatureByteArray));

                        MROHelperRepository helperRepo = new MROHelperRepository(_info);
                        MROHelper helper = await helperRepo.Select(1);
                        helper.sWhitebgimg = helper.sWhitebgimg.Replace("data:image/png;base64,", string.Empty);
                        byte[] data = Convert.FromBase64String(helper.sWhitebgimg);

                        System.Drawing.Image canvas = Bitmap.FromStream(new MemoryStream(data, 0, data.Length));
                        Graphics gra = Graphics.FromImage(canvas);
                        //Bitmap smallImg = new Bitmap(image2);
                        Bitmap smallImg = new Bitmap(image2, 350, 120);
                        gra.DrawImage(smallImg, new Point(0, 0));


                        var mssignaturewithbg = new MemoryStream();
                        canvas.Save(mssignaturewithbg, canvas.RawFormat);
                        mssignaturewithbg.ToArray();


                        XImage theImg = new XImage();
                        theImg.SetStream(mssignaturewithbg);
                        theDoc.Rect.String = theDoc.Form["MROSignature01"].Rect.String;
                        theDoc.Rect.Magnify(1, 1);
                        theDoc.Rect.Position(theDoc.Form["MROSignature01"].Rect.Left, theDoc.Form["MROSignature01"].Rect.Bottom);
                        theDoc.AddImageObject(theImg, false);

                        if (theDoc.Form["MROSignature02"] != null)
                        {
                            theDoc.Rect.String = theDoc.Form["MROSignature02"].Rect.String;
                            theDoc.Rect.Magnify(1, 1);
                            theDoc.Rect.Position(theDoc.Form["MROSignature02"].Rect.Left, theDoc.Form["MROSignature02"].Rect.Bottom);
                            theDoc.AddImageObject(theImg, false);
                        }

                        if (theDoc.Form["MROSignature03"] != null)
                        {
                            theDoc.Rect.String = theDoc.Form["MROSignature03"].Rect.String;
                            theDoc.Rect.Magnify(1, 1);
                            theDoc.Rect.Position(theDoc.Form["MROSignature03"].Rect.Left, theDoc.Form["MROSignature03"].Rect.Bottom);
                            theDoc.AddImageObject(theImg, false);
                        }

                        theImg.Clear();

                        theDoc.Form.Stamp();

                        byte[] pdfBytes = theDoc.GetData();
                        return pdfBytes;
                    }
                }

                return byteArrayToReturn;

            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "PDF Generation Error. RequesterID - " + requester.nRequesterID , ex.Message +" Stack Trace " + ex.StackTrace, _info);
                return null;
            }
        }

        #endregion

        #region ROI Email
        private static async Task<bool> SendROIEmail(Requesters requester, byte[] signedPDF, DBConnectionInfo _info, MROLogger passwordDecrypt)
        {
                FacilitiesRepository fRep = new FacilitiesRepository(_info);
                FacilityLocationsRepository lRep = new FacilityLocationsRepository(_info);
                Facilities dbFacility = await fRep.Select(requester.nFacilityID);
                FacilityLocations dbLocation = await lRep.Select(requester.nLocationID);
                //Check if Facility is Allowed to Send Mail
                if (dbFacility.bRequestorEmailConfirm)
                {

                    #region Decrypt SMTP Password
                    dbFacility.sSMTPPassword = passwordDecrypt.DecryptString(dbFacility.sSMTPPassword);
                    #endregion

                    #region Get Footer Image
                    MROHelperRepository helperRepo = new MROHelperRepository(_info);
                    MROHelper helper = await helperRepo.Select(1);
                    #endregion

                    //From 
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                    message.From.Add(from);

                    //To
                    MailboxAddress to = new MailboxAddress(requester.sPatientFirstName + " " + requester.sPatientLastName, requester.sRequesterEmailId);
                    message.To.Add(to);

                    //Subject
                    message.Subject = "Request Confirmation";
                    BodyBuilder bodyBuilder = new BodyBuilder();

                    helper.sMROEmailFooterImage = Regex.Replace(helper.sMROEmailFooterImage, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                    byte[] footerLogo = Convert.FromBase64String(helper.sMROEmailFooterImage);
                    var image = bodyBuilder.LinkedResources.Add("footerlogo", footerLogo);
                    image.ContentId = MimeUtils.GenerateMessageId();
                    var sFullName = requester.sPatientFirstName + " " + requester.sPatientLastName;
                    string htmlText = string.Format(@"<div style='border:1px solid black;padding: 25px;'>
                                    <p style='text-align: right;'>{0}&nbsp;</p><br/>
                                    <div style='margin-left: 25px;margin-right: 25px;text-align:justify;text-justify: inter-word;'>
                                        <p>Dear {1},</p>
                                        <p>Thank you for completing your medical records request! It is currently being processed and will ship as soon as it is complete. Please note, this email serves as your request confirmation.</p>
                                        <p><b>Request ID: </b>{2}</p>
                                        <p><b>Tracking ID: </b>XXXXXXXXXXXXX</p>
                                        <p>If you have any questions regarding your request, please use the Request ID provided above and contact us at:</p>
                                        <ul>
                                            <li>Online at <a href='https://mrocorp.com/record-requests/'><b>https://mrocorp.com/record-requests/</b></a> where you can check the status of your request or make a payment</li>
                                            <li>Call us at <b><a href='(610) 994-7500'>(610) 994-7500</a></b> to use our automated status system or speak with a Customer Service Expert, Mon-Fri 8:30AM-8:00PM (EST)</li>
                                        </ul>
                                        <p>Kindly,</p>
                                        <p><em>Team</em> {3}</p>
                                    </div>
                                    <div style='margin: 20px;'>
                                        <p>
                                        <h4  style='text-align:center;'>CONFIDENTIALITY NOTICE</p>
                                        </h4>
                                        <p style='text-align:justify;text-justify: inter-word;'>This communication is confidential property and privileged communication of the sender intended only for the person/entity to which it is addressed.  If you are not the intended recipient, you are notified that any use, review, disclosure, distribution, or taking of any other action relevant to the contents of this message is strictly prohibited. If this message was received in error, please notify privacy@mrocorp.com immediately.</p>
                                        <div style='text-align:center'><a href='https://mrocorp.com/' target='_blank'><img src=""cid:{4}""></a></div>
                                    </ div >
                                   </ div > ", DateTime.Now.ToString("ddd, MMMM dd, h:mm tt"),
                                                sFullName, requester.nRequesterID,
                                                dbFacility.sFacilityName,
                                                image.ContentId);

                    bodyBuilder.HtmlBody = htmlText;
                    message.Body = bodyBuilder.ToMessageBody();
                    //TODO:
                    //Get Port number
                    //Make SSL true
                    try
                    {
                        if (dbFacility.sSMTPUrl.Contains("protection"))
                        {
                            SmtpClient client = new SmtpClient();
                            client.Connect(dbFacility.sSMTPUrl, 25, false);
                            client.Capabilities &= ~SmtpCapabilities.Pipelining;
                            client.Send(message);
                            client.Disconnect(true);
                            client.Dispose();
                        }
                        else
                        {
                            SmtpClient client = new SmtpClient();
                            client.Connect(dbFacility.sSMTPUrl, 25, false);
                            client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                            client.Send(message);
                            client.Disconnect(true);
                            client.Dispose();
                        }
                        //client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                    }
                    catch (Exception ex)
                    {
                        MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Send ROI Email. RequesterID - " + requester.nRequesterID, ex.Message + " Stack Trace " + ex.StackTrace, _info);
                        return false;
                    }
                
                return true;
                }
            return false;
        }
        #endregion
    }
}
