using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MRODBL.BaseClasses;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace MROWebApi.Controllers
{
    public class LocationAuthorizationDocumentController
    {
        #region Loc Auth Doc Constructor
        static private Dictionary<string, string> _FieldList = new Dictionary<string, string>();
        public LocationAuthorizationDocumentController()
        {
            SetLicense();
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
                        sValidationText += rules.sKeyword + @" Not Found! <br/>";
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

        #region Replace values & Don't Stamp
        public byte[] ReplaceFieldKeywordsWithValueWOStamp(byte[] PDFFile, Dictionary<string, string> allFields,Requesters requestor ,out string sReplaceFieldsList)
        {
            Doc thePDFAuthDoc = new Doc();
            thePDFAuthDoc.Read(PDFFile);
            foreach (Field frm in thePDFAuthDoc.Form.Fields)
            {
                //check for MRO Appended Keyword
                try
                {
                    if (frm.Name.Substring(0, 3) == "MRO" && frm.Name != "MROSignature")
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

            //Adding the Photo of Driving License
            string result = Regex.Replace(requestor.sIdentityImage, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            XImage theDrivingLicense = new XImage();
            byte[] dlArray = Convert.FromBase64String(result);
            theDrivingLicense.SetData(dlArray);

            //Adding new page in Authorization PDF
            thePDFAuthDoc.Page = thePDFAuthDoc.AddPage();
            int theID = 0;
            string theText = requestor.sPatientFirstName + " " + requestor.sPatientLastName + " " + "(D.O.B. " + requestor.dtPatientDOB.Value.ToShortDateString() + ")";
            thePDFAuthDoc.Width = 4;
            thePDFAuthDoc.FontSize = 20;
            thePDFAuthDoc.TextStyle.Justification = 1;
            thePDFAuthDoc.Rect.Inset(20, 20);
            thePDFAuthDoc.FrameRect();
            theID = thePDFAuthDoc.AddTextStyled(theText);
            thePDFAuthDoc.FontSize = 12;
            thePDFAuthDoc.AddTextStyled("<br /><br /><b>Additional Identifiers Requested</b> <br /><b>1.ID Verification</b><br />");
            thePDFAuthDoc.AddTextStyled("<br /><b>2.Mailing Address<b> " + requestor.sAddStreetAddress + " " + requestor.sAddCity + " " + requestor.sAddState + " " + requestor.sAddZipCode + "<br />");
            thePDFAuthDoc.AddTextStyled("<br /><b>3.Email Address (User Confirmed)<b> " + requestor.sRequesterEmailId + " " + "(consented to an unencrypted emailed copy of their request)" + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>4.Phone Number (Verified)<b> " + requestor.sPhoneNo + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>5.Reason for Request<b> " + string.Join(",", requestor.sSelectedPrimaryReasons) + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>6.Note:Time Sensitive<b> " + requestor.dtDeadline != null ? requestor.dtDeadline.Value.ToShortDateString() : "No deadline" + "<br /");


            // Image insertion on a specific location on new page
            thePDFAuthDoc.Rect.Left = 50;
            thePDFAuthDoc.Rect.Bottom = 400;
            thePDFAuthDoc.Rect.Width = 200;
            thePDFAuthDoc.Rect.Height = 150;

            thePDFAuthDoc.AddImageObject(theDrivingLicense, false);
            
            byte[] ArrayToReturn = thePDFAuthDoc.GetData();
            sReplaceFieldsList = "";
            return ArrayToReturn;
        }
        #endregion

        #region ReplaceFieldKeywordsWithValue & Stamp as well
        /// <summary>
        /// Patient Web Portal - Replace Pre-defined Field Keywords with Values
        /// </summary>
        /// <param name="PDFFile">PDF - Byte Array</param>
        /// <returns>Byte Array - PDF (Replaced with values)</returns>
        public byte[] ReplaceFieldKeywordsWithValue(byte[] PDFFile, Dictionary<string, string> allFields, Requesters requestor, out string sReplaceFieldsList)
        {
            Doc thePDFAuthDoc = new Doc();
            thePDFAuthDoc.Read(PDFFile);

            foreach (Field frm in thePDFAuthDoc.Form.Fields)
            {
                //check for MRO Appended Keyword
                try
                {
                    if (frm.Name.Substring(0, 3) == "MRO" && frm.Name != "MROSignature")
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

            //Adding the Photo of Driving License
            string result = Regex.Replace(requestor.sIdentityImage, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            XImage theDrivingLicense = new XImage();
            byte[] dlArray = Convert.FromBase64String(result);
            theDrivingLicense.SetData(dlArray);

            //Adding new page in Authorization PDF
            thePDFAuthDoc.Page = thePDFAuthDoc.AddPage();
            int theID = 0;
            string theText = requestor.sPatientFirstName+" "+requestor.sPatientLastName + " "+"(D.O.B. "+requestor.dtPatientDOB.Value.ToShortDateString()+")";
            thePDFAuthDoc.Width = 4;
            thePDFAuthDoc.FontSize = 20;
            thePDFAuthDoc.TextStyle.Justification = 1;
            thePDFAuthDoc.Rect.Inset(20, 20);
            thePDFAuthDoc.FrameRect();
            theID = thePDFAuthDoc.AddTextStyled(theText);
            thePDFAuthDoc.FontSize = 12;

            thePDFAuthDoc.AddTextStyled("<br /><br /><b>Additional Identifiers Requested</b> <br /><b>1.ID Verification</b><br />");
            thePDFAuthDoc.AddTextStyled("<br /><b>2.Mailing Address<b> " + requestor.sAddStreetAddress + " " + requestor.sAddCity + " " + requestor.sAddState + " " + requestor.sAddZipCode + "<br />");
            thePDFAuthDoc.AddTextStyled("<br /><b>3.Email Address (User Confirmed)<b> " + requestor.sRequesterEmailId + " " + "(consented to an unencrypted emailed copy of their request)" + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>4.Phone Number (Verified)<b> " + requestor.sPhoneNo + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>5.Reason for Request<b> " + string.Join(",", requestor.sSelectedPrimaryReasons) + "<br /");
            thePDFAuthDoc.AddTextStyled("<br /><b>6.Note:Time Sensitive<b> " + requestor.dtDeadline != null ? requestor.dtDeadline.Value.ToShortDateString() : "No deadline" + "<br /");

            // Image insertion on a specific location on new page
            thePDFAuthDoc.Rect.Left = 50;
            thePDFAuthDoc.Rect.Bottom = 400;
            //thePDFAuthDoc.Rect.Width = theDrivingLicense.Width;
            thePDFAuthDoc.Rect.Width = 200;
            thePDFAuthDoc.Rect.Height = 150;
            //thePDFAuthDoc.Rect.Height = theDrivingLicense.Height;
            thePDFAuthDoc.AddImageObject(theDrivingLicense, false);
            
            thePDFAuthDoc.Form.Stamp();
            byte[] ArrayToReturn = thePDFAuthDoc.GetData();
            sReplaceFieldsList = "";
            return ArrayToReturn;
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
            _FieldList = authorizationDocFeilds.ToDictionary(sADF => sADF.sKeyword, sADF => sADF.sFieldname);
            return _FieldList;
        }
        #endregion

        #region Helper Methods
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
        #endregion
    }
}

