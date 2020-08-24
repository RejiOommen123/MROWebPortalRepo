using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public partial class TestSMTP
    {
        public string sSMTPUsername { get; set; }
        public string sSMTPPassword { get; set; }
        public string sSMTPUrl { get; set; }
        public string sOutboundEmail { get; set; }
        public string sToTestEmail { get; set; }
    }
}
