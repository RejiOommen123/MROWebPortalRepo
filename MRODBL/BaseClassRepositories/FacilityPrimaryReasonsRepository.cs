using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class FacilityPrimaryReasonsRepository :Repository<FacilityPrimaryReasons>
    {
        public FacilityPrimaryReasonsRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkFacilityPrimaryReasons", "nFacilityPrimaryReasonID");
        }
    }
}
