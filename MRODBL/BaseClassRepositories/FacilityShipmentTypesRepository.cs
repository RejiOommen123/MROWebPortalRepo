using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


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
