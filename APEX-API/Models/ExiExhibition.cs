using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ExiExhibition
    {
        public int ExiId { get; set; }
        public string ExiTitle { get; set; }
        public string ExiShowTitle { get; set; }
        public string ExiShowLocation { get; set; }
        public string ExiShowDate { get; set; }
        public string ExiShowBooth { get; set; }
        public string RpLanguage { get; set; }
        public DateTime? BeginDate { get; set; }
    }
}
