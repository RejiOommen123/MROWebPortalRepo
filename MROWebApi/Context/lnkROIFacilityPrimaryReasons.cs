using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilityPrimaryReasons : CommonModel
    {
        public int nROIFacilityPrimaryReasonID { get; set; }
        public int nPrimaryReasonID { get; set; }
        public int nROIFacilityID { get; set; }
        public string sPrimaryReasonName { get; set; }

        public virtual lstPrimaryReasons nPrimaryReason { get; set; }
        public virtual tblROIFacilities nROIFacility { get; set; }
    }
}
