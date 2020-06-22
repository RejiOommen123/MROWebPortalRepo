using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class WizardHelper
    {
        [Key]
        public int nWizardHelperID { get; set; }
        public string sWizardHelperType { get; set; }
        public string sWizardHelperValue { get; set; }
        public DateTime dtLastUpdate { get; set; }
        public int nWizardID { get; set; }
    }
}
