using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("tblFacilityOrganizations")]
    public partial class FacilityOrganizations : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityOrgID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("ROI Location Id")]
        public int nROILocationID { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Maximum 8 characters Location Code Allowed")]
        [DisplayName("Location Code")]
        public string? sLocationCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters location Name Allowed")]
        [DisplayName("Name")]
        public string sOrgName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Logo Name Allowed")]
        [DisplayName("Logo Name")]
        public string sConfigLogoName { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sConfigLogoData { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Background Name Allowed")]
        [DisplayName("Background Name")]
        public string sConfigBackgroundName { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sConfigBackgroundData { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sGUID { get; set; }
        [DisplayName("Primary Timeout")]
        public int nPrimaryTimeout { get; set; }
        [DisplayName("Secondary Timeout")]
        public int nSecondaryTimeout { get; set; }
        [MaxLength]
        [DisplayName("Support Email")]
        public string sSupportEmail { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Master Location Id")]
        public int? nFacilityMasterLocationID { get; set; }
        #endregion
    }
}
