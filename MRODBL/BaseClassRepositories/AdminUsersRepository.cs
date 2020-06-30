using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class AdminUsersRepository : Repository<AdminUsers>
    {
        public AdminUsersRepository(DBConnectionInfo DBInfo)
        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblAdminUsers", "nAdminUserID");
        }
    }
}
