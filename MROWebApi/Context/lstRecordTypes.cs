using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lstRecordTypes
    {
        public lstRecordTypes()
        {
            lnkROIFacilityRecordTypes = new HashSet<lnkROIFacilityRecordTypes>();
        }

        public int nRecordTypeID { get; set; }
        public string sRecordTypeName { get; set; }

        public virtual ICollection<lnkROIFacilityRecordTypes> lnkROIFacilityRecordTypes { get; set; }
    }
}
