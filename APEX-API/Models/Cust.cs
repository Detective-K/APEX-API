using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class Cust
    {
        public string CustId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Group { get; set; }
        public string Ceo { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax { get; set; }
        public string Invoice { get; set; }
        public string WebSite { get; set; }
        public string CountryCode { get; set; }
        public string Addr { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Tel3 { get; set; }
        public string Deliver { get; set; }
        public string Paymant { get; set; }
        public string PayNo { get; set; }
        public string PayBank { get; set; }
        public DateTime? CreateDate { get; set; }
        public string EmployId { get; set; }
        public string Memo { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public int? ClaimLevel { get; set; }
    }
}
