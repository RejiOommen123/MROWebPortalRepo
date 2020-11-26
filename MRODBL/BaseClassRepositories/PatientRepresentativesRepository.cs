using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;


namespace MRODBL.BaseClassRepositories
{
    public class PatientRepresentativesRepository : Repository<PatientRepresentatives>
    {
        public PatientRepresentativesRepository(DBConnectionInfo DBInfo)

        {
            Init(DBInfo.ConnectionString);
        }

        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstPatientRepresentatives", "nPatientRepresentativeID");
        }
    }
}
