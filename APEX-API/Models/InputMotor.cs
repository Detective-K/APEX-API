using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class InputMotor
    {
        public string MotorId { get; set; }
        public string OrderId { get; set; }
        public string OrderDetailId { get; set; }
        public string CustId { get; set; }
        public string La { get; set; }
        public string Lb { get; set; }
        public string Lc { get; set; }
        public string Le { get; set; }
        public string Lr { get; set; }
        public string Lz { get; set; }
        public string S { get; set; }
        public string Lt { get; set; }
        public string Lg { get; set; }
        public string PartNo { get; set; }
    }
}
