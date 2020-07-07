using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class FacilityLocationsRepository :Repository<FacilityLocations>
    {
        public FacilityLocationsRepository(DBConnectionInfo DBInfo) {
            Init(DBInfo.ConnectionString);
        }
        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblFacilityLocations", "nFacilityLocationID");
        }
    }
}
