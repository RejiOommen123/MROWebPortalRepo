using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityShipmentTypes")]

    public class FacilityShipmentTypes
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityShipmentTypeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nShipmentTypeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Shipment Type Allowed")]
        public string sShipmentTypeName { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
