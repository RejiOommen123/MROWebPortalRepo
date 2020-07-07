using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class SensitiveInfoRepository : Repository<SensitiveInfo>
    {
        public SensitiveInfoRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstSensitiveInfo", "nSensitiveInfoID");
        }
    }
}
