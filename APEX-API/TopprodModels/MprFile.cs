using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.TopprodModels
{
    public partial class MprFile
    {
        public string MprV { get; set; }
        public DateTime? Mpr01 { get; set; }
        public string Mpr02 { get; set; }
        public string Mpr03 { get; set; }
        public short? Mpr04 { get; set; }
        public string Mpr05 { get; set; }
        public string Mpr06 { get; set; }
        public string Mpr07 { get; set; }
        public string LotType { get; set; }
        public short? LotBm { get; set; }
        public string LotNo1 { get; set; }
        public string LotNo2 { get; set; }
        public DateTime? Forbdate { get; set; }
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
        public string MpsSave { get; set; }
        public string MpsMsa01 { get; set; }
        public DateTime? Mbdate { get; set; }
        public DateTime? Medate { get; set; }
        public string QtyType { get; set; }
    }
}
