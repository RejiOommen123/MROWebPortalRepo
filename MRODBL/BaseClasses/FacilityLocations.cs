using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("tblFacilityLocations")]
    public partial class FacilityLocations : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityLocationID { get; set; }
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
        public string sLocationName { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 characters Normalized Location Name Allowed")]
        [DisplayName("Normalized Name")]
        public string sNormalizedLocationName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters Location Address Allowed")]
        [DisplayName("Address")]
        public string sLocationAddress { get; set; }
        [Required]
        [DisplayName("Phone No")]
        public string sPhoneNo { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters Fax Number Allowed")]
        [DisplayName("Fax No")]
        public string sFaxNo { get; set; }
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
        public string sAuthTemplate { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Authentication Template Name Allowed")]
        [DisplayName("Template Name")]
        public string sAuthTemplateName { get; set; }
        [DisplayName("Active Status")]
        public bool bLocationActiveStatus { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Auth Expiration Month")]
        public int nAuthExpirationMonths { get; set; }
        [DisplayName("Include In Facility Level")]
        public bool bIncludeInFacilityLevel { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sGUID { get; set; }
        #endregion
    }
}
