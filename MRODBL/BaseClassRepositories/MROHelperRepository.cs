using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class MROHelperRepository:Repository<MROHelper>
    {
        public MROHelperRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstMROHelper", "nMROHelperID");
        }
    }
}
