using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class TrcFile
    {
        public string Trc01 { get; set; }
        public string Trc02 { get; set; }
        public string Trc03 { get; set; }
        public string Trcacti { get; set; }
        public string Trcuser { get; set; }
        public string Trcgrup { get; set; }
        public string Trcmodu { get; set; }
        public DateTime? Trcdate { get; set; }
        public string Trcorig { get; set; }
        public string Trcoriu { get; set; }
    }
}
