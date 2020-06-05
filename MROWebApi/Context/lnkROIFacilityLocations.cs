using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lnkROIFacilityLocations : CommonModel
    {
        public lnkROIFacilityLocations()
        {
        }

        public int nROIFacilityID { get; set; }
        public int nLocationID { get; set; }
        public string sLocationCode { get; set; }
        public string sLocationName { get; set; }
        public string sLocationAddress { get; set; }
        public string sPhoneNo { get; set; }
        public string sFaxNo { get; set; }
        public string sConfigLogoName { get; set; }
        public string sConfigLogoData { get; set; }
        public string sConfigBackgroundName { get; set; }
        public string sConfigBackgroundData { get; set; }

        public virtual tblROIFacilities nROIFacility { get; set; }
   
    }
}
