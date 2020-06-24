using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class AdminModuleLogger
    {
        [Key]
        public int nAMLID { get; set;}
        public int nAdminUserID { get; set; }
        public string sModuleName { get; set; }
        public string sEventName { get; set; }
        public string sDescription { get; set; }
        public DateTime dtLogTime { get; set; }
    }
}
