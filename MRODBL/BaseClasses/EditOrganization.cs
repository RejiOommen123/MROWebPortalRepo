using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class EditOrganization
    {
        #region Props
        public FacilityOrganizations cOrganization { get; set; }
        public List<int> addedlocationIds { get; set; }
        public List<int> removedlocationIds { get; set; }
        #endregion
    }
}
