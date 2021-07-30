using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class WebOrderCusUpload
    {
        public int Id { get; set; }
        public string CusUploadNum { get; set; }
        public string CustId { get; set; }
        public string SalesId { get; set; }
        public string CreateIp { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
