using System;
using System.ComponentModel.DataAnnotations;


namespace MRODBL.BaseClasses
{
    public class EmailInputObject
    {
        #region Props
        public int nFacilityID { get; set; }
        public int nLocationID { get; set; }
        public string sEmailId { get; set; }
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        #endregion
    }
}
