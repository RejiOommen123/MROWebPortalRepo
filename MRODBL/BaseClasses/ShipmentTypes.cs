using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class ShipmentTypes
    {
        [Key]
        public int nShipmentTypeID { get; set; }
        public string sShipmentTypeName { get; set; }
        public string sNormalizedShipmentTypeName { get; set; }

        public DateTime dtLastUpdate { get; set; }
        public int nWizardID { get; set; }

    }
}
