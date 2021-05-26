using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Table("lnkOverrideText")]
    public partial class OverrideText : CommonModel
    {
        #region Props
        public string sPlace { get; set; }
        public int nSubID { get; set; }
        public string sTextType { get; set; }
        public int nWizardID { get; set; }
        public string sTableName { get; set; }
        public int nControlID { get; set; }
        public int nLanguageID { get; set; }
        public string sText { get; set; }
        public int? nCommonControlID { get; set; }
        #endregion
    }
}
