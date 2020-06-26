using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class MROHelper
    {
        [Key]
        public int nMROHelperID { get; set; }
        public string sWhitebgimg { get; set; }
    }
}
