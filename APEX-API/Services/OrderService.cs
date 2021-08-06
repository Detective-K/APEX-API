using APEX_API.Models;
using System;
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
        public object OrderList(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Cust>(feStr);
            var feOrder = JsonSerializer.Deserialize<Order>(feStr);
            var joinOrder = _web2Context.Orders.Join(_web2Context.Custs,
                o => o.CustId,
                c => c.CustId,
                (o, c) => new
                {
                    o.OrderId,
                    o.CustId,
                    o.OrderDate,
                    o.DelivWay,
                    o.DelivAddr,
                    o.DelivTel,
                    o.Attn,
                    o.Ostatus,
                    o.Pono,
                    o.Memo,
                    c.Name
                })
                .Join(_web2Context.OrderDetails,
                o=>o.OrderId,
                od=>od.OrderId,
                (o,od)=> new 
                {
                    od.Spec
                }
                )
                .OrderByDescending(oc => oc.OrderDate).Select(oc => oc);
            if (!string.IsNullOrEmpty(feObject.CustId))
            {
                joinOrder = joinOrder.Where(oc => oc.CustId == feObject.CustId);
            }
            if (!string.IsNullOrEmpty(feOrder.Ostatus))
            {
                joinOrder = joinOrder.Where(oc => oc.Ostatus == feOrder.Ostatus);
            }
            return joinOrder.ToList();
        }

    }
}
