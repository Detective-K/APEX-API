using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class FgbFile
    {
        public string Fgb01 { get; set; }
        public short Fgb02 { get; set; }
        public string Fgb03 { get; set; }
        public string Fgb04 { get; set; }
        public string Fgbacti { get; set; }
        public string Fgbuser { get; set; }
        public string Fgbgrup { get; set; }
        public string Fgbmodu { get; set; }
        public DateTime? Fgbdate { get; set; }
        public string Fgborig { get; set; }
        public string Fgboriu { get; set; }
    }
}
