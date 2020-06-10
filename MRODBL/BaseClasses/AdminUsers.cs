using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class AdminUsers 
    {
        [Key]
		public int nAdminUserID { get; set; }
		public string sUPN { get; set; }
		public string sName { get; set; }
		public string sEmail { get; set; }
        public DateTime dtCreated { get; set; }
        public int nUpdatedAdminUserID { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
