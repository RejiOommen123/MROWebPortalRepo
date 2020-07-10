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
                return Content(ex.Message);
            }
        }
        #endregion

        #region  Add Requestor to DB, Generate XML, Send Attachment Email and ROI Email
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GenerateXML([FromBody] Requesters requestors)
        {
            if (ModelState.IsValid)
            {
                RequestersController requestersController = new RequestersController(_info);
                await requestersController.AddRequester(requestors);

                byte[] signedPDF = await GetSignedPDF(requestors);
                requestors.sPDF = Convert.ToBase64String(signedPDF);
                requestors.sPDF = "data:application/pdf;base64," + requestors.sPDF;
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(requestors.nFacilityID);
                FacilityLocationsRepository locaFac = new FacilityLocationsRepository(_info);
                FacilityLocations location = await locaFac.Select(requestors.nLocationID);
                //var sPatientDeceased = requestors.bIsPatientDeceased ? "Yes" : "No";
                var sMddleInitials = string.IsNullOrEmpty(requestors.sPatientMiddleInitial) ? "" : requestors.sPatientMiddleInitial;
                var sAreYouPatient = requestors.bAreYouPatient ? "No" : "Yes";
                var sRelativeName = string.IsNullOrEmpty(requestors.sRelativeName) ? "" : requestors.sRelativeName;
                var sRelationToPatient = string.IsNullOrEmpty(requestors.sRelationToPatient) ? "" : requestors.sRelationToPatient;
                var sConfirmReport = requestors.bConfirmReport ? "Opted to be mailed to registered Email ID" : "Not Opted";
                var sOtherReasons = string.IsNullOrEmpty(requestors.sOtherReasons) ? "" : requestors.sOtherReasons;
                var sComments = string.IsNullOrEmpty(requestors.sFeedbackComment) ? "" : requestors.sFeedbackComment;


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
                writer.WriteElementString("date_required_by", requestors.dtDeadline.ToString());
                writer.WriteElementString("reason", sOtherReasons);
                writer.WriteElementString("comments", sComments);
                writer.WriteElementString("expiration", requestors.dtAuthExpire.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("patient");
                writer.WriteElementString("firstname", requestors.sPatientFirstName);
                writer.WriteElementString("lastname", requestors.sPatientLastName);
                writer.WriteElementString("middle_name", sMddleInitials);
                writer.WriteElementString("dob", requestors.dtPatientDOB.Value.ToShortDateString());
                writer.WriteStartElement("address");
                //For Street,City,State,ZipCode
                writer.WriteElementString("street", requestors.sAddStreetAddress);
                writer.WriteElementString("city", requestors.sAddCity);
                writer.WriteElementString("state", requestors.sAddState);
                writer.WriteElementString("zipcode", requestors.sAddZipCode);
                writer.WriteEndElement();
                writer.WriteElementString("phone_number", requestors.sPhoneNo);
                writer.WriteEndElement();

                //Requestor - Part
                #region Requestor Part
                writer.WriteStartElement("requestor");
                writer.WriteElementString("name", sRelativeName);
                //Requestor Last Name
                //writer.WriteElementString("lastname", requestors.sPatientLastName);
                //Organization - Skipped
                //writer.WriteElementString("organization", requestors.);
                writer.WriteElementString("is_patient", requestors.bAreYouPatient.ToString());
                writer.WriteElementString("relation", sRelationToPatient);
                writer.WriteElementString("email", requestors.sRequesterEmailId);
                writer.WriteStartElement("address");
                //For Street,City,State,ZipCode
                writer.WriteElementString("street", requestors.sAddStreetAddress);
                writer.WriteElementString("city", requestors.sAddCity);
                writer.WriteElementString("state", requestors.sAddState);
                writer.WriteElementString("zipcode", requestors.sAddZipCode);
                writer.WriteEndElement();
                writer.WriteElementString("phone_number", requestors.sPhoneNo);
                //writer.WriteElementString("fax_number", requestors.sPhoneNo);
                writer.WriteEndElement();
                //Requestor Part Ends Here
                #endregion

                writer.WriteStartElement("requested_information");
                writer.WriteStartElement("dates_of_service");
                writer.WriteElementString("start", requestors.dtRecordRangeStart.Value.ToShortDateString());
                writer.WriteElementString("end", requestors.dtRecordRangeEnd.Value.ToShortDateString());
                writer.WriteEndElement();
                writer.WriteStartElement("types");
                foreach (string singleRecordType in requestors.sSelectedRecordTypes)
                {
                    writer.WriteStartElement("item");
                    writer.WriteElementString("name", singleRecordType);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();

                //PDF in Base 64 Encoding
                writer.WriteElementString("pdf", requestors.sPDF);
                writer.WriteEndElement();
                writer.Flush();

                #region Decrypt FTP Password
                MROLogger password = new MROLogger(_info);
                facility.sFTPPassword = password.DecryptString(facility.sFTPPassword);
                #endregion

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(facility.sFTPUrl + facility.sFacilityName+"_"+requestors.sPatientFirstName+requestors.sPatientLastName+"_"+ DateTime.Now.ToString("MM-dd-yyyy") + ".xml");

                #region Request Params
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(facility.sFTPUsername,facility.sFTPPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                #endregion

                byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());

                //Upload file
                try {
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
                catch (Exception ex) {
                    //return Content(ex.Message);
                }

                //AddRequestor(requestors, _info);

                //Send Email to Patient
                MROLogger passwordDecrypt = new MROLogger(_info);
                if (await SendEmail(requestors, signedPDF, _info, passwordDecrypt))
                {
                        await SendROIEmail(requestors, signedPDF, _info, passwordDecrypt);
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
        //private static int AddRequestor(Requesters requester,DBConnectionInfo _info) 
        //{

        //        try
        //        {
        //        #region Data Addition ! From UI
        //        requester.dtLastUpdate = DateTime.Now;
        //        #endregion

        //        #region Array Processing
        //        var PRArray = string.Join(",", requester.sSelectedPrimaryReasons);
        //        var SRArray = string.Join(",", requester.sSelectedRecordTypes);
        //        var STArray = string.Join(",", requester.sSelectedShipmentTypes);
        //        var SIArray = string.Join(",", requester.selectedSensitiveInfo);
        //        var relativeFileArray = string.Join(",", requester.sRelativeFileArray);
        //        requester.sSelectedPrimaryReasons = new string[] { PRArray };
        //        requester.sSelectedRecordTypes = new string[] { SRArray };
        //        requester.sSelectedShipmentTypes = new string[] { STArray };
        //        requester.selectedSensitiveInfo = new string[] { SIArray };
        //        requester.sRelativeFileArray = new string[] { relativeFileArray };
        //        #endregion

        //        RequestersRepository requestorsFac = new RequestersRepository(_info);
        //            int GeneratedID = (int)requestorsFac.Insert(requester);
        //            return GeneratedID;
        //        }
        //        catch (Exception ex)
        //        {
        //                return 0;
        //        }
        //}
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
                catch (Exception e)
                {
                    return Content(e.Message);
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
                if (string.IsNullOrEmpty(requestor.sSignatureData))
                {
                    byte[] pdfBytes = await GetFilledPDF(requestor, _info);
                    return File(pdfBytes, "application/pdf");
                }
                else
                { 
                    byte[] pdfBytes = await GetSignedPDF(requestor);
                    return File(pdfBytes, "application/pdf");
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
        private async Task<byte[]> GetSignedPDF(Requesters requestor)
        {
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            allFields.Add("MROPatientFullName", requestor.sPatientFirstName + " " + requestor.sPatientMiddleInitial + " " + requestor.sPatientLastName);
            allFields.Add("MROPatientFirstName", requestor.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requestor.sPatientMiddleInitial);
            allFields.Add("MROPatientLastName", requestor.sPatientLastName);
            allFields.Add("MROPatientDOB", requestor.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requestor.sRequesterEmailId);
            allFields.Add("MROAddZipCode", requestor.sAddZipCode);
            allFields.Add("MROAddCity", requestor.sAddCity);
            allFields.Add("MROAddState", requestor.sAddState);
            allFields.Add("MROAddStreetAddress", requestor.sAddStreetAddress);

            //Record Types
            for (int counter = 0; counter < requestor.sSelectedRecordTypes.Length; counter++)
            {
                allFields.Add(requestor.sSelectedRecordTypes[counter] + "=1", requestor.sSelectedRecordTypes[counter] == requestor.sSelectedRecordTypes[counter] ? "On" : "");
            }

            //Primary Reasons
            for (int counter = 0; counter < requestor.sSelectedPrimaryReasons.Length; counter++)
            {
                allFields.Add(requestor.sSelectedPrimaryReasons[counter] + "=1", requestor.sSelectedPrimaryReasons[counter] == requestor.sSelectedPrimaryReasons[counter] ? "On" : "");
            }
            //Sensitive Info
            for (int counter = 0; counter < requestor.selectedSensitiveInfo.Length; counter++)
            {
                allFields.Add(requestor.selectedSensitiveInfo[counter] + "=1", requestor.selectedSensitiveInfo[counter] == requestor.selectedSensitiveInfo[counter] ? "On" : "");
            }
            allFields.Add("MROPatientTelephoneNo", requestor.sPhoneNo);

            //Release To
            allFields.Add(requestor.sReleaseTo + "=1", requestor.sReleaseTo == "MROReleaseToMe" ? "On" : "");

            if (!string.IsNullOrEmpty(requestor.sSTFaxCompAdd))
                allFields.Add("MROSTFaxCompAdd", requestor.sSTFaxCompAdd);
            if (!string.IsNullOrEmpty(requestor.sSTEmailId))
                allFields.Add("MROSTEmailId", requestor.sSTEmailId);
            //if (!string.IsNullOrEmpty(requestor.sSTConfirmEmailId))
            //    allFields.Add("MROSTConfirmEmailId", requestor.sSTConfirmEmailId);
            if (!string.IsNullOrEmpty(requestor.sSTMailCompAdd))
                allFields.Add("MROSTMailCompAdd", requestor.sSTMailCompAdd);
            if (requestor.dtPickUp != null)
                allFields.Add("MROSTPike", requestor.dtPickUp.Value.ToShortDateString());

            FacilityLocationsRepository locRepo = new FacilityLocationsRepository(_info);
            FacilityLocations location = await locRepo.Select(requestor.nLocationID);
            location.sAuthTemplate = location.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
            byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);
            byte[] byteArrayToReturn = new LocationAuthorizationDocumentController().ReplaceFieldKeywordsWithValueWOStamp(pdfByteArray, allFields,requestor ,out string sReplaceFieldsList);

            allFields.Clear();

            Doc theDoc = new Doc();

            theDoc.Read(byteArrayToReturn);
            string removeDataTag = requestor.sSignatureData.Replace("data:image/png;base64,", string.Empty);
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
        private async static Task<byte[]> GetFilledPDF(Requesters requestor, DBConnectionInfo _info)
        {
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            allFields.Add("MROPatientFullName", requestor.sPatientFirstName + " " + requestor.sPatientMiddleInitial + " " + requestor.sPatientLastName);
            allFields.Add("MROPatientFirstName", requestor.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requestor.sPatientMiddleInitial);
            allFields.Add("MROPatientLastName", requestor.sPatientLastName);
            allFields.Add("MROPatientDOB", requestor.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requestor.sRequesterEmailId);
            allFields.Add("MROAddZipCode", requestor.sAddZipCode);
            allFields.Add("MROAddCity", requestor.sAddCity);
            allFields.Add("MROAddState", requestor.sAddState);
            allFields.Add("MROAddStreetAddress", requestor.sAddStreetAddress);

            //Record Types
            for (int counter = 0; counter < requestor.sSelectedRecordTypes.Length; counter++)
            {
                allFields.Add(requestor.sSelectedRecordTypes[counter] + "=1", requestor.sSelectedRecordTypes[counter] == requestor.sSelectedRecordTypes[counter] ? "On" : "");
            }

            //Primary Reasons
            for (int counter = 0; counter < requestor.sSelectedPrimaryReasons.Length; counter++)
            {
                allFields.Add(requestor.sSelectedPrimaryReasons[counter] + "=1", requestor.sSelectedPrimaryReasons[counter] == requestor.sSelectedPrimaryReasons[counter] ? "On" : "");
            }
            //Sensitive Info
            for (int counter = 0; counter < requestor.selectedSensitiveInfo.Length; counter++)
            {
                allFields.Add(requestor.selectedSensitiveInfo[counter] + "=1", requestor.selectedSensitiveInfo[counter] == requestor.selectedSensitiveInfo[counter] ? "On" : "");
            }
            allFields.Add("MROPatientTelephoneNo", requestor.sPhoneNo);

            //Release To
            allFields.Add(requestor.sReleaseTo + "=1", requestor.sReleaseTo == "MROReleaseToMe" ? "On" : "");
            if (!string.IsNullOrEmpty(requestor.sSTFaxCompAdd))
                allFields.Add("MROSTFaxCompAdd", requestor.sSTFaxCompAdd);
            if (!string.IsNullOrEmpty(requestor.sSTEmailId))
                allFields.Add("MROSTEmailId", requestor.sSTEmailId);
            //if (!string.IsNullOrEmpty(requestor.sSTConfirmEmailId))
            //    allFields.Add("MROSTConfirmEmailId", requestor.sSTConfirmEmailId);
            if (!string.IsNullOrEmpty(requestor.sSTMailCompAdd))
                allFields.Add("MROSTMailCompAdd", requestor.sSTMailCompAdd);
            if (requestor.dtPickUp != null)
                allFields.Add("MROSTPike", requestor.dtPickUp.Value.ToShortDateString());

            FacilityLocationsRepository locRepo = new FacilityLocationsRepository(_info);
            FacilityLocations location = await locRepo.Select(requestor.nLocationID);
            location.sAuthTemplate = location.sAuthTemplate.Replace("data:application/pdf;base64,", string.Empty);
            byte[] pdfByteArray = Convert.FromBase64String(location.sAuthTemplate);
            byte[] byteArrayToReturn = new LocationAuthorizationDocumentController().ReplaceFieldKeywordsWithValue(pdfByteArray, allFields,requestor,out string sReplaceFieldsList);

            allFields.Clear();
            return byteArrayToReturn;
        }
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
</ div > ", DateTime.Now.ToString("ddd, MMMM dd, h:mm tt"), sFullName,requestor.nRequesterID, dbFacility.sFacilityName, image.ContentId);

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
