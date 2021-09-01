using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class MmiFile
    {
        public string Mmi01 { get; set; }
        public string Mmi02 { get; set; }
        public string Mmi03 { get; set; }
        public string Mmiacti { get; set; }
        public string Mmiuser { get; set; }
        public string Mmigrup { get; set; }
        public string Mmimodu { get; set; }
        public DateTime? Mmidate { get; set; }
        public string Mmiorig { get; set; }
        public string Mmioriu { get; set; }
    }
}
