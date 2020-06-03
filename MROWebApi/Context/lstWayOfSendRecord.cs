using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Context
{
    public class lstWayOfSendRecord
    {
        public lstWayOfSendRecord()
        {
            lnkROIFacilityWayOfSendRecord = new HashSet<lnkROIFacilityWayOfSendRecord>();
        }

        public int nWayOfSendRecordID { get; set; }
        public string sWayOfSendRecordName { get; set; }

        public virtual ICollection<lnkROIFacilityWayOfSendRecord> lnkROIFacilityWayOfSendRecord { get; set; }
    }
}
