using MRODBL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class SendEmail
    {
        public int nFacilityID { get; set; }
        public int nLocationID { get; set; }
        public string sFromMailName { get; set; }
        public string sFromMailAddress { get; set; }
        public string sToMailName { get; set; }
        public string sToMailAddress { get; set; }
        public string sSubject { get; set; }
        public string sBody { get; set; }
        public DBConnectionInfo info { get; set; }
    }
}