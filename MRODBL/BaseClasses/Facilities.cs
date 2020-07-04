using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Facilities : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nROIFacilityID { get; set; }
        [MaxLength]
        public string sFacilityName { get; set; }
        [MaxLength]
        public string sDescription { get; set; }
        [MaxLength]
        public string sSMTPUsername { get; set; }
        [MaxLength]
        public string sSMTPPassword { get; set; }
        [MaxLength]
        public string sSMTPUrl { get; set; }
        [MaxLength]
        public string sFTPUsername { get; set; }
        [MaxLength]
        public string sFTPPassword { get; set; }
        [MaxLength]
        public string sFTPUrl { get; set; }
        [MaxLength]
        public string sOutboundEmail { get; set; }
        public bool bActiveStatus { get; set; }
        public bool bFacilityLogging { get; set; }
        public bool bRequestorEmailConfirm { get; set; }
        public bool bRequestorEmailVerify { get; set; }
        #endregion
    }
}
