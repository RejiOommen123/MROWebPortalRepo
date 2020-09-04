using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
    public class FacilityDisclaimersRepository :Repository<FacilityDisclaimers>
    {
        public FacilityDisclaimersRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkFacilityWizardHelper", "nFacilityWizardHelperID");
        }
    }
}
