using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class EmailConfirmation
    {
        #region Props
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum 30 characters Email ID allowed")]
        public string sPatientEmailId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Maximum 20 characters Patient First Name allowed")]
        public string sPatientFirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient Last Name allowed")]
        public string sPatientLastName { get; set; }
        #endregion
    }
}
