using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class OrderBom2
    {
        public string OrderId { get; set; }
        public int? OrderSub { get; set; }
        public string PartNo { get; set; }
        public string MotoName { get; set; }
        public string Spec { get; set; }
        public string Detail { get; set; }
        public string Qty { get; set; }
        public int Autoid { get; set; }
        public int? OrderDetailId { get; set; }
    }
}
