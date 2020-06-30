using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityRecordTypes")]

    public partial class FacilityRecordTypes
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityRecordTypeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRecordTypeID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Record Type Allowed")]
        public string sRecordTypeName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
