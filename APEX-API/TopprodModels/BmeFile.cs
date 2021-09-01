using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class BmeFile
    {
        public string Bme01 { get; set; }
        public string Bme02 { get; set; }
        public string Bme03 { get; set; }
        public string Bme04 { get; set; }
        public string Bme05 { get; set; }
        public string Bmeuser { get; set; }
        public string Bmegrup { get; set; }
        public string Bmemodu { get; set; }
        public DateTime? Bmedate { get; set; }
        public string Bmeacti { get; set; }
        public string Bmeorig { get; set; }
        public string Bmeoriu { get; set; }
    }
}
