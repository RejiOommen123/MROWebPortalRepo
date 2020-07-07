using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class FacilityConnectionsRepository:Repository<FacilityConnections>
    {
        public FacilityConnectionsRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblFacilityConnections", "nFacilityConnectionID");
        }
    }
}
