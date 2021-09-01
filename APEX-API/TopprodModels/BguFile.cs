using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class BguFile
    {
        public DateTime Bgu01 { get; set; }
        public short Bgu02 { get; set; }
        public string Bgu03 { get; set; }
        public string Bgu032 { get; set; }
        public string Bgu033 { get; set; }
        public short? Bgu034 { get; set; }
        public short? Bgu035 { get; set; }
        public decimal? Bgu04 { get; set; }
        public string Bguconf { get; set; }
        public string Bguacti { get; set; }
        public string Bguuser { get; set; }
        public string Bgugrup { get; set; }
        public string Bgumodu { get; set; }
        public DateTime? Bgudate { get; set; }
        public string Bguoriu { get; set; }
        public string Bguorig { get; set; }
    }
}
