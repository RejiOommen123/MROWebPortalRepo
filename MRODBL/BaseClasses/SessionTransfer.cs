using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class SessionTransfer
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nSessionTransferID { get; set; }
        [StringLength(255, ErrorMessage = "Maximum 255 characters ST Guid Allowed")]
        public string sSessionTranferGUID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityLocationID { get; set; }
        public int nRequesterID { get; set; }
        public DateTime dtCreated { get; set; }
        public DateTime dtExpired { get; set; }
        #endregion
    }
}
