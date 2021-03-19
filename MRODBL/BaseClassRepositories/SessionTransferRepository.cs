using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class SessionTransferRepository : Repository<SessionTransfer>
    {
        public SessionTransferRepository(DBConnectionInfo DBInfo)
        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblSessionTransfer", "nSessionTransferID");
        }
    }
}
