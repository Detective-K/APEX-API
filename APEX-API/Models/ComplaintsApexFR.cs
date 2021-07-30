using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexFR
    {
        public string ComplaintsNum { get; set; }
        public int Item { get; set; }
        public string Filename { get; set; }
        public string SalesId { get; set; }
        public string InternalFlag { get; set; }
    }
}
