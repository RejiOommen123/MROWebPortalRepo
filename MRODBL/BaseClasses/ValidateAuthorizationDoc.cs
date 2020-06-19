using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class ValidateAuthorizationDoc
    {
        [Key]
        public int nFieldID { get; set; }
        public string sKeyword { get; set; }
        public string sFieldname { get; set; }
    }
}
