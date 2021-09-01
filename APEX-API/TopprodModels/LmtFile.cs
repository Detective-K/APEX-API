using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class LmtFile
    {
        public string Lmt01 { get; set; }
        public string Lmt02 { get; set; }
        public string Lmt03 { get; set; }
        public string Lmtacti { get; set; }
        public DateTime? Lmtcrat { get; set; }
        public DateTime? Lmtdate { get; set; }
        public string Lmtgrup { get; set; }
        public string Lmtmodu { get; set; }
        public string Lmtuser { get; set; }
        public string Lmtorig { get; set; }
        public string Lmtoriu { get; set; }
    }
}
