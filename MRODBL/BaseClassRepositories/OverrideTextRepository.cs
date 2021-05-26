using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class OverrideTextRepository : Repository<OverrideText>
    {
        public OverrideTextRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkOverrideText", "sPlace");
        }
    }
}
