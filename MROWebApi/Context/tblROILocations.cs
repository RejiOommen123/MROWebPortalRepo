using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class tblROILocations : CommonModel
    {
        public tblROILocations()
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

        public virtual tblROIFacilities nROIFacility { get; set; }
        public virtual ICollection<tblRequestors> tblRequestors { get; set; }
    }
}
