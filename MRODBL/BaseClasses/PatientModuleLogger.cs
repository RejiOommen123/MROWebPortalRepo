using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class PatientModuleLogger
    {
        [Key]
        public int nPMLID { get; set; }
        public int sUserIPAddress { get; set; }
        public string sFacilityGUID { get; set; }
        public string sWizardName { get; set; }
        public DateTime dtLogTime { get; set; }
    }
}
