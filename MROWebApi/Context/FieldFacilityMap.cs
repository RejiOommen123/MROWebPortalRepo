using MROWebApi.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class FieldFacilityMap : CommonModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FieldFacilityMapId { get; set; }
        public int FieldId { get; set; }
        public int WizardId { get; set; }
        public int FacilityId { get; set; }
        public bool IsEnable { get; set; }
    }
}
