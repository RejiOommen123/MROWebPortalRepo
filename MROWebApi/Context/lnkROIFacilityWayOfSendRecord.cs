using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Context
{
    public class lnkROIFacilityWayOfSendRecord : CommonModel
    {
        public int nROIFacilityWayOfSendRecordID { get; set; }
        public int nROIFacilityID { get; set; }
        public int nWayOfSendRecordID { get; set; }
        public string sWayOfSendRecordName { get; set; }

        public virtual tblROIFacilities nROIFacility { get; set; }
        public virtual lstWayOfSendRecord nWayOfSendRecord { get; set; }
    }
}
