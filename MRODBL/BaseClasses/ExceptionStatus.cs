using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MRODBL.BaseClasses
{
   

    public enum ExceptionStatus 
    {

        [EnumMember(Value = "Error")] Error,
        [EnumMember(Value = "Information")] Info,
        [EnumMember(Value = "Warning")] Warning
        
    }

}
