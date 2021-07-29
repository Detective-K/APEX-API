using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class Country
    {
        public string CountryCode { get; set; }
        public string Country1 { get; set; }
        public string Currency { get; set; }
        public float? Rate { get; set; }
    }
}
