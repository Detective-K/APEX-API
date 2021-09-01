using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class TrbFile
    {
        public string Trb01 { get; set; }
        public string Trb02 { get; set; }
        public string Trb03 { get; set; }
        public string Trbacti { get; set; }
        public string Trbuser { get; set; }
        public string Trbgrup { get; set; }
        public string Trbmodu { get; set; }
        public DateTime? Trbdate { get; set; }
        public string Trborig { get; set; }
        public string Trboriu { get; set; }
    }
}
