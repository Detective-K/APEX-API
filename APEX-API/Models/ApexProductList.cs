using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ApexProductList
    {
        public int Id { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Overview { get; set; }
        public string Size { get; set; }
        public string ImageName { get; set; }
        public string Language { get; set; }
    }
}
