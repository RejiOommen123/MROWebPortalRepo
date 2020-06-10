using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class Fields
    {
        [Key]
        public int nFieldID { get; set; }
        public int nWizardID { get; set; }
        public string sFieldName { get; set; }
        public string sNormalizedFieldName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
