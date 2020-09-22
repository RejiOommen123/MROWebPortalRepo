using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class AuditFilterParameter
    {
        #region Props
        public string dtStart { get; set; }
        public string dtEnd { get; set; }
        public string sFacilityName { get; set; }
        public string sLocationName { get; set; }
        public string sAdminName { get; set; }
        #endregion
    }
}
