using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexF
    {
        public int Id { get; set; }
        public string ComplaintsNum { get; set; }
        public int Item { get; set; }
        public string Filename { get; set; }
    }
}
