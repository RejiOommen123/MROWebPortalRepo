using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityPrimaryReasons")]
    public partial class FacilityPrimaryReasons
    {
        #region Props
        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        //public int nFacilityPrimaryReasonID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nPrimaryReasonID { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Allowed")]
        public string sPrimaryReasonName { get; set; }
        public int? nFieldOrder { get; set; }
        public int nWizardID { get; set; }
        public bool bShow { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }

}

