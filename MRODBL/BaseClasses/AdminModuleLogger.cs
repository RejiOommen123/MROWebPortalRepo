using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class IgnorePropertyCompareAttribute : Attribute { }
    [Dapper.Contrib.Extensions.Table("tblAdminModuleLogger")]
    public class AdminModuleLogger
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nAMLID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nAdminUserID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Module Name Allowed")]
        public string sModuleName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Event Name Allowed")]
        public string sEventName { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 characters Description Allowed")]
        public string sDescription { get; set; }
        public string sNewValue { get; set; }
        public string sOldValue { get; set; }
        public DateTime dtLogTime { get; set; }
        #endregion
    }
}
