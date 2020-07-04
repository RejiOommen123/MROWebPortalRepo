using System;
using System.ComponentModel.DataAnnotations;


namespace MRODBL.BaseClasses
{
    public class FacilitiesFieldMapTable :CommonModel
    {
        #region Props
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityFieldMapID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFacilityID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nFieldID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        [Required]
        public bool bShow { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? nFieldOrder { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters Table Name Allowed")]
        public string sTableName { get; set; }
        [MaxLength]
        public string sFieldName { get; set; }
        #endregion
    }
}
