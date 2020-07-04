using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class RequesterModuleLoggerRepository:Repository<RequesterModuleLogger>
    {
        public RequesterModuleLoggerRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblRequesterModuleLogger", "nRMLID");
        }
    }
}
