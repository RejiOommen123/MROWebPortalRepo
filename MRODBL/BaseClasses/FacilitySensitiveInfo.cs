using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilitySensitiveInfo")]

    public partial class FacilitySensitiveInfo
    {
        #region Props
        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        //public int nFacilitySensitiveInfoID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nSensitiveInfoID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Sensitive Info Allowed")]
        public string sSensitiveInfoName { get; set; }
        public int? nFieldOrder { get; set; }
        public int nWizardID { get; set; }
        public bool bShow { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
