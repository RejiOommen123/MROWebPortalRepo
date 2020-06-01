using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lstPrimaryReasons
    {
        public lstPrimaryReasons()
        {
            lnkROIFacilityPrimaryReasons = new HashSet<lnkROIFacilityPrimaryReasons>();
        }

        public int nPrimaryReasonID { get; set; }
        public string sPrimaryReasonName { get; set; }

        public virtual ICollection<lnkROIFacilityPrimaryReasons> lnkROIFacilityPrimaryReasons { get; set; }
    }
}
