using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CckFile
    {
        public short Cck01 { get; set; }
        public short Cck02 { get; set; }
        public string CckW { get; set; }
        public string CckL { get; set; }
        public decimal Cck03 { get; set; }
        public decimal Cck04 { get; set; }
        public decimal Cck05 { get; set; }
        public decimal Cck06 { get; set; }
        public decimal Cck07 { get; set; }
        public string Cckacti { get; set; }
        public string Cckuser { get; set; }
        public string Cckgrup { get; set; }
        public string Cckmodu { get; set; }
        public DateTime? Cckdate { get; set; }
        public string Cckoriu { get; set; }
        public string Cckorig { get; set; }
        public string Ccklegal { get; set; }
    }
}
