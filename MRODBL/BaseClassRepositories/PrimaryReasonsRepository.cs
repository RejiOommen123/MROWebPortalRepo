using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class PrimaryReasonsRepository: Repository<PrimaryReasons>
    {
        public PrimaryReasonsRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstPrimaryReasons", "nPrimaryReasonID");
        }
    }
}
