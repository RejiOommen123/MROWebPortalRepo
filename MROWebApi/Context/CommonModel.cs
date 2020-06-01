 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Context
{
    public class CommonModel
    {
        public int sCreatedBy { get; set; }
        public DateTime dtCreatedDate { get; set; }
        public int sUpdatedBy { get; set; }
        public DateTime dtUpdatedDate { get; set; }
    }
}
