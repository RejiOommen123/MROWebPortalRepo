using System;
using System.Collections.Generic;
using System.Text;

namespace MRODBL.BaseClasses
{
    public class PDFAndXMLData
    {
        public List<RecordTypes> recordTypes { get; set; }
        public List<SensitiveInfo> sensitiveInfos { get; set; }
        public List<Fields> fields { get; set; }
        public List<Waiver> waiver { get; set; }
    }
}
