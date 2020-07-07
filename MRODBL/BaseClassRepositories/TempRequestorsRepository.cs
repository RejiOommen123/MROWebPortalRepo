using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class TempRequestorsRepository : Repository<TempRequestors>
    {
        public TempRequestorsRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblTempRequesters", "nRequesterID");
        }
    }
}
