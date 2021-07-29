using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexHCommunication
    {
        public int CommunicationId { get; set; }
        public string ComplaintsNum { get; set; }
        public DateTime Replydate { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime? ReciveDate { get; set; }
        public string InternalFlag { get; set; }
        public string InternalQaItem { get; set; }
        public int? UploadItem { get; set; }
        public string Users { get; set; }
        public string MotionIp { get; set; }
    }
}
