using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class FieldCustomerMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FieldCustomerMapId { get; set; }
        public int FieldId { get; set; }
        public int CustomerId { get; set; }
        public bool IsEnable { get; set; }
    }
}
