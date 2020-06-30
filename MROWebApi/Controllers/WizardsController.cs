using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using WebSupergoo.ABCpdf11;
using System.Net;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text.RegularExpressions;

namespace MROWebApi.Controllers
{
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

        #region Generate XML
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GenerateXML([FromBody] Requestors requestors)
        {
            if (ModelState.IsValid)
            {
                byte[] signedPDF = await GetSignedPDF(requestors);
                requestors.sPDF = Convert.ToBase64String(signedPDF);
                requestors.sPDF = "data:application/pdf;base64," + requestors.sPDF;
                FacilitiesRepository rpFac = new FacilitiesRepository(_info);
                Facilities facility = await rpFac.Select(requestors.nFacilityID);
                FacilityLocationsRepository locaFac = new FacilityLocationsRepository(_info);
                FacilityLocations location = await locaFac.Select(requestors.nLocationID);
                var sPatientDeceased = requestors.bIsPatientDeceased ? "Yes" : "No";
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
                writer.WriteElementString("email", requestors.sPatientEmailId);
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
                    //writer.WriteElementString("include", singleRecordType);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();

                //PDF in Base 64 Encoding
                writer.WriteElementString("pdf", requestors.sPDF);
                writer.WriteEndElement();
                writer.Flush();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://waws-prod-blu-117.ftp.azurewebsites.windows.net/site/wwwroot/xmls/" + requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml");

                #region Request Params
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(@"devmroportalcust1__DEV\$devmroportalcust1__DEV", "u4vruzYx1oAq2ush4afofyiHZwHP9QylGaZsrWhw7C6xSQEMyzdb1JKzM4A7");
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                #endregion

                byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());

                //Upload file
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                //Send Email to Patient
                if (await SendEmail(requestors, signedPDF, _info))
                    return Ok(xmlString.ToString());
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
        private static async Task<bool> SendEmail(Requestors requestor, byte[] signedPDF, DBConnectionInfo _info)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(_info);
            Facilities dbFacility = await fRep.Select(requestor.nFacilityID);
            //Check if Facility is Allowed to Send Mail
            if (dbFacility.bRequestorEmailConfirm) 
            {
                //From 
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                message.From.Add(from);

                //To
                MailboxAddress to = new MailboxAddress(requestor.sPatientFirstName + " " + requestor.sPatientLastName, requestor.sConfirmEmailId);
                message.To.Add(to);

                //Subject
                message.Subject = "Your Request for Medicals Records has been Received";
                BodyBuilder bodyBuilder = new BodyBuilder();
                string bodyText = "<h1>Hello " + requestor.sPatientFirstName + "!</h1><br/>Your request has been recived.<br/>Please Find Attached Request PDF";
                bodyBuilder.HtmlBody = bodyText;
                bodyBuilder.Attachments.Add(requestor.sPatientFirstName + " " + requestor.sPatientLastName + " request.pdf", signedPDF);
                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
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

            if (dbFacility.bRequestorEmailConfirm)
            {
                var sOTP = GenerateRandomNo().ToString();
                //From 
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
                message.From.Add(from);

                //To
                MailboxAddress to = new MailboxAddress(requestor.sPatientFirstName + " " + requestor.sPatientLastName, requestor.sPatientEmailId);
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
        public async Task<IActionResult> GeneratePDF(Requestors requestor)
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

        private async Task<byte[]> GetSignedPDF(Requestors requestor)
        {
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            allFields.Add("MROPatientFullName", requestor.sPatientFirstName + " " + requestor.sPatientMiddleInitial + " " + requestor.sPatientLastName);
            allFields.Add("MROPatientFirstName", requestor.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requestor.sPatientMiddleInitial);
            allFields.Add("MROPatientLastName", requestor.sPatientLastName);
            allFields.Add("MROPatientDOB", requestor.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requestor.sPatientEmailId);
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
            if (!string.IsNullOrEmpty(requestor.sSTConfirmEmailId))
                allFields.Add("MROSTConfirmEmailId", requestor.sSTConfirmEmailId);
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
        private async static Task<byte[]> GetFilledPDF(Requestors requestor, DBConnectionInfo _info)
        {
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            allFields.Add("MROPatientFullName", requestor.sPatientFirstName + " " + requestor.sPatientMiddleInitial + " " + requestor.sPatientLastName);
            allFields.Add("MROPatientFirstName", requestor.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requestor.sPatientMiddleInitial);
            allFields.Add("MROPatientLastName", requestor.sPatientLastName);
            allFields.Add("MROPatientDOB", requestor.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requestor.sPatientEmailId);
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
            if (!string.IsNullOrEmpty(requestor.sSTConfirmEmailId))
                allFields.Add("MROSTConfirmEmailId", requestor.sSTConfirmEmailId);
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
    }
}
