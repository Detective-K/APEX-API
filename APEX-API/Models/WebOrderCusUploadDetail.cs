using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class WebOrderCusUploadDetail
    {
        public int Id { get; set; }
        public string CusUploadNum { get; set; }
        public string SalesId { get; set; }
        public string ComfirmId { get; set; }
        public int Item { get; set; }
        public string Filename { get; set; }
        public string Confirmfilename { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string ModIp { get; set; }
        public DateTime Modtime { get; set; }
    }
}
