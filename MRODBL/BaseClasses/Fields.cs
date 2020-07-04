using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Fields
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFieldID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        [MaxLength]
        public string sFieldName { get; set; }
        [MaxLength]
        public string sNormalizedFieldName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
