using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Requesters
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRequesterID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nLocationID { get; set; }
        public string sSelectedLocation { get; set; }
        [Required]
        public bool bAreYouPatient { get; set; }
        [StringLength(30, ErrorMessage = "Maximum 30 characters Relative Name allowed")]
        public string sRelativeName { get; set; }
        [StringLength(20, ErrorMessage = "Maximum 20 characters Relation to Patient allowed")]
        public string sRelationToPatient { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Maximum 20 characters Patient First Name allowed")]
        public string sPatientFirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient Last Name allowed")]
        public string sPatientLastName { get; set; }
        [StringLength(1, ErrorMessage = "Maximum 1 character Middle Initial allowed")]
        public string sPatientMiddleInitial { get; set; }
        //public bool bIsPatientMinor { get; set; }
        //public bool bIsPatientDeceased { get; set; }
        [Required]
        public DateTime? dtPatientDOB { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum 30 characters Email ID allowed")]
        //public string sPatientEmailId { get; set; }
        public string sRequesterEmailId { get; set; }

        //[StringLength(30, ErrorMessage = "Maximum 30 characters Confirm Email ID allowed")]
        //public string sConfirmEmailId { get; set; }
        public bool bConfirmReport { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters Zip Code allowed")]
        public string sAddZipCode { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters City allowed")]
        public string sAddCity { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters State allowed")]
        public string sAddState { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Maximum 20 characters Street Address allowed")]
        public string sAddStreetAddress { get; set; }
        public DateTime? dtRecordRangeStart { get; set; }
        public DateTime? dtRecordRangeEnd { get; set; }
        public string[] sSelectedRecordTypes { get; set; }
        public string[] sSelectedPrimaryReasons { get; set; }
        public string sOtherReasons { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum 30 characters Release To allowed")]
        public string sReleaseTo { get; set; }
        public string[] sSelectedShipmentTypes { get; set; }
        public string[] selectedSensitiveInfo { get; set; }
        public DateTime? dtAuthExpire { get; set; }
        public string sAuthSpecificEvent { get; set; }
        public bool bDeadlineStatus { get; set; }
        public DateTime? dtDeadline { get; set; }
        public string sAdditionalData { get; set; }
        public string sIdentityIdName { get; set; }
        [Required]
        [MaxLength]
        public string sIdentityImage { get; set; }
        [MaxLength]
        public string sSignatureData { get; set; }
        public bool bRequestAnotherRecord { get; set; }
        [Range(0, 5, ErrorMessage = "Rating can be between 0 to 5")]
        public int nFeedbackRating { get; set; }
        public string sFeedbackComment { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Maximum 15 characters Phone Number allowed")]
        public string sPhoneNo { get; set; }
        [MaxLength]
        public string sPDF { get; set; }
        public DateTime dtLastUpdate { get; set; }
        public string sSTFaxCompAdd { get; set; }
        [StringLength(30, ErrorMessage = "Maximum 30 characters Shipment Type Email ID allowed")]
        public string sSTEmailId { get; set; }
        //[StringLength(30, ErrorMessage = "Maximum 30 characters Shipment Type Confirm Email ID allowed")]
        //public string sSTConfirmEmailId { get; set; }
        public string sSTMailCompAdd { get; set; }
        public DateTime? dtPickUp { get; set; }
        #endregion
    }
}
