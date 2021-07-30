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
        public string Application { get; set; }
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
        public string Reply { get; set; }
        public string ReturnFlg { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Conclusion { get; set; }
        public string NewsFlg { get; set; }
        public DateTime? ReciveDate { get; set; }
        public string SalesProgress { get; set; }
        public string ProblemAnalysis { get; set; }
        public string Correction { get; set; }
        public string CorrectionPreventive { get; set; }
        public string ConclusionConfirm { get; set; }
        public string Responsibility { get; set; }
        public string Defect { get; set; }
        public string EfgpprocessNo { get; set; }
        public int NewsCustomerFlg { get; set; }
        public string Reason2 { get; set; }
        public string ApexCodeSn { get; set; }
    }
}
