using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class AdminUsers 
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nAdminUserID { get; set; }
        [StringLength(1000, ErrorMessage = "Maximum 1000 charcters UPN Allowed")]
        public string sUPN { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 charcters Name Allowed")]
        public string sName { get; set; }
        [StringLength(1000, ErrorMessage = "Maximum 1000 charcters Email Allowed")]
        public string sEmail { get; set; }
        public DateTime dtCreated { get; set; }
        public DateTime dtLastUpdate { get; set; }
        public DateTime dtLastLoggedIn { get; set; }
        #endregion
    }
}
