using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexHSn
    {
        public int ComplaintsApexHSnId { get; set; }
        public string ComplaintsNum { get; set; }
        public string SnNum { get; set; }
        public string ModeNo { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string BackFlag { get; set; }
    }
}
