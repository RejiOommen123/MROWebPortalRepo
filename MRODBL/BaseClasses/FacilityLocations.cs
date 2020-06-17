using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class FacilityLocations : CommonModel
    {
        //[Key]
        public int nFacilityLocationID { get; set; }
        [Key]
        public int nFacilityID { get; set; }
        [Key]
        public int nROILocationID { get; set; }
        public string? sLocationCode { get; set; }
        public string sLocationName { get; set; }
        public string sLocationAddress { get; set; }
        public string sPhoneNo { get; set; }
        public string sFaxNo { get; set; }
        public string sConfigLogoName { get; set; }
        public string sConfigLogoData { get; set; }
        public string sConfigBackgroundName { get; set; }
        public string sConfigBackgroundData { get; set; }
        public string sAuthTemplate { get; set; }
        public string sAuthTemplateName { get; set; }
        public bool bLocationActiveStatus { get; set; }
    }
}
