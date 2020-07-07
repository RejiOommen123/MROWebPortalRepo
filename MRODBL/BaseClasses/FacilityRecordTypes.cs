using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityRecordTypes")]

    public partial class FacilityRecordTypes
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRecordTypeID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Record Type Allowed")]
        public string sRecordTypeName { get; set; }
        public int? nFieldOrder { get; set; }
        public int nWizardID { get; set; }
        public bool bShow { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
