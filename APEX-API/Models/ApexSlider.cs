using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ApexSlider
    {
        public int Id { get; set; }
        public string ContentTitle { get; set; }
        public string ContentMain { get; set; }
        public string ContentDetail { get; set; }
        public string ContentMore { get; set; }
        public string BannerId { get; set; }
        public int? SliderOrder { get; set; }
        public string WebSitePage { get; set; }
        public string ImageName { get; set; }
        public string SliderLanguage { get; set; }
    }
}
