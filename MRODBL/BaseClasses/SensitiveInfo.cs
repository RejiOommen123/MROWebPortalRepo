﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace MRODBL.BaseClasses
{
    public partial class SensitiveInfo
    {
        [Key]
        public int nSensitiveInfoID { get; set; }
        public string sSensitiveInfoName { get; set; }
        public string sNormalizedSensitiveInfoName { get; set; }

        public DateTime dtLastUpdate { get; set; }
        public int nWizardID { get; set; }

    }
}