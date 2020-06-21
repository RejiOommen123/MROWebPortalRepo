
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRODBL.BaseClasses
{
    public partial class Facilities : CommonModel
    {
        [Key]
        public int nFacilityID { get; set; }
        public int nROIFacilityID { get; set; }
        public string sFacilityName { get; set; }
        public string sDescription { get; set; }
        public string sSMTPUsername { get; set; }
        public string sSMTPPassword { get; set; }
        public string sSMTPUrl { get; set; }
        public string sFTPUsername { get; set; }
        public string sFTPPassword { get; set; }
        public string sFTPUrl { get; set; }
        public string sOutboundEmail { get; set; }
        public bool bActiveStatus { get; set; }
        public string sConfigShowFields { get; set; }
        public string sConfigShowWizards { get; set; }
        public int nFacLocCount { get; set; }
        public bool bFacilityLogging { get; set; }
    }
}
