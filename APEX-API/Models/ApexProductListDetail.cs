using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ApexProductListDetail
    {
        public int Id { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductKind { get; set; }
        public string Item { get; set; }
        public string StartPage { get; set; }
        public string EndPage { get; set; }
        public string PdfName { get; set; }
        public string Language { get; set; }
    }
}
