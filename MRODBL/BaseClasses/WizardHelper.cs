using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class WizardHelper
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nWizardHelperID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Helper Type allowed")]
        [DisplayName("Type")]
        public string sWizardHelperType { get; set; }
        [MaxLength]
        [DisplayName("Value")]
        public string sWizardHelperValue { get; set; }
        [IgnorePropertyCompare]
        public DateTime dtLastUpdate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Express Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
