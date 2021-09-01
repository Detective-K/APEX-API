using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class DpbFile
    {
        public string Dpb01 { get; set; }
        public string Dpb02 { get; set; }
        public string Dpb03 { get; set; }
        public string Dpbacti { get; set; }
        public string Dpbuser { get; set; }
        public string Dpbgrup { get; set; }
        public string Dpbmodu { get; set; }
        public DateTime? Dpbdate { get; set; }
        public string Dpborig { get; set; }
        public string Dpboriu { get; set; }
    }
}
