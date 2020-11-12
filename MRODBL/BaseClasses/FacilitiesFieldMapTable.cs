using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MRODBL.BaseClasses
{
    public class FacilitiesFieldMapTable :CommonModel
    {
        #region Props
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nFacilityFieldMapID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Facility Id")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Field Id")]
        public int nFieldID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Wizard Id")]
        public int nWizardID { get; set; }
        [Required]
        [DisplayName("Active Status")]
        public bool bShow { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [DisplayName("Order")]
        public int? nFieldOrder { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Table Name Allowed")]
        [DisplayName("Table Name")]
        public string sTableName { get; set; }
        [MaxLength]
        [DisplayName("Field Name")]
        public string sFieldName { get; set; }
        #endregion
    }
}
