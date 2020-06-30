using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public class AddFacility
    {
        #region Props
        public Facilities cFacility { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Maximum 1000 charcters Connection String Allowed")]
        public string sConnectionString { get; set; }
        #endregion
    }
}
