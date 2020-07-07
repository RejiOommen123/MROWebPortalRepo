using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class ShipmentTypesRepository :Repository<ShipmentTypes>
    {
        public ShipmentTypesRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstShipmentTypes", "nShipmentTypeID");
        }
    }
}
