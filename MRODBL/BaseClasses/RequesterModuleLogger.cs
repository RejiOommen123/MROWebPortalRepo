using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class RequesterModuleLogger
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nRMLID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(75, ErrorMessage = "Maximum 75 characters Wizard Name Allowed")]
        public string sWizardName { get; set; }
        public int nRequesterID { get; set; }
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters Description Allowed")]
        public string sDescription { get; set; }
        public DateTime dtLogTime { get; set; }
        #endregion
    }
}
