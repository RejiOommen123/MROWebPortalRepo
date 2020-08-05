using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class MROConnectionStringRepository:Repository<MROConnectionString>
    {
        public MROConnectionStringRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstMROConnectionString", "nConnectionStringID");
        }
    }
}
