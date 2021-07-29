using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class Order
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string CustId { get; set; }
        public string CustPo { get; set; }
        public string Attn { get; set; }
        public DateTime? DelivDate { get; set; }
        public string DelivWay { get; set; }
        public string DelivAddr { get; set; }
        public string PayTerm { get; set; }
        public string DelivTel { get; set; }
        public string InvAddr { get; set; }
        public string Currency { get; set; }
        public string Rate { get; set; }
        public string TaxType { get; set; }
        public double? Freight { get; set; }
        public double? TaxTot { get; set; }
        public double? OrdTotal { get; set; }
        public string EmployId { get; set; }
        public string Memo { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string QuotId { get; set; }
        public int? ChangePage { get; set; }
        public string AccountNo { get; set; }
        public string Pono { get; set; }
        public DateTime? SalesDate { get; set; }
        public string Ostatus { get; set; }
        public string Deliver { get; set; }
        public string DelivAddr2 { get; set; }
        public string Remail { get; set; }
        public string Mailcode { get; set; }
    }
}
