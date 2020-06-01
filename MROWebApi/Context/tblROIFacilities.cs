using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class tblROIFacilities : CommonModel
    {
        public tblROIFacilities()
        {
            lnkROIFacilityFieldMaps = new HashSet<lnkROIFacilityFieldMaps>();
            lnkROIFacilityPrimaryReasons = new HashSet<lnkROIFacilityPrimaryReasons>();
            lnkROIFacilityRecordTypes = new HashSet<lnkROIFacilityRecordTypes>();
            lnkROIFacilitySensitiveInfo = new HashSet<lnkROIFacilitySensitiveInfo>();
            tblROILocations = new HashSet<tblROILocations>();
            tblRequestors = new HashSet<tblRequestors>();
            tblTempRequestors = new HashSet<tblTempRequestors>();
        }

        public int nROIFacilityID { get; set; }
        public string sFacilityName { get; set; }
        public string sDescription { get; set; }
        public string sSMTPUsername { get; set; }
        public string sSMTPPassword { get; set; }
        public string sSMTPUrl { get; set; }
        public string sFTPUsername { get; set; }
        public string sFTPPassword { get; set; }
        public string sFTPUrl { get; set; }
        public string sOutboundEmail { get; set; }
        public bool bActiveStatus { get; set; }
        public string sConfigShowFields { get; set; }
        public string sConfigShowWizards { get; set; }
        public string sConfigFacilityLogo { get; set; }

        public virtual ICollection<lnkROIFacilityFieldMaps> lnkROIFacilityFieldMaps { get; set; }
        public virtual ICollection<lnkROIFacilityPrimaryReasons> lnkROIFacilityPrimaryReasons { get; set; }
        public virtual ICollection<lnkROIFacilityRecordTypes> lnkROIFacilityRecordTypes { get; set; }
        public virtual ICollection<lnkROIFacilitySensitiveInfo> lnkROIFacilitySensitiveInfo { get; set; }
        public virtual ICollection<tblROILocations> tblROILocations { get; set; }
        public virtual ICollection<tblRequestors> tblRequestors { get; set; }
        public virtual ICollection<tblTempRequestors> tblTempRequestors { get; set; }
    }
}
