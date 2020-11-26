using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilitySensitiveInfo")]

    public partial class FacilitySensitiveInfo : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nSensitiveInfoID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Sensitive Info Allowed")]
        [DisplayName("Name")]
        public string sSensitiveInfoName { get; set; }
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        #endregion
    }
}
