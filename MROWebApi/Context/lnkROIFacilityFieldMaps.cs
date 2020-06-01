using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilityFieldMaps : CommonModel
    {
        public int nROIFacilityFieldMapID { get; set; }
        public int nROIFacilityID { get; set; }
        public int nFieldID { get; set; }
        public bool bShow { get; set; }

        public virtual lstFields nField { get; set; }
        public virtual tblROIFacilities nROIFacility { get; set; }
    }
}
