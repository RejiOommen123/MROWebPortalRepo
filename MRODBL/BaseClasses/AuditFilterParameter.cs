using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class AuditFilterParameter
    {
        #region Props
        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        public string sFacilityName { get; set; }
        public string sLocationName { get; set; }
        public string sAdminName { get; set; }
        #endregion
    }
}
