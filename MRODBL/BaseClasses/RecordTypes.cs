using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class RecordTypes : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRecordTypeID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Record Type Allowed")]
        public string sRecordTypeName { get; set; }
        public string sNormalizedRecordTypeName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Tool Tip Allowed")]
        public string sFieldToolTip { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        #endregion
    }
}
