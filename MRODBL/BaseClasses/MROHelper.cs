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
        #endregion
    }
}
