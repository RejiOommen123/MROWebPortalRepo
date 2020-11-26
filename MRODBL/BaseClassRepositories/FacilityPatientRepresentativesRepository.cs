using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class FacilityPatientRepresentativesRepository : Repository<FacilityPatientRepresentatives>
    {
        public FacilityPatientRepresentativesRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lnkFacilityPatientRepresentatives", "nFacilityPatientRepresentativeID");
        }
    }
}
