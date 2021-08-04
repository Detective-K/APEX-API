using APEX_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APEX_API.Services
{
    public class OrderService
    {
        private readonly web2Context _web2Context;

        public OrderService(web2Context context)
        {
            _web2Context = context;
        }

        public int CheckMember(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Sale>(feStr);
            int MemberCount = _web2Context.Sales.Where(x => x.SalesId == feObject.SalesId && x.Pwd == feObject.Pwd && x.ClaimLevel != 0).Count();
            return MemberCount;
        }
    }
}
