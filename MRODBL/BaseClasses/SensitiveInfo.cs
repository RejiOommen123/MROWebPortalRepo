﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MRODBL.BaseClasses
{
    public partial class SensitiveInfo : CommonModel
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nSensitiveInfoID { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 100 characters Sensitive Info allowed")]
        public string sSensitiveInfoName { get; set; }
        public string sNormalizedSensitiveInfoName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters Sensitive Info Tool Tip allowed")]
        public string sFieldToolTip { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Zero & Only positive number allowed")]
        public int nWizardID { get; set; }
        #endregion
    }
}
