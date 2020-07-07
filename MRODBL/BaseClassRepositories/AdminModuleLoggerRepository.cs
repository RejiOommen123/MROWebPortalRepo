using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class AdminModuleLoggerRepository:Repository<AdminModuleLogger>
    {
        public AdminModuleLoggerRepository(DBConnectionInfo DBInfo)
        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblAdminModuleLogger", "nAMLID");
        }
    }
}
