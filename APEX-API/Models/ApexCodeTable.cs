using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class ApexCodeTable
    {
        public int ApexCodeId { get; set; }
        public string ApexCodeUnitId { get; set; }
        public string ApexCodeKindId { get; set; }
        public string ApexCodeSn { get; set; }
        public string ApexCodeName { get; set; }
    }
}
