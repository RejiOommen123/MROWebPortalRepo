using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Wizards
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Name allowed")]
        public string sWizardName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Wizard Description allowed")]
        public string sWizardDescription { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
