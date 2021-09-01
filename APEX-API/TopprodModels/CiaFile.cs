using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CiaFile
    {
        public short Cia01 { get; set; }
        public decimal? Cia02 { get; set; }
        public decimal? Cia03 { get; set; }
        public decimal? Cia04 { get; set; }
        public string Ciaacti { get; set; }
        public string Ciauser { get; set; }
        public string Ciagrup { get; set; }
        public string Ciamodu { get; set; }
        public DateTime? Ciadate { get; set; }
        public string Ciaorig { get; set; }
        public string Ciaoriu { get; set; }
    }
}
