using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Facilities : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("ROI Facility Id")]
        public int nROIFacilityID { get; set; }
        [MaxLength]
        [DisplayName("Name")]
        public string sFacilityName { get; set; }
        [MaxLength]
        [DisplayName("Description")]
        public string sDescription { get; set; }
        [MaxLength]
        [DisplayName("SMTP Username")]
        public string sSMTPUsername { get; set; }
        [MaxLength]
        [DisplayName("SMTP Password")]
        public string sSMTPPassword { get; set; }
        [MaxLength]
        [DisplayName("SMTP URL")]
        public string sSMTPUrl { get; set; }
        [MaxLength]
        [DisplayName("FTP Username")]
        public string sFTPUsername { get; set; }
        [MaxLength]
        [DisplayName("FTP Password")]
        public string sFTPPassword { get; set; }
        [MaxLength]
        [DisplayName("FTP URL")]
        public string sFTPUrl { get; set; }
        [MaxLength]
        [DisplayName("Outbound Email")]
        public string sOutboundEmail { get; set; }
        [DisplayName("Active Status")]
        public bool bActiveStatus { get; set; }
        [DisplayName("Logging for facility")]
        public bool bFacilityLogging { get; set; }
        [DisplayName("Confirm Email Switch")]
        public bool bRequestorEmailConfirm { get; set; }
        [DisplayName("Verify Email Switch")]
        public bool bRequestorEmailVerify { get; set; }
        #endregion
    }
}
