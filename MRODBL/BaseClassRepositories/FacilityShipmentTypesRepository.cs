using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClassRepositories
{
    public class FacilityShipmentTypesRepository :Repository<FacilityShipmentTypes>
    {
        public FacilityShipmentTypesRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkFacilityShipmentTypes", "nFacilityShipmentTypeID");
        }
    }
}
