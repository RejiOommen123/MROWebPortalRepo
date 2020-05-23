using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MROWebAPI.Models
{
    public class Patient
    {
        public string SelectedLocation { get; set; }
        public bool NotPatient { get; set; }
        public string RName { get; set; }
        public string RelationToPatient { get; set; }
        public string EmailID { get; set; }
        public bool ConfirmReport { get; set; }
        public string FName { get; set; }
        public string MInitial { get; set; }
        public string LName { get; set; }
        public bool IsPatientDeceased { get; set; }
        public string PostalCode { get; set; }
        public string StreetArea { get; set; }
        public string BDay { get; set; }
        public string imgData { get; set; }
    }
}
