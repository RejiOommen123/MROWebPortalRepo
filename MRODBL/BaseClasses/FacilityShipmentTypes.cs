using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityShipmentTypes")]

    public class FacilityShipmentTypes
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nShipmentTypeID { get; set; }
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Shipment Type Allowed")]
        public string sShipmentTypeName { get; set; }
        public int? nFieldOrder { get; set; }
        public int nWizardID { get; set; }
        public bool bShow { get; set; }
        public DateTime dtLastUpdate { get; set; }
        #endregion
    }
}
