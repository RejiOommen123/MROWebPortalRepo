using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityWizardHelper")]
    public partial class FacilityDisclaimers : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityWizardHelperID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Disclaimer Type Allowed")]
        public string sWizardHelperType { get; set; }
        public string sWizardHelperValue { get; set; }
        public int nWizardID { get; set; }
        #endregion
    }
}
