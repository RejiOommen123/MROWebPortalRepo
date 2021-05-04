using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class FacilityOrganizationsRepository :Repository<FacilityOrganizations>
    {
        public FacilityOrganizationsRepository(DBConnectionInfo DBInfo) {
            Init(DBInfo.ConnectionString);
        }
        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "tblFacilityOrganizations", "nFacilityOrgID");
        }
    }
}
