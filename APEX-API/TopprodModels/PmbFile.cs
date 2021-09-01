using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class PmbFile
    {
        public string Pmb01 { get; set; }
        public string Pmb02 { get; set; }
        public short Pmb03 { get; set; }
        public string Pmb04 { get; set; }
        public decimal? Pmb05 { get; set; }
        public string Pmb06 { get; set; }
        public string Pmb07 { get; set; }
        public string Pmbacti { get; set; }
        public string Pmbuser { get; set; }
        public string Pmbgrup { get; set; }
        public string Pmbmodu { get; set; }
        public DateTime? Pmbdate { get; set; }
        public string Pmborig { get; set; }
        public string Pmboriu { get; set; }
    }
}
