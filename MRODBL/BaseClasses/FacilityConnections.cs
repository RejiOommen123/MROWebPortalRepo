using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class FacilityConnections
    {
		[Key]
		public int nFacilityConnectionID { get; set; }
		public int nFacilityID { get; set; }
		public string sGUID { get; set; }
		public string sConnectionString { get; set; }

		public int nCreatingAdminUserID { get; set; } //nCreatedAdminUserID
		public DateTime dtCreated { get; set; } //dtCreated
		public int nUpdateAdminUserID { get; set; }  //nUpdatedAdminUserID
		public DateTime dtLastUpdate { get; set; } //dtLastUpdate
	}
}
