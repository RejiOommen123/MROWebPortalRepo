using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityWizardHelper")]
    public partial class FacilityDisclaimers : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityWizardHelperID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Location Id")]
        public int nFacilityLocationID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Disclaimer Type Allowed")]
        [DisplayName("Type")]
        public string sWizardHelperType { get; set; }
        [DisplayName("Value")]
        public string sWizardHelperValue { get; set; }
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
