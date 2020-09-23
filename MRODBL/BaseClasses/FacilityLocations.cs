using System;
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
        public int nFacilityLocationID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nROILocationID { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Maximum 8 characters Location Code Allowed")]
        public string? sLocationCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters location Name Allowed")]
        public string sLocationName { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 characters Normalized Location Name Allowed")]
        public string sNormalizedLocationName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters Location Address Allowed")]
        public string sLocationAddress { get; set; }
        [Required]
        public string sPhoneNo { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters Fax Number Allowed")]
        public string sFaxNo { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Logo Name Allowed")]
        public string sConfigLogoName { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sConfigLogoData { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Background Name Allowed")]
        public string sConfigBackgroundName { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sConfigBackgroundData { get; set; }
        [IgnorePropertyCompare]
        [MaxLength]
        public string sAuthTemplate { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Authentication Template Name Allowed")]
        public string sAuthTemplateName { get; set; }
        public bool bLocationActiveStatus { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nAuthExpirationMonths { get; set; }
        #endregion
    }
}
