using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class AddFacility
    {
        public Facilities cFacility { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 charcters")]
        public string sConnectionString { get; set; }
    }
}
