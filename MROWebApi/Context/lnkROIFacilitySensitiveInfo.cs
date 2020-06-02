using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilitySensitiveInfo : CommonModel
    {
        public int nROIFacilitySensitiveInfoID { get; set; }
        public int nSensitiveInfoID { get; set; }
        public int nROIFacilityID { get; set; }
        public string sSensitiveInfoName { get; set; }

        public virtual tblROIFacilities nROIFacility { get; set; }
        public virtual lstSensitiveInfo nSensitiveInfo { get; set; }
    }
}
