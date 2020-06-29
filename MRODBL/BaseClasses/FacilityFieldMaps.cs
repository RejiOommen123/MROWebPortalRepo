using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    [Table("lnkFacilityFieldMaps")]
    public partial class FacilityFieldMaps : CommonModel
    {
        [Key]
        public int nFacilityFieldMapID { get; set; }
        public int nFacilityID { get; set; }
        public int nFieldID { get; set; }
        public int nWizardID { get; set; }
        public bool bShow { get; set; }
        public string sTableName { get; set; }
    }
}
