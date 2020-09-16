using System;

namespace MRODBL.BaseClasses
{
    public class CommonModel
    {

        #region Common Props
        [IgnorePropertyCompare]
        public int nCreatedAdminUserID { get; set; }
        [IgnorePropertyCompare]
        public DateTime dtCreated { get; set; }
        [IgnorePropertyCompare]
        public int nUpdatedAdminUserID { get; set; }
        [IgnorePropertyCompare]
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
