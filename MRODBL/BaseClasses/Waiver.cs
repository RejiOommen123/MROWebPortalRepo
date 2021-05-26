﻿using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    [Table("lstWaiver")]
    public class Waiver : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("Id")]
        public int nWaiverID { get; set; }        
        [StringLength(500, ErrorMessage = "Maximum 500 characters Waiver allowed")]
        [DisplayName("Name")]
        [Write(false)]
        public string sWaiverName { get; set; }
        [DisplayName("Normalized Name")]
        public string sNormalizedWaiverName { get; set; }    
        [StringLength(500, ErrorMessage = "Maximum 500 characters Waiver Tool Tip allowed")]
        [DisplayName("Tooltip")]
        [Write(false)]
        public string sFieldToolTip { get; set; }        
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        [DisplayName("eXpress Id")]
        public int nWizardID { get; set; }
        #endregion
    }
}
