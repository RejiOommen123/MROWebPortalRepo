namespace MRODBL.BaseClasses
{
    public partial class SupportDocs
    {
        #region Props        
        public int nRequesterID { get; set; }
        public int nFacilityID { get; set; }
        public string[] sRelativeFileArray { get; set; }        
        public string[] sRelativeFileNameArray { get; set; }
        public string sWizardName { get; set; }
        #endregion
    }
}
