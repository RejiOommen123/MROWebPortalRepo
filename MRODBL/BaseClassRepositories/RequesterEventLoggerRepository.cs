using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class RequesterEventLoggerRepository : Repository<RequesterEventLogger>
    {
        public RequesterEventLoggerRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblRequesterEventLogger", "nRELID");
        }
    }
}
