using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class Requestors
    {
        [Key]
        public int nRequestorID { get; set; }
        public int? nFacilityID { get; set; }
        public int? nLocationID { get; set; }
        public bool? bAreYouPatient { get; set; }
        public string sRelativeName { get; set; }
        public string sRelationship { get; set; }
        public string sEmailId { get; set; }
        public string sPatientFirstName { get; set; }
        public string sPatientLastName { get; set; }
        public string sPatientMiddleInitial { get; set; }
        public bool? bIsPatientDeceased { get; set; }
        public string sPatientZip { get; set; }
        public string sPatientStreetAddress { get; set; }
        public DateTime? dtPatientDOB { get; set; }
        public DateTime? dtRecordTimeFrameStart { get; set; }
        public DateTime? dtRecordTimeFrameEnd { get; set; }
        public string sRecordType { get; set; }
        public string sRecordTypeOther { get; set; }
        public string sSensitiveData { get; set; }
        public DateTime? dtAuthExpireDate { get; set; }
        public bool? bRequestDeadline { get; set; }
        public DateTime? dtRequestDeadlineDate { get; set; }
        public string sWhomReleaseRecord { get; set; }
        public string sShipmentType { get; set; }
        public string sWayOfSendRecord { get; set; }
        public string sWhomReleaseRecordName { get; set; }
        public string sWhomReleaseRecordZip { get; set; }
        public string sWhomReleaseRecordStreetAdd { get; set; }
        public string sAdditionalData { get; set; }
        public string sPhoneNo { get; set; }
        public string sPhotoIdentity { get; set; }
        public string sSignature { get; set; }
        public string sPDF { get; set; }
        public bool? bToolEasyToUse { get; set; }
        public string sToolTextFeedback { get; set; }
        public DateTime dtLastUpdate { get; set; }
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
