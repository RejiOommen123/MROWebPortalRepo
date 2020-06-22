using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class RecordTypes
    {
        [Key]
        public int nRecordTypeID { get; set; }
        public string sRecordTypeName { get; set; }
        public string sNormalizedRecordTypeName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        public int nWizardID { get; set; }

    }
}
