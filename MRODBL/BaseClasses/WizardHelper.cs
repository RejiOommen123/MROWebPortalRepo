using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class WizardHelper
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardHelperID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Helper Type allowed")]
        public string sWizardHelperType { get; set; }
        [MaxLength]
        public string sWizardHelperValue { get; set; }
        public DateTime dtLastUpdate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        #endregion
    }
}
