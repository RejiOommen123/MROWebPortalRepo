using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class PrimaryReasons
    {
        [Key]
        public int nPrimaryReasonID { get; set; }
        public string sPrimaryReasonName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        public int nWizardID { get; set; }
    }
}
