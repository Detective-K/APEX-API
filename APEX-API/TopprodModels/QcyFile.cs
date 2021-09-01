using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class QcyFile
    {
        public string Qcy01 { get; set; }
        public short Qcy02 { get; set; }
        public short Qcy03 { get; set; }
        public string Qcy04 { get; set; }
        public decimal? Qcy05 { get; set; }
        public string Qcyplant { get; set; }
        public string Qcylegal { get; set; }
    }
}
