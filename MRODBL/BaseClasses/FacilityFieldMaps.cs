using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityFieldMaps")]
    public partial class FacilityFieldMaps : CommonModel
    {
        #region Props
        [Required]
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Required]
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFieldID { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [Required]
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        #endregion
    }
}
