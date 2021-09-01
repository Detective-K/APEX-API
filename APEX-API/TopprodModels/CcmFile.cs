using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CcmFile
    {
        public string Ccm01 { get; set; }
        public string Ccm02 { get; set; }
        public short? Ccm03 { get; set; }
        public short? Ccm04 { get; set; }
        public short? Ccm05 { get; set; }
        public short? Ccm06 { get; set; }
        public string Ccmuser { get; set; }
        public string Ccmgrup { get; set; }
        public DateTime? Ccmmodd { get; set; }
        public DateTime? Ccmdate { get; set; }
        public string Ccmorig { get; set; }
        public string Ccmoriu { get; set; }
    }
}
