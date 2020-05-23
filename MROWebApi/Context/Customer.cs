using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
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
