using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class FacilityConnections :CommonModel
    {
		[Key]
		public int nFacilityConnectionID { get; set; }
		public int nFacilityID { get; set; }
		public string sGUID { get; set; }
		public string sConnectionString { get; set; }
		
	}
}
