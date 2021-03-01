using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class Waiver : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nWaiverID { get; set; }        
        [StringLength(100, ErrorMessage = "Maximum 100 characters Waiver allowed")]
        [DisplayName("Name")]
        public string sWaiverName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedWaiverName { get; set; }    
        [StringLength(500, ErrorMessage = "Maximum 500 characters Waiver Tool Tip allowed")]
        [DisplayName("Tooltip")]
        public string sFieldToolTip { get; set; }        
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Express Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
