using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Controllers;
using MROWebAPI.Models;

using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using Wizard_Demo.Models;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PDFController : ControllerBase
    {
        #region Constructor
        private readonly DBConnectionInfo _info;
        private readonly ILogger<PDFController> _logger;
        public PDFController(ILogger<PDFController> logger, DBConnectionInfo info)
        {
            _logger = logger;
            _info = info;
        }
        #endregion

        #region Commented Code POC 
        // POST: api/PDF
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("[action]")]
        //public IActionResult GeneratePDF(Patient patient)
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Portrait,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 10 },
        //        DocumentTitle = patient.FName + " " + patient.LName,
        //        //Out = Path.Combine(Directory.GetCurrentDirectory(), "data", "pdf", patient.FName + " " + patient.LName + " - " + patient.PCell + ".pdf")
        //    };

        //    //var globalSettingsSave = new GlobalSettings
        //    //{

        //    //    ColorMode = ColorMode.Color,
        //    //    Orientation = Orientation.Portrait,
        //    //    PaperSize = PaperKind.A4,
        //    //    Margins = new MarginSettings { Top = 10 },
        //    //    DocumentTitle = patient.FName + " " + patient.LName + " - " + patient.PCell,
        //    //    Out = Path.Combine(Directory.GetCurrentDirectory(), "data", "pdf", patient.FName + " " + patient.LName + " - " + patient.PCell + ".pdf")
        //    //};

        //    var objectSettings = new ObjectSettings
        //    {
        //        //Page= "http://localhost:50598/",
        //        PagesCount = true,
        //        HtmlContent = TemplateGenerator.GetHtmlTemplate(patient),
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "StyleForHTML.css") },
        //        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
        //        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
        //    };
        //    //var objectSettingsSave = new ObjectSettings
        //    //{
        //    //    //Page= "http://localhost:50598/",
        //    //    PagesCount = true,
        //    //    HtmlContent = TemplateGenerator.GetHtmlTemplate(patient),
        //    //    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "StyleForHTML.css") },
        //    //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
        //    //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
        //    //};

        //    //var pdfSave = new HtmlToPdfDocument()
        //    //{
        //    //    GlobalSettings = globalSettingsSave,
        //    //    Objects = { objectSettingsSave }
        //    //};

        //    var pdfServe = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    };

        //    byte[] pdfBytes = _converter.Convert(pdfServe);
        //    _logger.LogInformation("PDF Generated");
        //    //Utility u = new Utility();
        //    GenerateXML(patient);
        //    _logger.LogInformation("XML Generated");
        //    return File(pdfBytes, "application/pdf", patient.FName + " " + patient.LName+ ".pdf");
        //}
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("[action]")]
        //public async Task<IActionResult> FillAndSendPDF(Requestors requestor)
        //{
        //    byte[] pdfBytes = await GetFilledPDF(requestor, _info);
        //    return File(pdfBytes, "application/pdf");
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("[action]")]
        //public async Task<IActionResult> SignAndSendPDF(Requestors requestor)
        //{
        //    byte[] pdfBytes = await GetSignedPDF(requestor);
        //    return File(pdfBytes, "application/pdf");
        //}

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

        //OnSubmit of SIgned Form
        //Call to Generate XMl
        //Wizards
        //SendXML
        //Object : requestor
        private async static Task<byte[]> GetFilledPDF(Requestors requestor, DBConnectionInfo _info)
        {
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
            //MRORequestDeadline
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
            byte[] byteArrayToReturn = new LocationAuthorizationDocument().ReplaceFieldKeywordsWithValue(pdfByteArray, allFields, out string sReplaceFieldsList);

            allFields.Clear();
            return byteArrayToReturn;
        }
        #endregion
    }
}
