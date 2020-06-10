using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    [Table("lnkFacilityShipmentTypes")]

    public class FacilityShipmentTypes
    {
        [Key]
        public int nFacilityShipmentTypeID { get; set; }
        public int nShipmentTypeID { get; set; }
        public int nFacilityID { get; set; }
        public string sShipmentTypeName { get; set; }
        public DateTime dtLastUpdate { get; set; }
    }
}
