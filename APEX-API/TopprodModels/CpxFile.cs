using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CpxFile
    {
        public decimal Cpx01 { get; set; }
        public decimal Cpx02 { get; set; }
        public decimal? Cpx03 { get; set; }
        public string Cpxacti { get; set; }
        public string Cpxuser { get; set; }
        public string Cpxgrup { get; set; }
        public string Cpxmodu { get; set; }
        public DateTime? Cpxdate { get; set; }
        public string Cpxorig { get; set; }
        public string Cpxoriu { get; set; }
    }
}
