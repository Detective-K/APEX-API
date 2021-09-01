using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class ImvFile
    {
        public string Imv01 { get; set; }
        public string Imv02 { get; set; }
        public short? Imv03 { get; set; }
        public string Imvacti { get; set; }
        public string Imvuser { get; set; }
        public string Imvgrup { get; set; }
        public string Imvmodu { get; set; }
        public DateTime? Imvdate { get; set; }
        public string Imvoriu { get; set; }
        public string Imvorig { get; set; }
    }
}
