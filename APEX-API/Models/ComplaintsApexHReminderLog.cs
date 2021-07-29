using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexHReminderLog
    {
        public int ReminderLogId { get; set; }
        public string ComplaintsNum { get; set; }
        public string Recipient { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime Localtime { get; set; }
    }
}
