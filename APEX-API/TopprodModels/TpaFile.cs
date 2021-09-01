using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class TpaFile
    {
        public string Tpa01 { get; set; }
        public short Tpa02 { get; set; }
        public decimal? Tpa03 { get; set; }
        public decimal? Tpa04 { get; set; }
        public string Tpaacti { get; set; }
        public string Tpauser { get; set; }
        public string Tpagrup { get; set; }
        public string Tpamodu { get; set; }
        public DateTime? Tpadate { get; set; }
        public string Tpaorig { get; set; }
        public string Tpaoriu { get; set; }
    }
}
