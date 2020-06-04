using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class tblRequestors : CommonModel
    {
        public int nRequestorID { get; set; }
        public int? nROIFacilityID { get; set; }
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

        
        public virtual tblROIFacilities nROIFacility { get; set; }
    }
}
