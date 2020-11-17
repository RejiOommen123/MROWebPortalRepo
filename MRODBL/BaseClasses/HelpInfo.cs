using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class HelpInfo
    {
        #region Props
        public Requesters oRequester { get; set; }
        public string sWizardName { get; set; }
        public string sName { get; set; }

       public string sPhoneNo { get; set; }

        public string sEmail { get; set; }

        public string sMessage { get; set; }
        #endregion
    }
}
