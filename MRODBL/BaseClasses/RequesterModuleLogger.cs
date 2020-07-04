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
        [StringLength(50, ErrorMessage = "Maximum 50 characters IP Address Allowed")]
        public string sUserIPAddress { get; set; }
        [MaxLength]
        public string sFacilityGUID { get; set; }
        [StringLength(75, ErrorMessage = "Maximum 75 characters Wizard Name Allowed")]
        public string sWizardName { get; set; }
        public int nRequesterID { get; set; }
        public DateTime dtLogTime { get; set; }
        #endregion
    }
}
