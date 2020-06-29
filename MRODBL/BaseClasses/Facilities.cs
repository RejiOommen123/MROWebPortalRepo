
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRODBL.BaseClasses
{
    public partial class Facilities : CommonModel
    {
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nROIFacilityID { get; set; }
        [Required]
        [MaxLength]
        public string sFacilityName { get; set; }
        [Required]
        [MaxLength]
        public string sDescription { get; set; }
        [Required]
        [MaxLength]
        public string sSMTPUsername { get; set; }
        [Required]
        [MaxLength]
        public string sSMTPPassword { get; set; }
        [Required]
        [MaxLength]
        public string sSMTPUrl { get; set; }
        [Required]
        [MaxLength]
        public string sFTPUsername { get; set; }
        [Required]
        [MaxLength]
        public string sFTPPassword { get; set; }
        [Required]
        [MaxLength]
        public string sFTPUrl { get; set; }
        [Required]
        [MaxLength]
        [EmailAddress]
        public string sOutboundEmail { get; set; }
        public bool bActiveStatus { get; set; }
        public string sConfigShowFields { get; set; }
        public string sConfigShowWizards { get; set; }
        //public int nFacLocCount { get; set; }
        public bool bFacilityLogging { get; set; }
        public bool bRequestorEmailConfirm { get; set; }
    }
}
