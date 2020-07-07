using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class FacilityFieldMapsRepository : Repository<FacilityFieldMaps>
    {
        public FacilityFieldMapsRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkFacilityFieldMaps", "nFacilityFieldMapID");
        }
    }
}
