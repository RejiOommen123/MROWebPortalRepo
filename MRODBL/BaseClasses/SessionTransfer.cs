using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class SessionTransfer
    {
        #region Props
        public string CompleteState { get; set; }
        public int nRequesterId { get; set; }
        public int nFacilityId { get; set; }
        public int nLocationId { get; set; }
        #endregion
    }
}
