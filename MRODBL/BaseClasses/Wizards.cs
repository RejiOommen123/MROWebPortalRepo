using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class Wizards
    { 
        [Key]
        public int nWizardID { get; set; }
        public string sWizardName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
