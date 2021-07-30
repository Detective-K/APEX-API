using System;
using System.Collections.Generic;

#nullable disable

namespace APEX_API.Models
{
    public partial class AgentsMember
    {
        public string AgentId { get; set; }
        public string MemberId { get; set; }
        public string MemberPwd { get; set; }
        public string MemberName { get; set; }
        public string MemberAddress { get; set; }
        public string MemberMail { get; set; }
        public string MemberTel { get; set; }
        public string MemberLevel { get; set; }
        public string MemberStatus { get; set; }
    }
}
