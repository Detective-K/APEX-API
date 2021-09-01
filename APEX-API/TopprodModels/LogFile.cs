using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class LogFile
    {
        public string Tab { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public string Key4 { get; set; }
        public string Key5 { get; set; }
        public string Col { get; set; }
        public DateTime? Ldate { get; set; }
        public string Luser { get; set; }
        public string Ltype { get; set; }
        public string OldVal { get; set; }
        public string NewVal { get; set; }
    }
}
