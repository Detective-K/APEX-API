using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class OrderDetail
    {
        public string OrderId { get; set; }
        public short? OrderSub { get; set; }
        public string PartNo { get; set; }
        public string CkpartNo { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public int? Qty { get; set; }
        public double? Price { get; set; }
        public double? SubTot { get; set; }
        public string Memo { get; set; }
        public int? MotoId { get; set; }
        public string MotoName { get; set; }
        public int? Ckqty { get; set; }
        public int OrderDetailId { get; set; }
        public string OrderType { get; set; }
        public string Mtmaker { get; set; }
        public string Creater { get; set; }
        public string IsWarranty { get; set; }
        public string Od001 { get; set; }
        public string Od002 { get; set; }
        public string Discount { get; set; }
        public string TaOeb005 { get; set; }
        public string M1 { get; set; }
        public string P2 { get; set; }
        public string Lubrication { get; set; }
        public string IsWarrantyO { get; set; }
        public string LubricationT1 { get; set; }
        public string MemoM1 { get; set; }
        public int? Bas0011 { get; set; }
        public string Bas0012 { get; set; }
        public string Customize { get; set; }
        public string UseToDeltarobot { get; set; }
        public string AdapterCus { get; set; }
        public string InertiaApp { get; set; }
        public string InertiaAppWarranty { get; set; }
        public DateTime? DeliveDate { get; set; }
        public string DesginTool { get; set; }
        public string AdapterCus2 { get; set; }
        public double? Surcharge { get; set; }
    }
}
