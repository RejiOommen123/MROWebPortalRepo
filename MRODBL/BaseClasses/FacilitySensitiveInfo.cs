using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    [Table("lnkFacilitySensitiveInfo")]

    public partial class FacilitySensitiveInfo
    {
        [Key]
        public int nFacilitySensitiveInfoID { get; set; }
        public int nSensitiveInfoID { get; set; }
        public int nFacilityID { get; set; }
        public string sSensitiveInfoName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
