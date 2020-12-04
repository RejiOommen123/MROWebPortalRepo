using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Table("tblRequesters")]
    public partial class Requesters
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRequesterID { get; set; }
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nFacilityID { get; set; }
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nLocationID { get; set; }
        public string sSelectedLocation { get; set; }
        public string sSelectedLocationName { get; set; }
        [Required]
        public bool bAreYouPatient { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Relative Name allowed")]
        public string sRelativeFirstName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Relative Name allowed")]
        public string sRelativeLastName { get; set; }
        public string sSelectedRelation { get; set; }
        public string sSelectedRelationName { get; set; }
        [Write(false)]
        public string[] sRelativeFileArray { get; set; }
        public string[] sRelativeFileNameArray { get; set; }
        //[Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient First Name allowed")]
        public string sPatientFirstName { get; set; }
        //[Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient Last Name allowed")]
        public string sPatientLastName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 character Middle Name allowed")]
        public string sPatientMiddleName { get; set; }

        
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient First Name allowed")]
        public string sPatientPreviousFirstName { get; set; }
       
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient Last Name allowed")]
        public string sPatientPreviousLastName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 character Middle Name allowed")]
        public string sPatientPreviousMiddleName { get; set; }
        public bool bPatientNameChanged { get; set; }

        //[Required]
        public DateTime? dtPatientDOB { get; set; }
        //[Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters Email ID allowed")]
        public string sRequesterEmailId { get; set; }
        public bool bConfirmReport { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters Zip Code allowed")]
        public string sAddZipCode { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters City allowed")]
        public string sAddCity { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 100 characters State allowed")]
        public string sAddState { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Street Address allowed")]
        public string sAddStreetAddress { get; set; }
        public string sAddApartment { get; set; }
        public DateTime? dtRecordRangeStart { get; set; }
        public DateTime? dtRecordRangeEnd { get; set; }
        public bool bRecordMostRecentVisit { get; set; }
        public bool bSpecifyVisit { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Specify Visit Text allowed")]
        public string sSpecifyVisitText { get; set; }        
        public string[] sSelectedRecordTypes { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Other RT Text allowed")]
        public string sOtherRTText { get; set; }        
        public bool bRTManualSelection { get; set; }
        public string[] sSelectedPrimaryReasons { get; set; }
        public string sSelectedPrimaryReasonsName { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 characters Release To allowed")]
        public string sReleaseTo { get; set; }
        public string sReleaseToName { get; set; }
        public string[] sSelectedSensitiveInfo { get; set; }
        public DateTime? dtAuthExpire { get; set; }
        public string sAuthSpecificEvent { get; set; }
        public bool bDeadlineStatus { get; set; }
        public DateTime? dtDeadline { get; set; }
        public string sAdditionalData { get; set; }
        public string sIdentityIdName { get; set; }
        [MaxLength]
        public string sIdentityImage { get; set; }
        [MaxLength]
        public string sSignatureData { get; set; }
        public bool bRequestAnotherRecord { get; set; }
        [Range(0, 5, ErrorMessage = "Rating can be between 0 to 5")]
        public int nFeedbackRating { get; set; }
        public string sFeedbackComment { get; set; }
        [StringLength(15, ErrorMessage = "Maximum 15 characters Phone Number allowed")]
        public string sPhoneNo { get; set; }
        public bool bPhoneNoVerified { get; set; }
        
        [MaxLength]
        public string sPDF { get; set; }


        public string[] sSelectedShipmentTypes { get; set; }
        public string sSelectedShipmentTypesName { get; set; }

        public string sSTEmailAddress { get; set; }
        public string sSTFaxNumber { get; set; }
        public string sSTAddZipCode { get; set; }
        public string sSTAddCity { get; set; }
        public string sSTAddState { get; set; }
        public string sSTAddStreetAddress { get; set; }
        public string sSTAddApartment { get; set; }


        public string sRecipientFirstName { get; set; }
        public string sRecipientLastName { get; set; }
        public string sRecipientOrganizationName { get; set; }


        public string sRecipientAddZipCode { get; set; }
        public string sRecipientAddCity { get; set; }
        public string sRecipientAddState { get; set; }
        public string sRecipientAddStreetAddress { get; set; }
        public string sRecipientAddApartment { get; set; }


        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Name allowed")]
        public string sWizardName { get; set; }
        public bool bRequestorFormSubmitted { get; set; }

        public DateTime dtLastUpdate { get; set; }
        public string sOSInfo { get; set; }
        public string sBrowserInfo { get; set; }
        public bool bEmailVerified { get; set; }
        public bool bForceCompliance { get; set; }        

        [MaxLength]
        public string sGUID { get; set; }
        #endregion
    }
}
