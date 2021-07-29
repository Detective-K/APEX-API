using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ApexVideoList
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ViedoHead { get; set; }
        public string VideoLength { get; set; }
        public int VideoOrder { get; set; }
    }
}
