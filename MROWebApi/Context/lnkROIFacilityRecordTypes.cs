using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilityRecordTypes : CommonModel
    {
        public int nROIFacilityRecordTypeID { get; set; }
        public int nROIFacilityID { get; set; }
        public int nRecordTypeID { get; set; }
        public string sRecordTypeName { get; set; }

        public virtual tblROIFacilities nROIFacility { get; set; }
        public virtual lstRecordTypes nRecordType { get; set; }
    }
}
