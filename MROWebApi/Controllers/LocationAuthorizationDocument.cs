//Reji's File 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using MRODBL.BaseClasses;
using System.IO;
using System.Text.RegularExpressions;
using WebSupergoo.ABCpdf11.Elements;

namespace MROWebApi.Controllers
{
    public class LocationAuthorizationDocument
    {
        static private Dictionary<string, string> _FieldList = new Dictionary<string, string>();
        #region Constructor

        /// <summary>
        /// Constructor to set the license key for abcPDF
        /// </summary>
        public LocationAuthorizationDocument()
        {
            SetLicense();
            //GetValidationFieldSet();
        }

        #endregion

        #region For Signature
        #endregion
        public byte[] ReplaceFieldKeywordsWithValueWOStamp(byte[] PDFFile, Dictionary<string, string> allFields, out string sReplaceFieldsList)
        {
            //Get Pre-defined Field set (Dictionary Data) from DB
            //GetFieldSetValueFromAuthorizationDocFields(authorizationDocFeilds);
            Doc thePDFAuthDoc = new Doc();
            thePDFAuthDoc.Read(PDFFile);

            //To insert each value in fields
            foreach (Field frm in thePDFAuthDoc.Form.Fields)
            {
                //TODO: Logic
                //check for MRO Appended Keyword
                //C# 8.0 New Feature
                try
                {
                    if (frm.Name.Substring(0, 3) == "MRO")
                    {
                        string sValue = null;
                        string sName = frm.Name;
                        string sNewValue;
                        if (InList(sName, allFields, out sNewValue))
                            sValue += sNewValue + " ";
                        Stamp(thePDFAuthDoc, frm.Name, sValue.Trim());
                    }
                }
                catch (Exception e)
                {
                    continue;
                }

            }

            //adding the Photo of Driving License
            //XImage theDrivingLicense = new XImage();
            //theImg.SetFile(sAppRoot + @"\download.jpg");
            //theImg.SetFile("https://image.shutterstock.com/image-photo/white-transparent-leaf-on-mirror-260nw-577160911.jpg");

            //Adding new page in Authorization PDF
            //thePDFAuthDoc.Page = thePDFAuthDoc.AddPage();
            //int theID = 0;
            //string theText = "Creat New Page"; // 
            //thePDFAuthDoc.Width = 4;
            //thePDFAuthDoc.FontSize = 32;
            //thePDFAuthDoc.TextStyle.Justification = 1;
            //thePDFAuthDoc.Rect.Inset(20, 20);
            //thePDFAuthDoc.FrameRect();
            //theID = thePDFAuthDoc.AddTextStyled(theText);

            //thePDFAuthDoc.FontSize = 12;
            //thePDFAuthDoc.AddTextStyled("<br /><br /><br /><br /><b>Reji Oommen</b> <br />Lets go to Office <br /><br /><br />" +
            //    "<br /><br /><b>Siddesh Lad</b> <br />Lets go to Park<br /><br /><br />");

            //// Image insertion on a specific location on new page
            //thePDFAuthDoc.Rect.Left = 50;
            //thePDFAuthDoc.Rect.Bottom = 400;
            //thePDFAuthDoc.Rect.Width = theImg.Width;
            //thePDFAuthDoc.Rect.Height = 300;
            //thePDFAuthDoc.Rect.Height = theImg.Height;
            //thePDFAuthDoc.AddImageObject(theImg, false);
            //thePDFAuthDoc.Clear();



            //thePDFAuthDoc.Form.Stamp();
            byte[] ArrayToReturn = thePDFAuthDoc.GetData();
            ////To check and verify Document save the PDF in folder
            //string sAppRoot = GetApplicationRoot();
            //thePDFAuthDoc.Save(sAppRoot + @"\Auth_sample.pdf");


            sReplaceFieldsList = "";
            return ArrayToReturn;
        }
        #region ReplaceFieldKeywordsWithValue
        /// <summary>
        /// Patient Web Portal - Replace Pre-defined Field Keywords with Values
        /// </summary>
        /// <param name="PDFFile">PDF - Byte Array</param>
        /// <returns>Byte Array - PDF (Replaced with values)</returns>
        public byte[] ReplaceFieldKeywordsWithValue(byte[] PDFFile, Dictionary<string, string> allFields, out string sReplaceFieldsList)
        {
            //Get Pre-defined Field set (Dictionary Data) from DB
            //GetFieldSetValueFromAuthorizationDocFields(authorizationDocFeilds);
            Doc thePDFAuthDoc = new Doc();
            thePDFAuthDoc.Read(PDFFile);

            //To insert each value in fields
            foreach (Field frm in thePDFAuthDoc.Form.Fields)
            {
                //TODO: Logic
                //check for MRO Appended Keyword
                //C# 8.0 New Feature
                try
                {
                    if (frm.Name.Substring(0, 3) == "MRO")
                    {
                        string sValue = null;
                        string sName = frm.Name;
                        string sNewValue;
                        if (InList(sName, allFields, out sNewValue))
                            sValue += sNewValue + " ";
                        Stamp(thePDFAuthDoc, frm.Name, sValue.Trim());
                    }
                }
                catch (Exception e)
                {
                    continue;
                }

            }

            //adding the Photo of Driving License
            //XImage theDrivingLicense = new XImage();
            //theImg.SetFile(sAppRoot + @"\download.jpg");
            //theImg.SetFile("https://image.shutterstock.com/image-photo/white-transparent-leaf-on-mirror-260nw-577160911.jpg");

            //Adding new page in Authorization PDF
            //thePDFAuthDoc.Page = thePDFAuthDoc.AddPage();
            //int theID = 0;
            //string theText = "Creat New Page"; // 
            //thePDFAuthDoc.Width = 4;
            //thePDFAuthDoc.FontSize = 32;
            //thePDFAuthDoc.TextStyle.Justification = 1;
            //thePDFAuthDoc.Rect.Inset(20, 20);
            //thePDFAuthDoc.FrameRect();
            //theID = thePDFAuthDoc.AddTextStyled(theText);

            //thePDFAuthDoc.FontSize = 12;
            //thePDFAuthDoc.AddTextStyled("<br /><br /><br /><br /><b>Reji Oommen</b> <br />Lets go to Office <br /><br /><br />" +
            //    "<br /><br /><b>Siddesh Lad</b> <br />Lets go to Park<br /><br /><br />");

            //// Image insertion on a specific location on new page
            //thePDFAuthDoc.Rect.Left = 50;
            //thePDFAuthDoc.Rect.Bottom = 400;
            //thePDFAuthDoc.Rect.Width = theImg.Width;
            //thePDFAuthDoc.Rect.Height = 300;
            //thePDFAuthDoc.Rect.Height = theImg.Height;
            //thePDFAuthDoc.AddImageObject(theImg, false);
            //thePDFAuthDoc.Clear();



            thePDFAuthDoc.Form.Stamp();
            byte[] ArrayToReturn = thePDFAuthDoc.GetData();
            ////To check and verify Document save the PDF in folder
            //string sAppRoot = GetApplicationRoot();
            //thePDFAuthDoc.Save(sAppRoot + @"\Auth_sample.pdf");


            sReplaceFieldsList = "";
            return ArrayToReturn;
        }
        static void Stamp(Doc theDoc, string sField, string sValue)
        {
            if (theDoc.Form == null || theDoc.Form[sField] == null)
                return;
            theDoc.Form[sField].Value = sValue;
        }
        static bool InList(string sField, Dictionary<string, string> allFields, out string sValue)
        {
            sValue = sField;
            string sFromList;
            if (allFields.TryGetValue(sField, out sFromList))
                sValue = sFromList;
            else if (allFields.TryGetValue(sField + "=1", out sFromList))
                sValue = sFromList;

            return true;
        }
        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        private Dictionary<string, string> GetFieldSetValueFromAuthorizationDocFields(IEnumerable<ValidateAuthorizationDoc> authorizationDocFeilds)
        {
            //Get all values to dictionary and update the 
            _FieldList = authorizationDocFeilds.ToDictionary(sADF => sADF.sKeyword, sADF => sADF.sFieldname);

            return _FieldList;
        }
        #endregion

        #region Validate Authorization Document

        /// <summary>
        ///  Validate Authorization Document
        /// </summary>
        /// <param name="PDFFile">Byte Array (PDF File)</param>
        /// <returns>Bool</returns>

        public bool ValidateAuthorizationDocument(byte[] PDFFile, IEnumerable<ValidateAuthorizationDoc> validationRules, out string sValidationText)
        {
            sValidationText = "";
            bool bValidation = true;
            if (PDFFile != null)
            {
                Doc authPdfDocument = new Doc();
                authPdfDocument.Read(PDFFile);
                var pdfFormFields = authPdfDocument.Form.Fields;
                foreach (ValidateAuthorizationDoc rules in validationRules)
                {
                    Field field = pdfFormFields.FirstOrDefault(c => c.Name == rules.sKeyword);
                    if (field != null)
                    {
                        //sKeyword is Present
                        continue;
                    }
                    else
                    {
                        //sKeyword is Not Present
                        sValidationText += rules.sKeyword + " Not found! <br />";
                        bValidation = false;
                    }
                }
            }
            else
            {
                bValidation = false;
            }
            _FieldList.Clear();
            return bValidation;
        }

        /// <summary>
        /// To get Field set for validating Authorization 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetValidationFieldSet()
        {

            _FieldList.Add("MROPatientFullName", "MROPatientFullName");
            _FieldList.Add("MROPatientAddress", "MROPatientAddress");
            _FieldList.Add("MROPatientDOB", "MROPatientDOB");
            _FieldList.Add("MROPatientTelephoneNo", "MROPatientTelephoneNo");
            return _FieldList;
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

    }
}
