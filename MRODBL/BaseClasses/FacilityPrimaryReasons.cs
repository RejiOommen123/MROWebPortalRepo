using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityPrimaryReasons")]
    public partial class FacilityPrimaryReasons
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityPrimaryReasonID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nPrimaryReasonID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Primary Reason Allowed")]
        public string sPrimaryReasonName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }

}

