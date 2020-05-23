using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class WizardCustomerMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WizardCustomerMapId { get; set; }
        public int WizardId { get; set; }
        public int CustomerId { get; set; }
        public bool IsEnable { get; set; }
    }
}
