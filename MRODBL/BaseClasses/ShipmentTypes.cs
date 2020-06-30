using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class ShipmentTypes
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nShipmentTypeID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Shipment Type allowed")]
        public string sShipmentTypeName { get; set; }
        public string sNormalizedShipmentTypeName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Shipment Type Tool Tip allowed")]
        public string sFieldToolTip { get; set; }
        public DateTime dtLastUpdate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        #endregion
    }
}
