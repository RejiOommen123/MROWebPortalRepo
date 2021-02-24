using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class RecordTypes : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nRecordTypeID { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Record Type Allowed")]
        [DisplayName("Name")]
        public string sRecordTypeName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedRecordTypeName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Tool Tip Allowed")]
        [DisplayName("Tooltip")]
        public string sFieldToolTip { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Express Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
