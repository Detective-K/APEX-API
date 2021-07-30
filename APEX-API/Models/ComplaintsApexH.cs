using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ComplaintsApexH
    {
        public string ComplaintsNum { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ComfirmDate { get; set; }
        public string Ptype { get; set; }
        public string SnNum { get; set; }
        public string ModeNo { get; set; }
        public string Description { get; set; }
        public string Machinery { get; set; }
        public string T2n { get; set; }
        public string DriveType { get; set; }
        public string T2max { get; set; }
        public string F2rB { get; set; }
        public string F2aB { get; set; }
        public string OperationalModeS1 { get; set; }
        public string OperationalModeS5 { get; set; }
        public string AttachmentPartsSetCollar { get; set; }
        public string AttachmentPartsPlug { get; set; }
        public string AttachmentPartsScrews { get; set; }
        public string AttachmentPartsKey { get; set; }
        public string AttachmentPartsBushing { get; set; }
        public string AttachmentPartsManual { get; set; }
        public string State { get; set; }
        public string Users { get; set; }
        public string AcCode { get; set; }
        public string AcCodeBig { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}
