﻿using System;
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
        [StringLength(4, ErrorMessage = "Maximum 4 characters Location Code Allowed")]
        public string? sLocationCode { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Maximum 40 characters location Name Allowed")]
        public string sLocationName { get; set; }
        public string sNormalizedLocationName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters Location Address Allowed")]
        public string sLocationAddress { get; set; }
        [Required]
        public string sPhoneNo { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters Fax Number Allowed")]
        public string sFaxNo { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Logo Name Allowed")]
        public string sConfigLogoName { get; set; }
        [MaxLength]
        public string sConfigLogoData { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Background Name Allowed")]
        public string sConfigBackgroundName { get; set; }
        [MaxLength]
        public string sConfigBackgroundData { get; set; }
        [MaxLength]
        public string sAuthTemplate { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 characters Authentication Template Name Allowed")]
        public string sAuthTemplateName { get; set; }
        public bool bLocationActiveStatus { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nAuthExpirationMonths { get; set; }
        #endregion
    }
}
