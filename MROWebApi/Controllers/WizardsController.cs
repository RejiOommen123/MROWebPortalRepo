using System;
using System.Collections.Generic;
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
        public  async Task<IActionResult> SendXML([FromBody] Requestors requestors)
        {
            //Requestors requestors = new Requestors();
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
            string xmlString = "";
            using XmlWriter writer = XmlWriter.Create(Path.Combine(Directory.GetCurrentDirectory(), requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml"), xmlWriterSetting);
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
            writer.WriteElementString("date_required_by",requestors.dtDeadline.ToString());
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
            foreach (string singleRecordType in requestors.sSelectedRecordTypes) {
                writer.WriteStartElement("item");
                writer.WriteElementString("name",singleRecordType);
                //writer.WriteElementString("include", singleRecordType);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();

            //PDF Here
            //Get PDF & Show in Base 64 Encoding
            //writer.WriteElementString("pdf", requestors.sPDF);
            writer.WriteEndElement();
            writer.Flush();
            return Content(xmlString, "text/xml", System.Text.Encoding.UTF8);
        }
        #endregion
    }
}
