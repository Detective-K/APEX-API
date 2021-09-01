using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class NqcFile
    {
        public string Nqc01 { get; set; }
        public string Nqc02 { get; set; }
        public short Nqc03 { get; set; }
        public short Nqc04 { get; set; }
        public decimal? Nqc05 { get; set; }
        public string Nqcacti { get; set; }
        public string Nqcuser { get; set; }
        public string Nqcgrup { get; set; }
        public string Nqcmodu { get; set; }
        public DateTime? Nqcdate { get; set; }
        public string Nqcorig { get; set; }
        public string Nqcoriu { get; set; }
    }
}
