using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Fields
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFieldID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [MaxLength]
        [DisplayName("Field Name")]
        public string sFieldName { get; set; }
        [MaxLength]
        [DisplayName("Normalized Name")]
        public string sNormalizedFieldName { get; set; }
        [IgnorePropertyCompare]
        [DisplayName("Date")]
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
