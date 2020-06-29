using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{   
    [Dapper.Contrib.Extensions.Table("tblFacilityLocations")]
    public partial class FacilityLocations : CommonModel
    {
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
        [StringLength(4, ErrorMessage = "Maximum 4 characters")]
        public string? sLocationCode { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum 30 characters")]
        public string sLocationName { get; set; }
        public string sNormalizedLocationName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters")]
        public string sLocationAddress { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        public string sPhoneNo { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        public string sFaxNo { get; set; }
        public string sConfigLogoName { get; set; }
        public string sConfigLogoData { get; set; }
        public string sConfigBackgroundName { get; set; }
        public string sConfigBackgroundData { get; set; }
        public string sAuthTemplate { get; set; }
        public string sAuthTemplateName { get; set; }
        public bool bLocationActiveStatus { get; set; }
        public int nAuthExpirationMonths { get; set; }
    }
}
