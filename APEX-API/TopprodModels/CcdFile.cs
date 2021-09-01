using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CcdFile
    {
        public short Ccd01 { get; set; }
        public string Ccd02 { get; set; }
        public string Ccd03 { get; set; }
        public string Ccd04 { get; set; }
        public string Ccd05 { get; set; }
        public string Ccdacti { get; set; }
        public string Ccduser { get; set; }
        public string Ccdgrup { get; set; }
        public string Ccdmodu { get; set; }
        public DateTime? Ccddate { get; set; }
        public string Ccdorig { get; set; }
        public string Ccdoriu { get; set; }
    }
}
