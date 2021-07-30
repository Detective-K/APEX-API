using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ExiNews
    {
        public int ExiNewsId { get; set; }
        public string ExiNewsTitle { get; set; }
        public string ExiNewsContent { get; set; }
        public string ExiNewsPicContent { get; set; }
        public string ExiNewsLink { get; set; }
        public string ExiNewsLinkUrl { get; set; }
        public string ExiNewsPicUrl { get; set; }
        public string ExiNewsPic2Url { get; set; }
        public int? ExiNewsOrder { get; set; }
        public string ExiNewsLanguage { get; set; }
    }
}
