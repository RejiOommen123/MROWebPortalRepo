using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class PrimaryReasons
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nPrimaryReasonID { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Primary Reason Allowed")]
        public string sPrimaryReasonName { get; set; }
        public string sNormalizedPrimaryReasonName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Tool Tip Allowed")]
        public string sFieldToolTip { get; set; }
        public DateTime dtLastUpdate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        #endregion
    }
}
