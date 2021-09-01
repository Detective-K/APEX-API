using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class FcfFile
    {
        public string Fcf01 { get; set; }
        public DateTime? Fcf02 { get; set; }
        public string Fcf03 { get; set; }
        public decimal? Fcf04 { get; set; }
        public string Fcfconf { get; set; }
        public short? Fcfprsw { get; set; }
        public string Fcfuser { get; set; }
        public string Fcfgrup { get; set; }
        public string Fcfmodu { get; set; }
        public DateTime? Fcfdate { get; set; }
        public string Fcflegal { get; set; }
        public string Fcforig { get; set; }
        public string Fcforiu { get; set; }
    }
}
