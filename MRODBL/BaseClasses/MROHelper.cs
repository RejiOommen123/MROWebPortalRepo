using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class MROHelper
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        public int nMROHelperID { get; set; }
        [MaxLength]
        public string sWhitebgimg { get; set; }
        [MaxLength]
        public string sDefaultLogoData { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Default Logo Name Allowed")]
        public string sDefaultLogoName { get; set; }
        [MaxLength]
        public string sDefaultBGData { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Default BG Name Allowed")]
        public string sDefaultBGName { get; set; }
        [MaxLength]
        public string sMROEmailFooterImage { get; set; }
        #endregion
    }
}
