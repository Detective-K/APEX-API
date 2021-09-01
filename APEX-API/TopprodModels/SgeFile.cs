using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class SgeFile
    {
        public DateTime Sge01 { get; set; }
        public string Sge02 { get; set; }
        public decimal? Sge03 { get; set; }
        public string Sgeacti { get; set; }
        public string Sgeuser { get; set; }
        public string Sgegrup { get; set; }
        public string Sgemodu { get; set; }
        public DateTime? Sgedate { get; set; }
        public string Sgeorig { get; set; }
        public string Sgeoriu { get; set; }
    }
}
