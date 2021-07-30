using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class Sale
    {
        public string SalesId { get; set; }
        public string EmployeeCnName { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public string Receiver { get; set; }
        public int ClaimLevel { get; set; }
        public string ExceptionLoginIp { get; set; }
    }
}
