using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class NppFile
    {
        public string Nppsys { get; set; }
        public short Npp00 { get; set; }
        public string Npp01 { get; set; }
        public short Npp011 { get; set; }
        public DateTime? Npp02 { get; set; }
        public DateTime? Npp03 { get; set; }
        public short? Npp04 { get; set; }
        public string Npp05 { get; set; }
        public string Npp06 { get; set; }
        public string Npp07 { get; set; }
        public string Nppglno { get; set; }
        public string Npptype { get; set; }
        public string Npplegal { get; set; }
        public string Npp08 { get; set; }
    }
}
