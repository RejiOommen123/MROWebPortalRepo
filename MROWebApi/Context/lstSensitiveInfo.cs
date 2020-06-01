using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lstSensitiveInfo
    {
        public lstSensitiveInfo()
        {
            lnkROIFacilitySensitiveInfo = new HashSet<lnkROIFacilitySensitiveInfo>();
        }

        public int nSensitiveInfoID { get; set; }
        public string sSensitiveInfoName { get; set; }

        public virtual ICollection<lnkROIFacilitySensitiveInfo> lnkROIFacilitySensitiveInfo { get; set; }
    }
}
