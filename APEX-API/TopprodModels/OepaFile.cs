using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class OepaFile
    {
        public string Oepa01 { get; set; }
        public short Oepa011 { get; set; }
        public string Oepa02 { get; set; }
        public short Oepa03 { get; set; }
        public string Oepa04a { get; set; }
        public string Oepa04b { get; set; }
        public DateTime? Oepa05a { get; set; }
        public DateTime? Oepa05b { get; set; }
        public DateTime? Oepa06a { get; set; }
        public DateTime? Oepa06b { get; set; }
        public decimal? Oepa07a { get; set; }
        public decimal? Oepa07b { get; set; }
        public decimal? Oepa08a { get; set; }
        public decimal? Oepa08b { get; set; }
        public string Oepalegal { get; set; }
        public string Oepaplant { get; set; }
    }
}
