using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilitySensitiveInfo")]

    public partial class FacilitySensitiveInfo
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilitySensitiveInfoID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nSensitiveInfoID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Sensitive Info Allowed")]
        public string sSensitiveInfoName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
