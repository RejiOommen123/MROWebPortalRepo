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
        [HttpGet("GetWizardConfig/{nFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<object> GetWizardConfigurationAsync(int nFacilityID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                object Wizard_Config = await  fieldsRepository.GetWizardConfigurationAsync(nFacilityID);
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

        #region Logo & BG for Facility
        [HttpGet("GetLogoAndBackgroundImageforFacility/{nFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<dynamic> GetLogoAndBackgroundImageforFacility(int nFacilityID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                dynamic LogoAndBackgroundImageforFacility = await fieldsRepository.GetLogoBackGroundforFacilityAsync(nFacilityID);
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
        private static void GenerateXML(Requestors requestors)
        {

            //var PatDec = patient.IsPatientDeceased ? "Yes" : "No";
            var middleInitials = string.IsNullOrEmpty(requestors.sPatientMiddleInitial) ? "" : requestors.sPatientMiddleInitial;
            //var NotP = patient.NotPatient ? "No" : "Yes";
            //var RName = string.IsNullOrEmpty(patient.RName) ? "" : patient.RName;
            //var RelToP = string.IsNullOrEmpty(patient.RelationToPatient) ? "" : patient.RelationToPatient;
            //var FormRequest = patient.ConfirmReport ? "Opted to be mailed to registered Email ID" : "Not Opted";
            XmlWriterSettings xmlWriterSetting = new XmlWriterSettings
            {
                OmitXmlDeclaration = false,
                ConformanceLevel = ConformanceLevel.Document
            };
            using XmlWriter writer = XmlWriter.Create(Path.Combine(Directory.GetCurrentDirectory(), "data", "xml", requestors.sPatientFirstName + " " + requestors.sPatientLastName + ".xml"), xmlWriterSetting);
            writer.WriteStartElement("request");
                writer.WriteStartElement("facility");
                //For facility Code,Name,ID
                writer.WriteEndElement();
                writer.WriteStartElement("location");
                    writer.WriteStartElement("item");
                    //For Location Code,Name,ID
                    writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("detail");
                //For Date, Reason,Comments
                writer.WriteEndElement();
            writer.WriteStartElement("patient");
            writer.WriteElementString("firstname", requestors.sPatientFirstName);
            writer.WriteElementString("lastname", requestors.sPatientLastName);
            writer.WriteElementString("middle_name", middleInitials);
            writer.WriteElementString("dob", requestors.dtPatientDOB.ToString());
            writer.WriteStartElement("address");
            //For Street,City,State,ZipCode
            writer.WriteElementString("street", requestors.sPatientStreetAddress);
                //Add
                //writer.WriteElementString("city", requestors.sPatientCity);
                //writer.WriteElementString("state", requestors.sPatientState); 
                writer.WriteElementString("zipcode", requestors.sPatientZip);
            writer.WriteEndElement();
            writer.WriteElementString("phone_number", requestors.sPhoneNo);
            writer.WriteEndElement();



            //Requestor
            writer.WriteStartElement("requestor");
            writer.WriteElementString("firstname", requestors.sPatientFirstName);
            writer.WriteElementString("lastname", requestors.sPatientLastName);
            //Organization
            //writer.WriteElementString("organization", requestors.);
            writer.WriteElementString("is_patient", requestors.bAreYouPatient.ToString());
            writer.WriteElementString("is_patient", requestors.bAreYouPatient.ToString());
            writer.WriteElementString("relation", requestors.sRelationship);
            writer.WriteElementString("email", requestors.sEmailId);
            writer.WriteStartElement("address");
            //For Street,City,State,ZipCode
            writer.WriteElementString("street", requestors.sPatientStreetAddress);
            //Add
            //writer.WriteElementString("city", requestors.sPatientCity);
            //writer.WriteElementString("state", requestors.sPatientState); 
            writer.WriteElementString("zipcode", requestors.sPatientZip);
            writer.WriteEndElement();
            writer.WriteElementString("phone_number", requestors.sPhoneNo);
            writer.WriteElementString("fax_number", requestors.sPhoneNo);
            writer.WriteEndElement();

            //requestor ends here


            writer.WriteStartElement("requested_information");
                writer.WriteStartElement("dates_of_service");
                writer.WriteElementString("start", requestors.dtRecordTimeFrameStart.ToString());
            writer.WriteElementString("end", requestors.dtRecordTimeFrameEnd.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("types");
            //Add Items Here
            writer.WriteEndElement();
            writer.WriteEndElement();

            //PDF Here
            //Get PDF & Show in Base 64 Encoding
            //writer.WriteElementString("pdf", requestors.dtRecordTimeFrameEnd.ToString());
            writer.WriteEndElement();
            writer.Flush();
        }
        #endregion
    }
}
