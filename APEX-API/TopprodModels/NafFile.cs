using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class NafFile
    {
        public string Naf01 { get; set; }
        public string Naf02 { get; set; }
        public short Naf03 { get; set; }
        public short Naf04 { get; set; }
        public string Nafconf { get; set; }
        public DateTime? Nafdate { get; set; }
        public string Nafgrup { get; set; }
        public string Naflegal { get; set; }
        public string Nafmodu { get; set; }
        public string Nafuser { get; set; }
    }
}
