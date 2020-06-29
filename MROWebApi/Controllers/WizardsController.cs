using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using WebSupergoo.ABCpdf11.Elements;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using System.Net;
using System.Text;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class WizardsController : ControllerBase
    {
        private readonly DBConnectionInfo _info;

        #region Wizards Constructor
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
                object Wizard_Config = await  fieldsRepository.GetWizardConfigurationAsync(nFacilityID,nFacilityLocationID);
                return Wizard_Config;

                //var oWizardConfigFields = Function(wizard_config.ofield);
                //return OptimizeObject(unoptimized_wizard_config);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex;
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
                //throw ex;
                return ex;
            }
        }
        #endregion

        #region Logo & Bg for Location
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
                return ex;
            }
        }
        #endregion

        #region Send Only Required Values
        //private object OptimizeObject(object unoptimized_wizard_config) {

        //    object wizard_config = null;

        //    //var oFields = unoptimized_wizard_config.

        //    return wizard_config;
        //}
        #endregion

        #region Commented for XML Part
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public  async Task<IActionResult> GenerateXML([FromBody] Requestors requestors)
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
            //Path.Combine(Directory.GetCurrentDirectory(), requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml")
            using XmlWriter writer = XmlWriter.Create(xmlString, xmlWriterSetting);
            writer.WriteStartElement("request");
            writer.WriteStartElement("facility");
            //For facility Code,Name,ID
            //writer.WriteElementString("code", facility.sCode);
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



            //Requestor
            #region Requestor
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
            //Fax Number Are WE Taking ?
            //writer.WriteElementString("fax_number", requestors.sPhoneNo);
            writer.WriteEndElement();
            //requestor ends here
            #endregion


            writer.WriteStartElement("requested_information");
            writer.WriteStartElement("dates_of_service");
            writer.WriteElementString("start", requestors.dtRecordRangeStart.Value.ToShortDateString());
            writer.WriteElementString("end", requestors.dtRecordRangeEnd.Value.ToShortDateString());
            writer.WriteEndElement();
            writer.WriteStartElement("types");
            //Add Items Here
            foreach (string singleRecordType in requestors.sSelectedRecordTypes)
            {
                writer.WriteStartElement("item");
                writer.WriteElementString("name", singleRecordType);
                //writer.WriteElementString("include", singleRecordType);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();

            //PDF Here
            //Get PDF & Show in Base 64 Encoding
            writer.WriteElementString("pdf", requestors.sPDF);
            writer.WriteEndElement();
            writer.Flush();
            //using (var client = new WebClient())
            //{

            //    client.Credentials = new NetworkCredential(@"devmroportalcust1__DEV\$devmroportalcust1__DEV", "u4vruzYx1oAq2ush4afofyiHZwHP9QylGaZsrWhw7C6xSQEMyzdb1JKzM4A7");
            //    client.UploadFile("ftp://waws-prod-blu-117.ftp.azurewebsites.windows.net/site/wwwroot/xmls/" + Path.Combine(Directory.GetCurrentDirectory(), requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml") , WebRequestMethods.Ftp.UploadFile, Path.Combine(Directory.GetCurrentDirectory(), requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml"));

            //}

            FtpWebRequest request =
                        (FtpWebRequest)WebRequest.Create("ftp://waws-prod-blu-117.ftp.azurewebsites.windows.net/site/wwwroot/xmls/" + requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml");

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(@"devmroportalcust1__DEV\$devmroportalcust1__DEV", "u4vruzYx1oAq2ush4afofyiHZwHP9QylGaZsrWhw7C6xSQEMyzdb1JKzM4A7");
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            //FileStream stream = System.IO.File.OpenRead(xmlString.ToString());
            byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());
            //byte[] buffer = new byte[stream.Length];

            //stream.Read(buffer, 0, buffer.Length);
            //stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            //return Content(xmlString, "text/xml", System.Text.Encoding.UTF8);
            return Ok(xmlString.ToString());
        }

        //private static void StoreXMLFTP(Requestors requestors, StringBuilder xmlString)
        //{
        //    FtpWebRequest request =
        //                (FtpWebRequest)WebRequest.Create("ftp://waws-prod-blu-117.ftp.azurewebsites.windows.net/site/wwwroot/xmls/" + requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml");

        //    request.Method = WebRequestMethods.Ftp.UploadFile;
        //    request.Credentials = new NetworkCredential(@"devmroportalcust1__DEV\$devmroportalcust1__DEV", "u4vruzYx1oAq2ush4afofyiHZwHP9QylGaZsrWhw7C6xSQEMyzdb1JKzM4A7");
        //    request.UsePassive = true;
        //    request.UseBinary = true;
        //    request.KeepAlive = false;

        //    //Load the file
        //    //FileStream stream = System.IO.File.OpenRead(xmlString.ToString());
        //    byte[] buffer = Encoding.ASCII.GetBytes(xmlString.ToString());
        //    //byte[] buffer = new byte[stream.Length];

        //    //stream.Read(buffer, 0, buffer.Length);
        //    //stream.Close();

        //    //Upload file
        //    Stream reqStream = request.GetRequestStream();
        //    reqStream.Write(buffer, 0, buffer.Length);
        //    reqStream.Close();
        //}
        #endregion

        #region Get Signed PDF
        private async Task<byte[]> GetSignedPDF(Requestors requestor)
        {
            //string sAppRoot = GetApplicationRoot();
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            //allFields.Add("MROAreYouPatient", requestor.bAreYouPatient.ToString());
            allFields.Add("MROPatientFullName", requestor.sPatientFirstName + " " + requestor.sPatientMiddleInitial + " " + requestor.sPatientLastName);
            allFields.Add("MROPatientFirstName", requestor.sPatientFirstName);
            allFields.Add("MROPatientMiddleInitial", requestor.sPatientMiddleInitial);
            allFields.Add("MROPatientLastName", requestor.sPatientLastName);
            allFields.Add("MROPatientDOB", requestor.dtPatientDOB.Value.ToShortDateString());
            allFields.Add("MROPEmailId", requestor.sPatientEmailId);
            //allFields.Add("MROConfirmReport", requestor.bConfirmReport.ToString());
            allFields.Add("MROAddZipCode", requestor.sAddZipCode);
            allFields.Add("MROAddCity", requestor.sAddCity);
            allFields.Add("MROAddState", requestor.sAddState);
            allFields.Add("MROAddStreetAddress", requestor.sAddStreetAddress);
            //allFields.Add("MROTFDateRange", requestor.dtRecordRangeStart);

            //Record Type
            for (int counter = 0; counter < requestor.sSelectedRecordTypes.Length; counter++)
            {
                allFields.Add(requestor.sSelectedRecordTypes[counter] + "=1", requestor.sSelectedRecordTypes[counter] == requestor.sSelectedRecordTypes[counter] ? "On" : "");
            }

            //Primary Reason
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




            allFields.Add(requestor.sReleaseTo + "=1", requestor.sReleaseTo == "MROReleaseToMe" ? "On" : "");

            //allFields.Add("MROAuthExpireDateAfterNMonths", requestor.sPatientFirstName);
            //allFields.Add("MROAuthExpireDateSpecificDate", requestor.sPatientFirstName);
            //allFields.Add("MROAuthExpireDateEventOccurs", requestor.sPatientFirstName);
            //allFields.Add("MRORequestDeadline", requestor.sPatientFirstName);
            //allFields.Add("MRORequestDeadlineDate", requestor.sPatientFirstName);
            //allFields.Add("MROPatientAdditionalDetails", requestor.sPatientFirstName);
            //allFields.Add("MRODLVerification", requestor.sPatientFirstName);
            //allFields.Add("MROOtherGovID", requestor.sPatientFirstName);
            //allFields.Add("MROCameraUpload", requestor.sPatientFirstName);
            //allFields.Add("MROPDFView", requestor.sPatientFirstName);
            //allFields.Add("MRORequestReceived", requestor.sPatientFirstName);
            //allFields.Add("MROFeedbackRating", requestor.sPatientFirstName);
            //allFields.Add("MROFeedbackComment", requestor.sPatientFirstName);
            //allFields.Add("MROThankyou", requestor.sPatientFirstName);
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
            byte[] byteArrayToReturn = new LocationAuthorizationDocument().ReplaceFieldKeywordsWithValueWOStamp(pdfByteArray, allFields, out string sReplaceFieldsList);

            allFields.Clear();

            Doc theDoc = new Doc();

            theDoc.Read(byteArrayToReturn);
            string removeDataTag = requestor.sSignatureData.Replace("data:image/png;base64,", string.Empty);
            byte[] signatureByteArray = Convert.FromBase64String(removeDataTag);
            Image image2 = Image.FromStream(new MemoryStream(signatureByteArray));

            //string image1 = @sAppRoot + @"\Whitebg.png";
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
            theDoc.Rect.Position(theDoc.Form["Signature"].Rect.Left, theDoc.Form["Signature"].Rect.Bottom);
            theDoc.AddImageObject(theImg, false);
            theImg.Clear();

            theDoc.Form.Stamp();

            byte[] pdfBytes = theDoc.GetData();
            return pdfBytes;
        }
        #endregion
    }
}
