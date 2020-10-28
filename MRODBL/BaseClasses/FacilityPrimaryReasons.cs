using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityPrimaryReasons")]
    public partial class FacilityPrimaryReasons : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nPrimaryReasonID { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Primary Reason Allowed")]
        [DisplayName("Name")]
        public string sPrimaryReasonName { get; set; }
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        [DisplayName("Tooltip")]
        public string sFieldToolTip { get; set; }
        #endregion
    }

}

