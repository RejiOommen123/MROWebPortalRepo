using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class Wizards
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nWizardID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Name allowed")]
        [DisplayName("Name")]
        public string sWizardName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Wizard Description allowed")]
        [DisplayName("Description")]
        public string sWizardDescription { get; set; }
        [IgnorePropertyCompare]
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
