using System;
using System.Collections.Generic;

namespace MROWebApi.Context
{
    public partial class lstWizards
    {
        public lstWizards()
        {
            lstFields = new HashSet<lstFields>();
        }

        public int nWizardID { get; set; }
        public string sWizardName { get; set; }

        public virtual ICollection<lstFields> lstFields { get; set; }
    }
}
