using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class BgaFile
    {
        public string Bga01 { get; set; }
        public short Bga02 { get; set; }
        public short Bga03 { get; set; }
        public string Bga04 { get; set; }
        public decimal? Bga05 { get; set; }
        public string Bgaacti { get; set; }
        public string Bgauser { get; set; }
        public string Bgagrup { get; set; }
        public string Bgamodu { get; set; }
        public DateTime? Bgadate { get; set; }
        public string Bgaorig { get; set; }
        public string Bgaoriu { get; set; }
    }
}
