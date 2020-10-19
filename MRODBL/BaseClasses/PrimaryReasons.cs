using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class PrimaryReasons : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nPrimaryReasonID { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Primary Reason Allowed")]
        [DisplayName("Name")]
        public string sPrimaryReasonName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedPrimaryReasonName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Tool Tip Allowed")]
        [DisplayName("Tooltip")]
        public string sFieldToolTip { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
