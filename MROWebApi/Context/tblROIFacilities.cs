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
            lnkROIFacilityWayOfSendRecord = new HashSet<lnkROIFacilityWayOfSendRecord>();
            lnkROIFacilityConnection = new HashSet<lnkROIFacilityConnection>();
            lnkROIFacilityLocations = new HashSet<lnkROIFacilityLocations>();
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
       

        public virtual ICollection<lnkROIFacilityFieldMaps> lnkROIFacilityFieldMaps { get; set; }
        public virtual ICollection<lnkROIFacilityPrimaryReasons> lnkROIFacilityPrimaryReasons { get; set; }
        public virtual ICollection<lnkROIFacilityRecordTypes> lnkROIFacilityRecordTypes { get; set; }
        public virtual ICollection<lnkROIFacilitySensitiveInfo> lnkROIFacilitySensitiveInfo { get; set; }
        public virtual ICollection<lnkROIFacilityWayOfSendRecord> lnkROIFacilityWayOfSendRecord { get; set; } 
        public virtual ICollection<lnkROIFacilityConnection> lnkROIFacilityConnection { get; set; }
        public virtual ICollection<lnkROIFacilityLocations> lnkROIFacilityLocations { get; set; }
        public virtual ICollection<tblRequestors> tblRequestors { get; set; }
        public virtual ICollection<tblTempRequestors> tblTempRequestors { get; set; }
    }
}
