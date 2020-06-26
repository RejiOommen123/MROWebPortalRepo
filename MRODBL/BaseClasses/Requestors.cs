using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    //use only for xml  for now 
    //when structure is finalized make changes to DB
    public partial class Requestors
    {
        [Key]
        public int nRequestorID { get; set; }
        public int nFacilityID { get; set; }
        public int nLocationID { get; set; }
        public string sSelectedLocation { get; set; }
        public bool bAreYouPatient { get; set; }
        public string sRelativeName { get; set; }
        public string sRelationToPatient { get; set; }
        public string sPatientFirstName { get; set; }
        public string sPatientLastName { get; set; }
        public string sPatientMiddleInitial { get; set; }
        public bool bIsPatientMinor { get; set; }
        public bool bIsPatientDeceased { get; set; }
        public DateTime? dtPatientDOB { get; set; }
        public string sPatientEmailId { get; set; }
        public string sConfirmEmailId { get; set; }
        public bool bConfirmReport { get; set; }
        public string sAddZipCode { get; set; }
        public string sAddCity { get; set; }
        public string sAddState { get; set; }
        public string sAddStreetAddress { get; set; }
        public DateTime? dtRecordRangeStart { get; set; }
        public DateTime? dtRecordRangeEnd { get; set; }
        public string[] sSelectedRecordTypes { get; set; }
        public string[] sSelectedPrimaryReasons { get; set; }
        public string sOtherReasons { get; set; }
        public string sReleaseTo { get; set; }
        public string[] sSelectedShipmentTypes { get; set; }
        public string[] selectedSensitiveInfo { get; set; }
        public DateTime? dtAuthExpire { get; set; }
        public string sAuthSpecificEvent { get; set; }
        public bool bDeadlineStatus { get; set; }
        public DateTime? dtDeadline { get; set; }
        public string sAdditionalData { get; set; }
        public string sIdentityIdName { get; set; }
        public string sIdentityImage { get; set; }
        public string sSignatureData { get; set; }
        public bool bRequestAnotherRecord { get; set; }
        public int nFeedbackRating { get; set; }
        public string sFeedbackComment { get; set; }
        public string sPhoneNo { get; set; }
        public string sPDF { get; set; }
        public DateTime dtLastUpdate { get; set; }
        //Extra Added
        public string sSTFaxCompAdd { get; set; }
        public string sSTEmailId {get;set;}
        public string sSTConfirmEmailId {get;set;}
        public string sSTMailCompAdd {get;set;}
        public DateTime? dtPickUp {get;set;}
    }
}


//public class Requestors
//{
//    [Key]
//    public int nRequestorID { get; set; }
//    //Hidden
//    public int nFacilityID { get; set; }
//    //Nothing on Wizard - 1
//    //Wizard - 2 
//    public int nLocationID { get; set; }
//    //Wizard - 3
//    public bool bAreYouPatient { get; set; }
//    //Wizard -4
//    //Yes Section (This Details Will be Taken)
//    public string sPatientFirstName { get; set; }
//    public string sPatientLastName { get; set; }
//    public string sPatientMiddleInitial { get; set; }
//    //No Section (Extra if not Patient)
//    public string sRelativeName { get; set; }
//    public string sRelationship { get; set; }
//    //Wizard - 5
//    public DateTime? dtPatientDOB { get; set; }
//    //Wizard - 6
//    //Confirm Email ?? Is It Required?
//    public string sEmailId { get; set; }
//    //public string sConfirmEmailId { get; set; }
//    //Wizard - 7
//    public string sPatientZip { get; set; }
//    public string sPatientCity { get; set; }
//    public string sPatientState { get; set; }
//    public string sPatientStreetAddress { get; set; }
//    //Wizard - 8
//    public DateTime? dtRecordTimeFrameStart { get; set; }
//    public DateTime? dtRecordTimeFrameEnd { get; set; }
//    //Wizard - 9
//    public string[] sRecordTypeArray { get; set; }
//    //Wziard-10
//    public string[] sPrimaryReasonArray { get; set; }
//    //Wizard - 10
//    public string sWhomReleaseRecord { get; set; }
//    //Wizard - 11
//    //Big Wizard
//    public string sShipmentTypeArray { get; set; }
//    //Wizard - 12
//    public DateTime? dtAuthExpire { get; set; }
//    //More 2 Options
//    //Wizard - 13
//    public string[] sSensitiveInfoArray { get; set; }
//    //Wizard - 14
//    public bool? bRequestDeadline { get; set; }
//    //Wizard - 15
//    //Yes Section
//    public DateTime? dtRequestDeadline { get; set; }
//    //Wizard - 16
//    public string sAdditionalData { get; set; }
//    //Wizard - 17
//    public string sPhoneNo { get; set; }
//    //Wizard - 18
//    public string sPhotoIdentity { get; set; }
//    //Wizard - 19
//    //Are We Storing Identity Data ?
//    public string sPhotoIdentityData { get; set; }
//    //Wizard - 20
//    //Showing PDF Filled With values
//    //Wizard - 21
//    //Sign PDF
//    //Wizard - 22
//    //Request Submitted
//    //Wizard - 23
//    public bool? bToolEasyToUse { get; set; }
//    public int nRequestorRating { get; set; }
//    public string sToolTextFeedback { get; set; }
//    //wizard - 24
//    //Thanks
//}
