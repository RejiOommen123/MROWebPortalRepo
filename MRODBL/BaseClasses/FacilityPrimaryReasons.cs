using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    [Table("lnkFacilityPrimaryReasons")]
    public partial class FacilityPrimaryReasons
    {
        [Key]
        public int nFacilityPrimaryReasonID { get; set; }
        public int nFacilityID { get; set; }
        public int nPrimaryReasonID { get; set; }
        public string sPrimaryReasonName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
    
}

