using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class RqrFile
    {
        public string RqrV { get; set; }
        public DateTime? Rqr01 { get; set; }
        public string Rqr02 { get; set; }
        public string Rqr03 { get; set; }
        public short? Rqr04 { get; set; }
        public string Rqr05 { get; set; }
        public string InclWo { get; set; }
        public string InclMps { get; set; }
        public string InclPlm { get; set; }
    }
}
