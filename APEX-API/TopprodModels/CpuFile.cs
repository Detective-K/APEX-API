using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class CpuFile
    {
        public string Cpu01 { get; set; }
        public string Cpuacti { get; set; }
        public string Cpuuser { get; set; }
        public string Cpugrup { get; set; }
        public string Cpumodu { get; set; }
        public DateTime? Cpudate { get; set; }
        public string Cpuorig { get; set; }
        public string Cpuoriu { get; set; }
    }
}
