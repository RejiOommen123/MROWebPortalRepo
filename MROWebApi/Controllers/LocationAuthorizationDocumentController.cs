using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using MRODBL.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using MROWebAPI.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class LocationAuthorizationDocumentController : ControllerBase
    {
        static private Dictionary<string, string> FieldList =
                        new Dictionary<string, string>();
        #region Constructor

        /// <summary>
        /// Constructor to set the license key for abcPDF
        /// </summary>
        public LocationAuthorizationDocumentController()
        {
            SetLicense();
        }

        #endregion

        #region ReplaceFieldKeywordsWithValue
        /// <summary>
        /// Patient Web Portal - Replace Pre-defined Field Keywords with Values
        /// </summary>
        /// <param name="PDFFile"></param>
        /// <returns></returns>
        public byte[] ReplaceFieldKeywordsWithValue(byte[] PDFFile)
        {
            //Get Pre-defined Field set from DB
            //TODO: fetch the details from DB 
            return PDFFile;
        }
        #endregion

        #region Validate Authorization Document

        /// <summary>
        ///  Validate Authorization Document
        /// </summary>
        /// <param name="PDFFile"></param>
        /// <returns></returns>

        //public bool ValidateAuthorizationDocument(byte[] PDFFile,out string sValidationText)
        //{
        //    //string 
        //    sValidationText = "";

        //    if (PDFFile != null)
        //    {
        //        Doc theDoc = new Doc();
        //        theDoc.Read(PDFFile);

        //        foreach (Field frm in theDoc.Form.Fields)
        //        {
        //            string sFileKeyword = frm.Name;
        //            string sFromList;
        //            if (FieldList.TryGetValue(sFileKeyword, out sFromList))
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                sValidationText += sFileKeyword + " Not found! <br />";
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        /// <summary>
        /// To get Field set for validating Authorization 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetValidationFieldSet()
        {

            FieldList.Add("Destination", "");
            FieldList.Add("RecordType=1", "");
            FieldList.Add("DOB", "");
            FieldList.Add("Patient", "");
            return FieldList;
        }
        #endregion

        #region Set License for abcPDF
        public static bool SetLicense()
        {
            if (XSettings.LicenseDescription != "Professional License")
            {
                return XSettings.InstallRedistributionLicense(@"X/VKS0cMn5FgsCJaa6+Eao3yLL9GQ4MYlq3wxL3
KBkwwzUmSbl38dNU + aUtqxFg1MKBguzRHpTBli + A
ySbR + x0MlZW6cBnA / 2EsUZeafY1zR + lfjwuwEXJC
xQneIIF0yWu5eV9pQ4BciTAZ6xUy5JUcLXxy3Bki
luLQd8VM + R + TJpdgOpi230UWpyYBMjmwMY0AA22Y
W9iOxBEYtRrdvsjs1 / hf0baE = ");
            }
            return true;
        }
        #endregion



        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult GetPDF()
        {
            string sAppRoot = GetApplicationRoot();

            Doc theDoc = new Doc();
            theDoc.Read(sAppRoot + @"\auth1.pdf");

            byte[] pdfBytes = theDoc.GetData();

            return File(pdfBytes, "application/pdf", "Auth1" + ".pdf");
        }

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
        public IActionResult GeneratePDF(Patient patient)
        {
            //string signatureData = "data:image/png;base64,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";
            string sAppRoot = GetApplicationRoot();

            Doc theDoc = new Doc();

            theDoc.Read(sAppRoot + @"\auth1.pdf");
            //theDoc.Read("D:/MRO Code/MROWebApi/auth1.pdf");
            string removeDataTag = patient.imgData.Replace("data:image/png;base64,", string.Empty);
            byte[] signatureByteArray = Convert.FromBase64String(removeDataTag);
            Image image2 = Image.FromStream(new MemoryStream(signatureByteArray));


            //Bitmap bmp;
            //using (var ms = new MemoryStream(signatureByteArray))
            //{
            //    bmp = new Bitmap(ms);
            //    bmp.Save("D:/MRO Code/MROWebApi/outputwithbitmap.png", ImageFormat.Png);
            //}


            string image1 = @sAppRoot + @"\Whitebg.png";


            System.Drawing.Image canvas = Bitmap.FromFile(image1);
            Graphics gra = Graphics.FromImage(canvas);
            Bitmap smallImg = new Bitmap(image2);
            gra.DrawImage(smallImg, new Point(0, 0));
            //canvas.Save(@"D:\MRO Code\MROWebApi\signaturewithbg.png", System.Drawing.Imaging.ImageFormat.Jpeg);

            var mssignaturewithbg = new MemoryStream();
            canvas.Save(mssignaturewithbg, canvas.RawFormat);
            mssignaturewithbg.ToArray();

            //Bitmap bitmap = new Bitmap("D:/MRO Code/MROWebApi/output.png");
            //Graphics graph = Graphics.FromImage(bitmap);
            //graph.Clear(Color.Red);
            //bitmap.Save("D:/MRO Code/MROWebApi/outputwithbg.png", ImageFormat.Png);

            XImage theImg = new XImage();
            theImg.SetStream(mssignaturewithbg);
            theDoc.Rect.String = theImg.Selection.String;
            theDoc.Rect.Magnify(0.15, 0.2);
            theDoc.Rect.Position(theDoc.Form["Signature"].Rect.Left, theDoc.Form["Signature"].Rect.Bottom);
            theDoc.AddImageObject(theImg, false);
            theImg.Clear();


            theDoc.Form.Stamp();

            //System.IO.File.Delete(@"D:\MRO Code\MROWebApi\signature.png");
            //System.IO.File.Delete(@"D:\MRO Code\MROWebApi\signaturewithbg.png");
            //theDoc.Save("D:/MRO Code/MROWebApi/auth1.pdf".ToLower().Replace(".pdf", "_stamped.pdf"));

            byte[] pdfBytes = theDoc.GetData();

            return File(pdfBytes, "application/pdf", "Stamped" + ".pdf");


        }
    }
}

