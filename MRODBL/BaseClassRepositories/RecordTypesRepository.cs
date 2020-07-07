using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class RecordTypesRepository:Repository<RecordTypes>
    {
        public RecordTypesRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstRecordTypes", "nRecordTypeID");
        }
    }
}

