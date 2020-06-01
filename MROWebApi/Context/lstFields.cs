using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lstFields
    {
        public lstFields()
        {
            lnkROIFacilityFieldMaps = new HashSet<lnkROIFacilityFieldMaps>();
        }

        public int nFieldID { get; set; }
        public int nWizardID { get; set; }
        public string sFieldName { get; set; }
        public string sNormalizedFieldName { get; set; }

        public virtual lstWizards nWizard { get; set; }
        public virtual ICollection<lnkROIFacilityFieldMaps> lnkROIFacilityFieldMaps { get; set; }
    }
}
