using MROWebApi.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{

    public class Facility : CommonModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string Description { get; set; }
        public string NumOfInstitution { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPUrl { get; set; }
        public string FTPUsername { get; set; }
        public string FTPPassword { get; set; }
        public string FTPUrl { get; set; }
        public string ConfigFileUrl { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
