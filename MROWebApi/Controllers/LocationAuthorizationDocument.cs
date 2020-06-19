using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using MRODBL.BaseClasses;
using System.IO;
using System.Text.RegularExpressions;

namespace MROWebApi.Controllers
{
    public class LocationAuthorizationDocument
    {
        static private Dictionary<string, string> _FieldList =
                        new Dictionary<string, string>();
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

        #region ReplaceFieldKeywordsWithValue
        /// <summary>
        /// Patient Web Portal - Replace Pre-defined Field Keywords with Values
        /// </summary>
        /// <param name="PDFFile">PDF - Byte Array</param>
        /// <returns>Byte Array - PDF (Replaced with values)</returns>
        public byte[] ReplaceFieldKeywordsWithValue(byte[] PDFFile, IEnumerable<ValidateAuthorizationDoc> authorizationDocFeilds, out string sReplaceFieldsList)
        {
            //Get Pre-defined Field set (Dictionary Data) from DB
            GetFieldSetValueFromAuthorizationDocFields(authorizationDocFeilds);
            Doc thePDFAuthDoc = new Doc();
            thePDFAuthDoc.Read(PDFFile);

            //To insert each value in fields
            foreach (Field frm in thePDFAuthDoc.Form.Fields)
            {
                string sValue = null;
                string sName = frm.Name;
                string sNewValue;
                if (InList(sName, out sNewValue))
                    sValue += sNewValue + " ";
            }

            //To check and verify Document save the PDF in folder
            string sAppRoot = GetApplicationRoot();



            sReplaceFieldsList = "";
            return PDFFile;
        }
        static bool InList(string sField, out string sValue)
        {
            sValue = sField;
            string sFromList;
            if (_FieldList.TryGetValue(sField, out sFromList))
                sValue = sFromList;
            else if (_FieldList.TryGetValue(sField + "=1", out sFromList))
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
        /// <param name="PDFFile"></param>
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
                    Field field = pdfFormFields.FirstOrDefault(c=>c.Name==rules.sKeyword);
                    if (field != null) {
                        //sKeyword is Present
                        continue;
                    }
                    else {
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
