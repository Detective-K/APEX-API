using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CmfFile
    {
        public short Cmf01 { get; set; }
        public short Cmf02 { get; set; }
        public string Cmf03 { get; set; }
        public string Cmf031 { get; set; }
        public string Cmf04 { get; set; }
        public decimal? Cmf05 { get; set; }
        public string Cmf06 { get; set; }
        public DateTime? Cmfdate { get; set; }
        public string Cmfgrup { get; set; }
        public string Cmfmodu { get; set; }
        public string Cmfuser { get; set; }
        public string Cmforig { get; set; }
        public string Cmforiu { get; set; }
        public string Cmflegal { get; set; }
    }
}
