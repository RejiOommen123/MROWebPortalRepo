using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class RequesterEventLogger
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRELID { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 characters Event Name Allowed")]
        public string sEventName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Wizard Name Allowed")]
        public string sWizardName { get; set; }
        public int nRequesterID { get; set; }
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters Description Allowed")]
        public string sDescription { get; set; }
        public DateTime dtLogTime { get; set; }
        #endregion
    }
}
