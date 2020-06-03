using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilityLocations : CommonModel
    {
        public lnkROIFacilityLocations()
        {
            tblRequestors = new HashSet<tblRequestors>();
        }

        public int nROIFacilityID { get; set; }
        public int nLocationID { get; set; }
        public int? sLocationCode { get; set; }
        public string sLocationName { get; set; }
        public string sLocationAddress { get; set; }
        public int? nPhoneNo { get; set; }
        public int? nFaxNo { get; set; }
        public string sConfigFacilityLogo { get; set; }
        public string sConfigBackgroundImg { get; set; }

        public virtual tblROIFacilities nROIFacility { get; set; }
        public virtual ICollection<tblRequestors> tblRequestors { get; set; }
    }
}
