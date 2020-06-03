using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Context
{
    public class lnkROIFacilityConnection : CommonModel
    {
        public int nROIFacilityConnectionID { get; set; }
        public string sGUID { get; set; }
        public int nROIFacilityID { get; set; }
       
        public tblROIFacilities nROIFacility { get; set; }
    }
}
