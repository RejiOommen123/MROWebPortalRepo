using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class MROConnectionString
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        public int nConnectionID { get; set; }
        public string sConnectionString { get; set; }
        public string sConnectionDisplayName { get; set; }
        [IgnorePropertyCompare]
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
