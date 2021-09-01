using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class BgtFile
    {
        public int Bgt01 { get; set; }
        public int Bgt02 { get; set; }
        public decimal? Bgt03 { get; set; }
        public decimal? Bgt04 { get; set; }
        public string Bgtacti { get; set; }
        public string Bgtuser { get; set; }
        public string Bgtgrup { get; set; }
        public string Bgtmodu { get; set; }
        public DateTime? Bgtdate { get; set; }
        public string Bgtorig { get; set; }
        public string Bgtoriu { get; set; }
    }
}
