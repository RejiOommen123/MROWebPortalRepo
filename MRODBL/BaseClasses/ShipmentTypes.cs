using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Table("lstShipmentTypes")]
    public class ShipmentTypes : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nShipmentTypeID { get; set; }        
        [StringLength(100, ErrorMessage = "Maximum 100 characters Shipment Type allowed")]
        [DisplayName("Name")]
        [Write(false)]
        public string sShipmentTypeName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedShipmentTypeName { get; set; }    
        [StringLength(500, ErrorMessage = "Maximum 500 characters Shipment Type Tool Tip allowed")]
        [DisplayName("Tooltip")]
        [Write(false)]
        public string sFieldToolTip { get; set; }        
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("eXpress Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
