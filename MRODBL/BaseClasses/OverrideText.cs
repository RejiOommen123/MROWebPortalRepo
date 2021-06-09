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
        [ExplicitKey]
        [DisplayName("Place")]
        public string sPlace { get; set; }
        [ExplicitKey]
        [DisplayName("Sub ID")]
        public int nSubID { get; set; }
        [ExplicitKey]
        [DisplayName("Text Type")]
        public string sTextType { get; set; }
        [ExplicitKey]
        [DisplayName("Wizard ID")]
        public int nWizardID { get; set; }
        [DisplayName("Table Name")]
        public string sTableName { get; set; }
        [ExplicitKey]
        [DisplayName("Control ID")]
        public int nControlID { get; set; }
        [ExplicitKey]
        [DisplayName("Language ID")]
        public int nLanguageID { get; set; }
        [DisplayName("Text")]
        public string sText { get; set; }
        [DisplayName("Common Control ID")]
        public int? nCommonControlID { get; set; }
        #endregion
    }
}
