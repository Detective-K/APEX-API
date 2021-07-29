using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexHInternal
    {
        public int ComplaintsApexHInternalId { get; set; }
        public string ComplaintsNum { get; set; }
        public DateTime Replydate { get; set; }
        public string EmployeeCode { get; set; }
        public string InternalReply { get; set; }
    }
}
