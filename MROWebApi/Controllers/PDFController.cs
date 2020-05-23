using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using MROWebAPI.Models;
using Wizard_Demo.Models;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PDFController : ControllerBase
    {
        private IConverter _converter;
        private readonly ILogger<PDFController> _logger;
        public PDFController(ILogger<PDFController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        //// GET: api/PDF
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/PDF/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/PDF
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        //public void Post([FromBody] string value)
        public IActionResult GeneratePDF(Patient patient)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = patient.FName + " " + patient.LName,
                //Out = Path.Combine(Directory.GetCurrentDirectory(), "data", "pdf", patient.FName + " " + patient.LName + " - " + patient.PCell + ".pdf")
            };

            //var globalSettingsSave = new GlobalSettings
            //{

            //    ColorMode = ColorMode.Color,
            //    Orientation = Orientation.Portrait,
            //    PaperSize = PaperKind.A4,
            //    Margins = new MarginSettings { Top = 10 },
            //    DocumentTitle = patient.FName + " " + patient.LName + " - " + patient.PCell,
            //    Out = Path.Combine(Directory.GetCurrentDirectory(), "data", "pdf", patient.FName + " " + patient.LName + " - " + patient.PCell + ".pdf")
            //};

            var objectSettings = new ObjectSettings
            {
                //Page= "http://localhost:50598/",
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHtmlTemplate(patient),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "StyleForHTML.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            //var objectSettingsSave = new ObjectSettings
            //{
            //    //Page= "http://localhost:50598/",
            //    PagesCount = true,
            //    HtmlContent = TemplateGenerator.GetHtmlTemplate(patient),
            //    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "StyleForHTML.css") },
            //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            //};

            //var pdfSave = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = globalSettingsSave,
            //    Objects = { objectSettingsSave }
            //};

            var pdfServe = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            byte[] pdfBytes = _converter.Convert(pdfServe);
            _logger.LogInformation("PDF Generated");
            //Utility u = new Utility();
            GenerateXML(patient);
            _logger.LogInformation("XML Generated");
            return File(pdfBytes, "application/pdf", patient.FName + " " + patient.LName+ ".pdf");
        }

        private static void GenerateXML(Patient patient)
        {

            var PatDec = patient.IsPatientDeceased ? "Yes" : "No";
            var middleInitials = string.IsNullOrEmpty(patient.MInitial) ? "" : patient.MInitial;
            var NotP = patient.NotPatient ? "No" : "Yes";
            var RName = string.IsNullOrEmpty(patient.RName) ? "" : patient.RName;
            var RelToP = string.IsNullOrEmpty(patient.RelationToPatient) ? "" : patient.RelationToPatient;
            var FormRequest = patient.ConfirmReport ? "Opted to be mailed to registered Email ID" : "Not Opted";
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings
            {
                OmitXmlDeclaration =false,
                ConformanceLevel = ConformanceLevel.Document
            };
            using XmlWriter writer = XmlWriter.Create(Path.Combine(Directory.GetCurrentDirectory(), "data", "xml", patient.FName + " " + patient.LName + ".xml"), xmlWriterSetting);
            writer.WriteStartElement("patientdetails");
            writer.WriteElementString("location", patient.SelectedLocation);
            writer.WriteElementString("ispatient", NotP);
            writer.WriteElementString("rname", RName);
            writer.WriteElementString("relationwithpatient", RelToP);
            writer.WriteElementString("emailID", patient.EmailID);
            writer.WriteElementString("formrequested", FormRequest);
            writer.WriteElementString("fname", patient.FName);
            writer.WriteElementString("mname", middleInitials);
            writer.WriteElementString("lname", patient.LName);
            writer.WriteElementString("deceased", PatDec);
            writer.WriteElementString("postalcode", patient.PostalCode);
            writer.WriteElementString("streetarea", patient.StreetArea);
            writer.WriteElementString("birthdate", patient.BDay);

            writer.WriteEndElement();
            writer.Flush();

            //Utility u = new Utility()
        }

        //// PUT: api/PDF/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
