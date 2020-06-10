using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    [Table("lnkFacilityRecordTypes")]

    public partial class FacilityRecordTypes
    {
        [Key]
        public int nFacilityRecordTypeID { get; set; }
        public int nFacilityID { get; set; }
        public int nRecordTypeID { get; set; }
        public string sRecordTypeName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
