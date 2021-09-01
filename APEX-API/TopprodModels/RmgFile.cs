using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class RmgFile
    {
        public DateTime Rmg01 { get; set; }
        public decimal? Rmg02 { get; set; }
        public string Rmguser { get; set; }
        public string Rmggrup { get; set; }
        public string Rmgmodu { get; set; }
        public DateTime? Rmgdate { get; set; }
        public string Rmgplant { get; set; }
        public string Rmglegal { get; set; }
        public string Rmgorig { get; set; }
        public string Rmgoriu { get; set; }
    }
}
