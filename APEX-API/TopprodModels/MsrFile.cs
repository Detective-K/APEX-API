using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class MsrFile
    {
        public string MsrV { get; set; }
        public DateTime? Msr01 { get; set; }
        public string Msr02 { get; set; }
        public string Msr03 { get; set; }
        public int? Msr04 { get; set; }
        public string Msr05 { get; set; }
        public string Msr06 { get; set; }
        public string Msr07 { get; set; }
        public string Msr08 { get; set; }
        public string LotType { get; set; }
        public short? LotBm { get; set; }
        public string LotNo1 { get; set; }
        public string LotNo2 { get; set; }
        public DateTime? Bdate { get; set; }
        public DateTime? Edate { get; set; }
        public string BukType { get; set; }
        public string BukCode { get; set; }
        public int? PoDays { get; set; }
        public int? WoDays { get; set; }
        public string InclId { get; set; }
        public string InclSo { get; set; }
        public string MsbExpl { get; set; }
        public string OebExpl { get; set; }
        public string MssExpl { get; set; }
        public string SubFlag { get; set; }
        public int? SubDays { get; set; }
        public string Msr09 { get; set; }
        public string InclMds { get; set; }
        public string Msr10 { get; set; }
        public string Msr11 { get; set; }
        public string Msr919 { get; set; }
        public string Msr12 { get; set; }
    }
}
