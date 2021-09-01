using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class IcdFile
    {
        public string Icd01 { get; set; }
        public string Icd02 { get; set; }
        public string Icd03 { get; set; }
        public string Icd04 { get; set; }
        public string Icd05 { get; set; }
        public short? Icd06 { get; set; }
        public string Icdacti { get; set; }
        public DateTime? Icddate { get; set; }
        public string Icdgrup { get; set; }
        public string Icdmodu { get; set; }
        public string Icduser { get; set; }
        public string Icdorig { get; set; }
        public string Icdoriu { get; set; }
    }
}
