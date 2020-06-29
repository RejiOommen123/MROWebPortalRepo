//Siddhesh's File

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
        static private Dictionary<string, string> FieldList = new Dictionary<string, string>();

        #region Constructor
        /// <summary>
        /// Constructor to set the license key for abcPDF
        /// </summary>
        public LocationAuthorizationDocumentController()
        {
            SetLicense();
            //GetValidationFieldSet();
        }

        #endregion

        #region ReplaceFieldKeywordsWithValue
        /// <summary>
        /// Patient Web Portal - Replace Pre-defined Field Keywords with Values
        /// </summary>
        /// <param name="PDFFile">PDF - Byte Array</param>
        /// <returns>Byte Array - PDF (Replaced with values)</returns>
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

        
    }
}

