using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class SessionTransferObject
    {
        #region Props
        public string CompleteState { get; set; }
        public int nRequesterId { get; set; }
        public int nFacilityId { get; set; }
        public int nLocationId { get; set; }
        public string sEmailId { get; set; }
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        public bool bSendEmail { get; set; }
        #endregion
    }
}