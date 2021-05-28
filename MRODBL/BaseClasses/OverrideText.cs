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
        [DisplayName("Place")]
        public string sPlace { get; set; }
        [DisplayName("Sub ID")]
        public int nSubID { get; set; }
        [DisplayName("Text Type")]
        public string sTextType { get; set; }
        [DisplayName("Wizard ID")]
        public int nWizardID { get; set; }
        [DisplayName("Table Name")]
        public string sTableName { get; set; }
        [DisplayName("Control ID")]
        public int nControlID { get; set; }
        [DisplayName("Language ID")]
        public int nLanguageID { get; set; }
        [DisplayName("Text")]
        public string sText { get; set; }
        [DisplayName("Common Control ID")]
        public int? nCommonControlID { get; set; }
        #endregion
    }
}
