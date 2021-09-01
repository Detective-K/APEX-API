using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class MslFile
    {
        public string MslV { get; set; }
        public DateTime? Msl01 { get; set; }
        public string Msl02 { get; set; }
        public string Msl03 { get; set; }
        public string Msl04 { get; set; }
        public string Mslplant { get; set; }
        public string Msllegal { get; set; }
    }
}
