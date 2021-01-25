using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Dapper.Contrib.Extensions.Table("lnkFacilityShipmentTypes")]

    public class FacilityShipmentTypes : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityShipmentTypeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Shipment Type Id")]
        public int nShipmentTypeID { get; set; }     
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Location Id")]
        public int nFacilityLocationID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Shipment Type Allowed")]
        [DisplayName("Name")]
        public string sShipmentTypeName { get; set; }
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        #endregion
    }
}
