using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Table("lstSensitiveInfo")]
    public partial class SensitiveInfo : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nSensitiveInfoID { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Sensitive Info allowed")]
        [DisplayName("Name")]
        [Write(false)]
        public string sSensitiveInfoName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedSensitiveInfoName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Sensitive Info Tool Tip allowed")]
        [DisplayName("Tooltip")]
        [Write(false)]
        public string sFieldToolTip { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("eXpress Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
