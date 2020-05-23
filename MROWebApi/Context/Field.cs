using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class Field
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FieldId { get; set; }
        public int WizardId { get; set; }
        public string FieldName { get; set; }
        public string NormalizedFieldName { get; set; }
    }
}
