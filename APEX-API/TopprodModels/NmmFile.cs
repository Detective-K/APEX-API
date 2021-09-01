using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class NmmFile
    {
        public short Nmm01 { get; set; }
        public short Nmm02 { get; set; }
        public decimal? Nmm03 { get; set; }
        public decimal? Nmm04 { get; set; }
        public string Nmmacti { get; set; }
        public string Nmmuser { get; set; }
        public string Nmmgrup { get; set; }
        public string Nmmmodu { get; set; }
        public DateTime? Nmmdate { get; set; }
        public string Nmmlegal { get; set; }
        public string Nmmoriu { get; set; }
        public string Nmmorig { get; set; }
    }
}
