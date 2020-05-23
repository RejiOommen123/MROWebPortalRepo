using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class Wizard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WizardId { get; set; }
        public string WizardName { get; set; }
    }
}
