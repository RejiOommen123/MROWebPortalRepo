using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class ValidateAuthorizationDoc
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFieldID { get; set; }
        [MaxLength]
        public string sKeyword { get; set; }
        [MaxLength]
        public string sFieldname { get; set; }
        #endregion
    }
}
