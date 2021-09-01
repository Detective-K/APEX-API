using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CzcFile
    {
        public decimal Czc01 { get; set; }
        public decimal? Czc02 { get; set; }
        public decimal? Czc03 { get; set; }
        public decimal? Czc04 { get; set; }
        public string Czcuser { get; set; }
        public string Czcgrup { get; set; }
        public string Czcmodu { get; set; }
        public DateTime? Czcdate { get; set; }
        public string Czcorig { get; set; }
        public string Czcoriu { get; set; }
    }
}
