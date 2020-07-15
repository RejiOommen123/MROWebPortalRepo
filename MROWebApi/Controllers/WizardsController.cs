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

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
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
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard Location Details - By FacilityID and LocationID", ex.Message, _info);
                return Content(ex.Message);
            }
        }
        #endregion

        #region Logo & BG for Facility using Facility GUID
        [HttpGet("GetFacilityDatafromFacilityGUID/{sGUID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<object> GetFacilityDatafromFacilityGUID(string sGUID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                dynamic LogoAndBackgroundImageforFacility = await fieldsRepository.GetLogoBackGroundforFacilityByGUIDAsync(sGUID);
                return LogoAndBackgroundImageforFacility;
            }
            catch (Exception ex)
            {
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard From GUID - By Facility GUID", ex.Message, _info);
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
                MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Wizard Location - By LocationId", ex.Message, _info);
                return Content(ex.Message);
            }
        }
        #endregion

        #region  Add Requestor to DB, Generate XML, Send Attachment Email and ROI Email
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GenerateXML([FromBody] Requesters requester)
        {
            if (ModelState.IsValid)
            {
                //DB Storing
                RequestersController requestersController = new RequestersController(_info);
                await requestersController.AddRequester(requester);


                byte[] signedPDF = await GetSignedPDF(requester);
                requester.sPDF = Convert.ToBase64String(signedPDF);
                requester.sPDF = "data:application/pdf;base64," + requester.sPDF;
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(requester.nFacilityID);
                FacilityLocationsRepository locaFac = new FacilityLocationsRepository(_info);
                FacilityLocations location = await locaFac.Select(requester.nLocationID);
                //var sPatientDeceased = requestors.bIsPatientDeceased ? "Yes" : "No";
                var sMiddleName = string.IsNullOrEmpty(requester.sPatientMiddleName) ? "" : requester.sPatientMiddleName;
                var sAreYouPatient = requester.bAreYouPatient ? "No" : "Yes";
                var sRelativeName = string.IsNullOrEmpty(requester.sRelativeFirstName) || string.IsNullOrEmpty(requester.sRelativeLastName) ? "" : requester.sRelativeFirstName + " " + requester.sRelativeLastName;
                var sRelationToPatient = string.IsNullOrEmpty(requester.sSelectedRelationName) ? "" : requester.sSelectedRelationName;
                var sConfirmReport = requester.bConfirmReport ? "Opted to be mailed to registered Email ID" : "Not Opted";
                var sOtherReasons = string.IsNullOrEmpty(requester.sOtherReasons) ? "" : requester.sOtherReasons;
                var sComments = string.IsNullOrEmpty(requester.sFeedbackComment) ? "" : requester.sFeedbackComment;
                

                XmlWriterSettings xmlWriterSetting = new XmlWriterSettings
                {
                    OmitXmlDeclaration = false,
                    ConformanceLevel = ConformanceLevel.Document
                };
                StringBuilder xmlString = new StringBuilder();
                using XmlWriter writer = XmlWriter.Create(xmlString, xmlWriterSetting);
                writer.WriteStartElement("request");
                writer.WriteStartElement("facility");
                writer.WriteElementString("name", facility.sFacilityName);
                writer.WriteElementString("ID", facility.nFacilityID.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("locations");
                writer.WriteStartElement("item");
                //For Location Code,Name,ID
                writer.WriteElementString("code", location.sLocationCode);
                writer.WriteElementString("name", location.sLocationName);
                writer.WriteElementString("ID", location.nFacilityLocationID.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("detail");
                //For Date, Reason,Comments
                writer.WriteElementString("date", DateTime.Now.ToString());
                writer.WriteElementString("date_required_by", requester.dtDeadline.ToString());
                writer.WriteElementString("reason", sOtherReasons);
                writer.WriteElementString("comments", sComments);
                writer.WriteElementString("expiration", requester.dtAuthExpire.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("patient");
                writer.WriteElementString("firstname", requester.sPatientFirstName);
                writer.WriteElementString("lastname", requester.sPatientLastName);
                writer.WriteElementString("middle_name", sMiddleName);
                writer.WriteElementString("dob", requester.dtPatientDOB.Value.ToShortDateString());
                writer.WriteStartElement("address");
                //For Street,City,State,ZipCode
                writer.WriteElementString("street", requester.sAddStreetAddress);
                writer.WriteElementString("city", requester.sAddCity);
                writer.WriteElementString("state", requester.sAddState);
                writer.WriteElementString("zipcode", requester.sAddZipCode);
                writer.WriteEndElement();
                writer.WriteElementString("phone_number", requester.sPhoneNo);
                writer.WriteEndElement();

                //Requestor - Part
                #region Requestor Part
                writer.WriteStartElement("requestor");
                writer.WriteElementString("name", sRelativeName);
                //Requestor Last Name
                //writer.WriteElementString("lastname", requestors.sPatientLastName);
                //Organization - Skipped
                //writer.WriteElementString("organization", requestors.);
                writer.WriteElementString("is_patient", requester.bAreYouPatient.ToString());
                writer.WriteElementString("relation", sRelationToPatient);
                writer.WriteElementString("email", requester.sRequesterEmailId);
                writer.WriteStartElement("address");
                //For Street,City,State,ZipCode
                writer.WriteElementString("street", requester.sAddStreetAddress);
                writer.WriteElementString("city", requester.sAddCity);
                writer.WriteElementString("state", requester.sAddState);
                writer.WriteElementString("zipcode", requester.sAddZipCode);
                writer.WriteEndElement();
                writer.WriteElementString("phone_number", requester.sPhoneNo);
                //writer.WriteElementString("fax_number", requestors.sPhoneNo);
                writer.WriteEndElement();
                //Requestor Part Ends Here
                #endregion

                writer.WriteStartElement("requested_information");
                writer.WriteStartElement("dates_of_service");
                if ((requester.dtRecordRangeStart != null) && (requester.dtRecordRangeEnd != null))
                {
                    writer.WriteElementString("start", requester.dtRecordRangeStart.Value.ToShortDateString());
                    writer.WriteElementString("end", requester.dtRecordRangeEnd.Value.ToShortDateString());
                }
                else
                {
                    writer.WriteElementString("start", string.Empty);
                    writer.WriteElementString("end", string.Empty);
                }
                writer.WriteEndElement();
                writer.WriteStartElement("types");
                foreach (string singleRecordType in requester.sSelectedRecordTypes)
                {
                    writer.WriteStartElement("item");
                    writer.WriteElementString("name", singleRecordType);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();

                //PDF in Base 64 Encoding
                writer.WriteElementString("pdf", requester.sPDF);
                writer.WriteEndElement();
                writer.Flush();

                #region Decrypt FTP Password
                MROLogger password = new MROLogger(_info);
                facility.sFTPPassword = password.DecryptString(facility.sFTPPassword);
                #endregion

                //XML File Genration 
                //File Name
                string sXMLFileName = facility.sFacilityName + "_" + requester.sPatientFirstName + requester.sPatientLastName + "_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xml";

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
                        MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in ftp", ex.Message, _info);
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
                        MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - XML Generation in sFTP", ex.Message, _info);
                    }
                }

                //AddRequestor(requestors, _info);

                //Send Email to Patient
                MROLogger passwordDecrypt = new MROLogger(_info);
                if (await SendEmail(requester, signedPDF, _info, passwordDecrypt))
                {
                        await SendROIEmail(requester, signedPDF, _info, passwordDecrypt);
                        return Ok(xmlString.ToString());
                }
                else
                    return Content("Email Not Sent");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
       
        private static async Task<bool> SendEmail(Requesters requestor, byte[] signedPDF, DBConnectionInfo _info,MROLogger passwordDecrypt)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(_info);
            FacilityLocationsRepository lRep = new FacilityLocationsRepository(_info);
            Facilities dbFacility = await fRep.Select(requestor.nFacilityID);
            FacilityLocations dbLocation = await lRep.Select(requestor.nLocationID);
            //Check if Facility is Allowed to Send Mail
            if (dbFacility.bRequestorEmailConfirm) 
            {

                #region Decrypt SMTP Password
                dbFacility.sSMTPPassword = passwordDecrypt.DecryptString(dbFacility.sSMTPPassword);
                #endregion

                //From 
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                message.From.Add(from);

                //To
                MailboxAddress to = new MailboxAddress(requestor.sPatientFirstName + " " + requestor.sPatientLastName, requestor.sRequesterEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Your Request for Medicals Records has been submitted successfully";
                BodyBuilder bodyBuilder = new BodyBuilder();

                dbLocation.sConfigLogoData = Regex.Replace(dbLocation.sConfigLogoData, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                byte[] locationLogo = Convert.FromBase64String(dbLocation.sConfigLogoData);
                var image = bodyBuilder.LinkedResources.Add("locationlogo",locationLogo);
                image.ContentId = MimeUtils.GenerateMessageId();           
                string htmlText = string.Format(@"<div style='border:1px solid black;padding: 25px;'>
    <p style='text-align: right;'>{1}&nbsp;</p><img src=""cid:{0}""><br/><br/>
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
</div>", image.ContentId, DateTime.Now.ToString("ddd, MMMM dd, h:mm tt"));
                bodyBuilder.HtmlBody = htmlText;
                bodyBuilder.Attachments.Add(requestor.sPatientFirstName + " " + requestor.sPatientLastName + " request.pdf", signedPDF);
                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                //TODO:
                //Get Port number
                //Make SSL true
                client.Connect(dbFacility.sSMTPUrl, 25, false);
                try
                {
                    client.Authenticate(dbFacility.sOutboundEmail, dbFacility.sSMTPPassword);
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Sending Email", ex.Message, _info);
                    return false;
                }
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return true;
            }
            return false;
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
        public async Task<ActionResult<string>> VerfiyRequestorEmail(EmailConfirmation requestor)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(_info);
            Facilities dbFacility = await fRep.Select(requestor.nFacilityID);

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
                MailboxAddress to = new MailboxAddress(requestor.sPatientFirstName + " " + requestor.sPatientLastName, requestor.sRequesterEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Here's Your 4 Digit Verification Code " + sOTP;
                BodyBuilder bodyBuilder = new BodyBuilder();
                string bodyText = "<h1>Hello " + requestor.sPatientFirstName + "!</h1><br/> Here's Your 4 Digit Email Verification Code " + sOTP;
                bodyBuilder.HtmlBody = bodyText;
                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                //GET Port number
                //Make SSL true
                client.Connect(dbFacility.sSMTPUrl, 25, false);
                try
                {
                    client.Authenticate(dbFacility.sOutboundEmail, dbFacility.sSMTPPassword);
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Email Verification", ex.Message, _info);
                    return Content(ex.Message);
                }
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return Ok(sOTP);
            }
            return "false";
        }
        #endregion

        #region Generate PDF
        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GeneratePDF(Requesters requestor)
        {
            if (ModelState.IsValid)
            {
                //if (string.IsNullOrEmpty(requestor.sSignatureData))
                //{
                //    byte[] pdfBytes = await GetFilledPDF(requestor, _info);
                //    return File(pdfBytes, "application/pdf");
                //}
                //else
                //{ 
                    byte[] pdfBytes = await GetSignedPDF(requestor);
                    return File(pdfBytes, "application/pdf");
                //}
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
        }
        private async Task<byte[]> GetSignedPDF(Requesters requester)
        {
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            allFields.Add("MROPatientFullName", requester.sPatientFirstName + " " + requester.sPatientMiddleName + " " + requester.sPatientLastName);
            allFields.Add("MROPatientFirstName", requester.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requester.sPatientMiddleName);
            allFields.Add("MROPatientLastName", requester.sPatientLastName);
            allFields.Add("MROPatientDOB", requester.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requester.sRequesterEmailId);
            allFields.Add("MROAddZipCode", requester.sAddZipCode);
            allFields.Add("MROAddCity", requester.sAddCity);
            allFields.Add("MROAddState", requester.sAddState);
            allFields.Add("MROAddStreetAddress", requester.sAddStreetAddress);


            //Record Types
            for (int counter = 0; counter < requester.sSelectedRecordTypes.Length; counter++)
            {
                allFields.Add(requester.sSelectedRecordTypes[counter] + "=1", requester.sSelectedRecordTypes[counter] == requester.sSelectedRecordTypes[counter] ? "On" : "");
            }

            //Primary Reasons
            for (int counter = 0; counter < requester.sSelectedPrimaryReasons.Length; counter++)
            {
                allFields.Add(requester.sSelectedPrimaryReasons[counter] + "=1", requester.sSelectedPrimaryReasons[counter] == requester.sSelectedPrimaryReasons[counter] ? "On" : "");
            }
            //Sensitive Info
            for (int counter = 0; counter < requester.selectedSensitiveInfo.Length; counter++)
            {
                allFields.Add(requester.selectedSensitiveInfo[counter] + "=1", requester.selectedSensitiveInfo[counter] == requester.selectedSensitiveInfo[counter] ? "On" : "");
            }

            //Shipment Types 
            for (int counter = 0; counter < requester.sSelectedShipmentTypes.Length; counter++)
            {
                allFields.Add(requester.sSelectedShipmentTypes[counter] + "=1", requester.sSelectedShipmentTypes[counter] == requester.sSelectedShipmentTypes[counter] ? "On" : "");
            }

            //Shipment Type Related Fields

            //Shipment Type Mail Address
            allFields.Add("MROSTAddZipCode", requester.sSTAddZipCode);
            allFields.Add("MROSTAddState", requester.sSTAddState);
            allFields.Add("MROSTAddCity", requester.sSTAddCity);
            allFields.Add("MROSTAddStreetAddress", requester.sSTAddStreetAddress);
            allFields.Add("MROSTAddApartment", requester.sSTAddApartment);
            allFields.Add("MROSTCompleteAddress", requester.sSTAddApartment + ", " 
                                                + requester.sSTAddStreetAddress + ", "
                                                + requester.sSTAddCity + ", "
                                                + requester.sSTAddState + ", "
                                                + requester.sSTAddZipCode);

            //Shipment Type Email
            allFields.Add("MROSTEmailId", requester.sSTEmailId);
            //Shipment Type Fax Number
            allFields.Add("MROSTFaxNo", requester.sSTFaxNo);




            allFields.Add("MROPatientTelephoneNo", requester.sPhoneNo);

            //Release To 'MROReleaseToMyself' 'MROReleaseToFamilyCaregiver' 'MROReleaseToDoctor' 'MROReleaseToThirdParty'
            allFields.Add(requester.sReleaseTo + "=1", requester.sReleaseTo == requester.sReleaseTo ? "On" : "");
          


            FacilityLocationsRepository locRepo = new FacilityLocationsRepository(_info);
            FacilityLocations location = await locRepo.Select(requester.nLocationID);
            location.sAuthTemplate = location.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
            byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);

            byte[] byteArrayToReturn = new LocationAuthorizationDocumentController().ReplaceFieldKeywordsWithValue(pdfByteArray, allFields,requester ,out string sReplaceFieldsList);

            allFields.Clear();

            if (!string.IsNullOrEmpty(requester.sSignatureData))
            {
                //Signing the Document
                Doc theDoc = new Doc();

                theDoc.Read(byteArrayToReturn);
                string removeDataTag = requester.sSignatureData.Replace("data:image/png;base64,", string.Empty);
                byte[] signatureByteArray = Convert.FromBase64String(removeDataTag);
                Image image2 = Image.FromStream(new MemoryStream(signatureByteArray));

                MROHelperRepository helperRepo = new MROHelperRepository(_info);
                MROHelper helper = await helperRepo.Select(1);
                helper.sWhitebgimg = helper.sWhitebgimg.Replace("data:image/png;base64,", string.Empty);
                byte[] data = Convert.FromBase64String(helper.sWhitebgimg);

                System.Drawing.Image canvas = Bitmap.FromStream(new MemoryStream(data, 0, data.Length));
                Graphics gra = Graphics.FromImage(canvas);
                Bitmap smallImg = new Bitmap(image2);
                gra.DrawImage(smallImg, new Point(0, 0));


                var mssignaturewithbg = new MemoryStream();
                canvas.Save(mssignaturewithbg, canvas.RawFormat);
                mssignaturewithbg.ToArray();


                XImage theImg = new XImage();
                theImg.SetStream(mssignaturewithbg);
                theDoc.Rect.String = theImg.Selection.String;
                theDoc.Rect.Magnify(0.15, 0.2);
                theDoc.Rect.Position(theDoc.Form["MROSignature"].Rect.Left, theDoc.Form["MROSignature"].Rect.Bottom);
                theDoc.AddImageObject(theImg, false);
                theImg.Clear();

                theDoc.Form.Stamp();

                byte[] pdfBytes = theDoc.GetData();
                return pdfBytes;
            }
            
            return byteArrayToReturn;
        }
        //private async static Task<byte[]> GetFilledPDF(Requesters requester, DBConnectionInfo _info)
        //{
        //    Dictionary<string, string> allFields = new Dictionary<string, string>();
        //    allFields.Add("MROPatientFullName", requester.sPatientFirstName + " " + requester.sPatientMiddleName + " " + requester.sPatientLastName);
        //    allFields.Add("MROPatientFirstName", requester.sPatientFirstName);
        //    allFields.Add("MROPatientMiddleInitial", requester.sPatientMiddleName);
        //    allFields.Add("MROPatientLastName", requester.sPatientLastName);
        //    allFields.Add("MROPatientDOB", requester.dtPatientDOB.Value.ToShortDateString());
        //    allFields.Add("MROPEmailId", requester.sRequesterEmailId);
        //    allFields.Add("MROAddZipCode", requester.sAddZipCode);
        //    allFields.Add("MROAddCity", requester.sAddCity);
        //    allFields.Add("MROAddState", requester.sAddState);
        //    allFields.Add("MROAddStreetAddress", requester.sAddStreetAddress);

        //    //Record Types
        //    for (int counter = 0; counter < requester.sSelectedRecordTypes.Length; counter++)
        //    {
        //        allFields.Add(requester.sSelectedRecordTypes[counter] + "=1", requester.sSelectedRecordTypes[counter] == requester.sSelectedRecordTypes[counter] ? "On" : "");
        //    }

        //    //Primary Reasons
        //    for (int counter = 0; counter < requester.sSelectedPrimaryReasons.Length; counter++)
        //    {
        //        allFields.Add(requester.sSelectedPrimaryReasons[counter] + "=1", requester.sSelectedPrimaryReasons[counter] == requester.sSelectedPrimaryReasons[counter] ? "On" : "");
        //    }
        //    //Sensitive Info
        //    for (int counter = 0; counter < requester.selectedSensitiveInfo.Length; counter++)
        //    {
        //        allFields.Add(requester.selectedSensitiveInfo[counter] + "=1", requester.selectedSensitiveInfo[counter] == requester.selectedSensitiveInfo[counter] ? "On" : "");
        //    }
        //    //Shipment Types 
        //    for (int counter = 0; counter < requester.sSelectedShipmentTypes.Length; counter++)
        //    {
        //        allFields.Add(requester.sSelectedShipmentTypes[counter] + "=1", requester.sSelectedShipmentTypes[counter] == requester.sSelectedShipmentTypes[counter] ? "On" : "");
        //    }

        //    //Shipment Type Related Fields

        //    //Shipment Type Mail Address
        //    allFields.Add("MROSTAddZipCode", requester.sSTAddZipCode);
        //    allFields.Add("MROSTAddState", requester.sSTAddState);
        //    allFields.Add("MROSTAddCity", requester.sSTAddCity);
        //    allFields.Add("MROSTAddStreetAddress", requester.sSTAddStreetAddress);
        //    allFields.Add("MROSTAddApartment", requester.sSTAddApartment);
        //    allFields.Add("MROSTCompleteAddress", requester.sSTAddApartment + ", "
        //                                        + requester.sSTAddStreetAddress + ", "
        //                                        + requester.sSTAddCity + ", "
        //                                        + requester.sSTAddState + ", "
        //                                        + requester.sSTAddZipCode);

        //    //Shipment Type Email
        //    allFields.Add("MROSTEmailId", requester.sSTEmailId);
        //    //Shipment Type Fax Number
        //    allFields.Add("MROSTFaxNo", requester.sSTFaxNo);


        //    allFields.Add("MROPatientTelephoneNo", requester.sPhoneNo);

        //    //Release To
        //    allFields.Add(requester.sReleaseTo + "=1", requester.sReleaseTo == "MROReleaseToMyself" ? "On" : "");
        //    allFields.Add(requester.sReleaseTo + "=1", requester.sReleaseTo == "MROReleaseToFamilyCaregiver" ? "On" : "");
        //    allFields.Add(requester.sReleaseTo + "=1", requester.sReleaseTo == "MROReleaseToDoctor" ? "On" : "");
        //    allFields.Add(requester.sReleaseTo + "=1", requester.sReleaseTo == "MROReleaseToThirdParty" ? "On" : "");


        //    FacilityLocationsRepository locRepo = new FacilityLocationsRepository(_info);
        //    FacilityLocations location = await locRepo.Select(requester.nLocationID);
        //    location.sAuthTemplate = location.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
        //    byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);
        //    byte[] byteArrayToReturn = new LocationAuthorizationDocumentController().ReplaceFieldKeywordsWithValue(pdfByteArray, allFields,requester,out string sReplaceFieldsList);

        //    allFields.Clear();
        //    return byteArrayToReturn;
        //}
        #endregion

        #region ROI Email
        private static async Task<bool> SendROIEmail(Requesters requestor, byte[] signedPDF, DBConnectionInfo _info, MROLogger passwordDecrypt)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(_info);
            FacilityLocationsRepository lRep = new FacilityLocationsRepository(_info);
            Facilities dbFacility = await fRep.Select(requestor.nFacilityID);
            FacilityLocations dbLocation = await lRep.Select(requestor.nLocationID);
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
                MailboxAddress to = new MailboxAddress(requestor.sPatientFirstName + " " + requestor.sPatientLastName, requestor.sRequesterEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Request Confirmation";
                BodyBuilder bodyBuilder = new BodyBuilder();

                helper.sMROEmailFooterImage = Regex.Replace(helper.sMROEmailFooterImage, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                byte[] footerLogo = Convert.FromBase64String(helper.sMROEmailFooterImage);
                var image = bodyBuilder.LinkedResources.Add("footerlogo", footerLogo);
                image.ContentId = MimeUtils.GenerateMessageId();
                var sFullName = requestor.sPatientFirstName + " " + requestor.sPatientLastName;
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
                                            sFullName,requestor.nRequesterID, 
                                            dbFacility.sFacilityName, 
                                            image.ContentId);

                bodyBuilder.HtmlBody = htmlText;
                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                //TODO:
                //Get Port number
                //Make SSL true
                client.Connect(dbFacility.sSMTPUrl, 25, false);
                try
                {
                    client.Authenticate(dbFacility.sOutboundEmail, dbFacility.sSMTPPassword);
                }
                catch (Exception ex)
                {
                    MROLogger.LogExceptionRecords(ExceptionStatus.Error.ToString(), "Submit Form - Send ROI Email", ex.Message, _info);
                    return false;
                }
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return true;
            }
            return false;
        }
        #endregion
    }
}
