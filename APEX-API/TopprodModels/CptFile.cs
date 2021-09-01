using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CptFile
    {
        public DateTime Cpt01 { get; set; }
        public string Cpt02 { get; set; }
        public string Cpt03 { get; set; }
        public string Cptacti { get; set; }
        public string Cptuser { get; set; }
        public string Cptgrup { get; set; }
        public string Cptmodu { get; set; }
        public DateTime? Cptdate { get; set; }
        public string Cptorig { get; set; }
        public string Cptoriu { get; set; }
    }
}
