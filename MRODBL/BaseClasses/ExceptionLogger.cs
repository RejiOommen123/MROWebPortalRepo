using System;

namespace MRODBL.BaseClasses
{
    public class ExceptionLogger
    {
        #region Props
        [Dapper.Contrib.Extensions.Key]
        public int nExceptionID { get; set; }
        public int? nRequesterID { get; set; }
        public string  sStatusName { get; set; }
        public string sModuleName { get; set; }
        public string sDescription { get; set; }
        public DateTime dtExceptionTime { get; set; }
        #endregion
    }
}
