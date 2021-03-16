using System;
using System.ComponentModel.DataAnnotations;


namespace MRODBL.BaseClasses
{
    public class EmailConfirmation
    {
        #region Props
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int nFacilityID { get; set; }
        public int nLocationID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum 30 characters Email ID allowed")]
        public string sRequesterEmailId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Maximum 20 characters Patient First Name allowed")]
        public string sPatientFirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters Patient Last Name allowed")]
        public string sPatientLastName { get; set; }
        #endregion
        public bool bAreYouPatient { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Relative Name allowed")]
        public string sRelativeFirstName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Relative Name allowed")]
        public string sRelativeLastName { get; set; }
    }
}
