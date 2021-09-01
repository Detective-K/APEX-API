using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CpcFile
    {
        public short Cpc01 { get; set; }
        public decimal? Cpc02 { get; set; }
        public decimal? Cpc03 { get; set; }
        public decimal? Cpc04 { get; set; }
        public string Cpcuser { get; set; }
        public string Cpcgrup { get; set; }
        public string Cpcmodu { get; set; }
        public DateTime? Cpcdate { get; set; }
        public string Cpcorig { get; set; }
        public string Cpcoriu { get; set; }
    }
}
