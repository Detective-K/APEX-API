using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class PmrFile
    {
        public string Pmr01 { get; set; }
        public short Pmr02 { get; set; }
        public decimal Pmr03 { get; set; }
        public decimal Pmr04 { get; set; }
        public decimal? Pmr05 { get; set; }
        public decimal? Pmr05t { get; set; }
        public string Pmrplant { get; set; }
        public string Pmrlegal { get; set; }
    }
}
