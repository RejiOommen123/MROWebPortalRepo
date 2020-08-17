using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class FacilityConnections
    {
		#region Props
		[Dapper.Contrib.Extensions.Key]
		[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
		public int nFacilityConnectionID { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
		public int nFacilityID { get; set; }
		[MaxLength]
		public string sGUID { get; set; }
		[StringLength(200, ErrorMessage = "Maximum 200 charcters GUID Allowed")]
		public string sConnectionString { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
		public int nCreatingAdminUserID { get; set; }
		public DateTime dtCreated { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
		public int nConnectionID { get; set; }
		public int nUpdateAdminUserID { get; set; }
		public DateTime dtLastUpdate { get; set; }
		#endregion
	}
}
