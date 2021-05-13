using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class AddOrganization
    {
        #region Props
        public FacilityOrganizations cOrganization { get; set; }
        public List<int> locationIds { get; set; }
        #endregion
    }
}
