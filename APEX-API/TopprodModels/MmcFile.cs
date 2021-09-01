using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class MmcFile
    {
        public string Mmc01 { get; set; }
        public string Mmc02 { get; set; }
        public string Mmcacti { get; set; }
        public string Mmcuser { get; set; }
        public string Mmcgrup { get; set; }
        public string Mmcmodu { get; set; }
        public DateTime? Mmcdate { get; set; }
        public string Mmcorig { get; set; }
        public string Mmcoriu { get; set; }
    }
}
