using System;
using System.Collections.Generic;
using System.Text;
using MRODBL.BaseClasses;
using MRODBL.Entities;
using MRODBL.Repositories;

namespace MRODBL.BaseClassRepositories
{
   public class WizardRepository:Repository<Wizards>
    {
        public WizardRepository(DBConnectionInfo DBInfo)
        {
            Init(DBInfo.ConnectionString);
        }
        public void Init(string sConnectIn)
        {
            Init(sConnectIn, "lstWizards", "nWizardID");
        }
    }
}
