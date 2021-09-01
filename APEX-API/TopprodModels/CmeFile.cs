using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CmeFile
    {
        public short Cme01 { get; set; }
        public short Cme02 { get; set; }
        public string Cme03 { get; set; }
        public decimal? Cme04 { get; set; }
        public decimal? Cme05 { get; set; }
        public string Cme031 { get; set; }
        public string Cme06 { get; set; }
        public string Cmelegal { get; set; }
    }
}
