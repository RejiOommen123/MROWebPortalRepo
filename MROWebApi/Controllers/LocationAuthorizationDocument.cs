using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using MRODBL.BaseClasses;

namespace MROWebApi.Controllers
{
    public class LocationAuthorizationDocument
    {
        static private Dictionary<string, string> FieldList =
                        new Dictionary<string, string>();
        #region Constructor

        /// <summary>
        /// Constructor to set the license key for abcPDF
        /// </summary>
        public LocationAuthorizationDocument()
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

        public bool ValidateAuthorizationDocument(byte[] PDFFile,out string sValidationText)
        {
            //string 
            sValidationText = "";

            if (PDFFile != null)
            {
                Doc theDoc = new Doc();
                theDoc.Read(PDFFile);

                foreach (Field frm in theDoc.Form.Fields)
                {
                    string sFileKeyword = frm.Name;
                    string sFromList;
                    if (FieldList.TryGetValue(sFileKeyword, out sFromList))
                    {
                        continue;
                    }
                    else
                    {
                        sValidationText += sFileKeyword + " Not found! <br />";
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

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

    }
}
