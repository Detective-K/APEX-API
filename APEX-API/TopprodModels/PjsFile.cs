using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class PjsFile
    {
        public short Pjs01 { get; set; }
        public short Pjs02 { get; set; }
        public string Pjs03 { get; set; }
        public string Pjs04 { get; set; }
        public string Pjs05 { get; set; }
        public decimal? Pjs10 { get; set; }
        public decimal? Pjs11 { get; set; }
        public decimal? Pjs12 { get; set; }
        public DateTime? Pjsdate { get; set; }
        public string Pjstime { get; set; }
        public string Pjsuser { get; set; }
        public string Pjsorig { get; set; }
        public string Pjsoriu { get; set; }
    }
}
