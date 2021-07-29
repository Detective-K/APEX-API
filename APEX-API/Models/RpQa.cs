using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class RpQa
    {
        public int RpId { get; set; }
        public string RpLanguage { get; set; }
        public string RpQ { get; set; }
        public string RpA { get; set; }
    }
}
