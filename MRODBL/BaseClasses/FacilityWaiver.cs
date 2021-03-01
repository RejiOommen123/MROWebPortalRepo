using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityWaiver")]

    public partial class FacilityWaiver : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityWaiverID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Waiver Id")]
        public int nWaiverID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Location Id")]
        public int nFacilityLocationID { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Sensitive Info Allowed")]
        [DisplayName("Name")]
        public string sWaiverName { get; set; }
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        [DisplayName("Express Id")]
        public int nWizardID { get; set; }
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        #endregion
    }
}
