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
        public object OrderList(string feStr)
        {
            var feObject = JsonSerializer.Deserialize<Cust>(feStr);
            var feOrder = JsonSerializer.Deserialize<Order>(feStr);
            //var joinOrder = _web2Context.Orders.Join(_web2Context.Custs,
            //    o => o.CustId,
            //    c => c.CustId,
            //    (o, c) => new
            //    {
            //        o.OrderId,
            //        o.CustId,
            //        o.OrderDate,
            //        o.DelivWay,
            //        o.DelivAddr,
            //        o.DelivTel,
            //        o.Attn,
            //        o.Ostatus,
            //        o.Pono,
            //        o.Memo,
            //        c.Name
            //    })
            //    .GroupJoin(_web2Context.OrderDetails,
            //    oc => oc.OrderId,
            //    od => od.OrderId,
            //    (oc, od) => new
            //    {
            //        oc = oc,
            //        od = od.DefaultIfEmpty()
            //    })
            //    .SelectMany(ocd => ocd.od.Select( ocd => ocd))
            //    ;

            //var joinOrder = _web2Context.Orders.GroupJoin(_web2Context.Custs,
            //    o => o.CustId,
            //    c => c.CustId,
            //    (o, c) => new
            //    {
            //        o.OrderId,
            //        o.CustId,
            //        o.OrderDate,
            //        o.DelivWay,
            //        o.DelivAddr,
            //        o.DelivTel,
            //        o.Attn,
            //        o.Ostatus,
            //        o.Pono,
            //        o.Memo,
            //        c.Name
            //    });

            var result = from person in _web2Context.Orders
                         join detail in _web2Context.OrderDetails on person.OrderId equals detail.OrderId into Details
                         from m in Details.DefaultIfEmpty()
                         select new
                         {
                             id = person.OrderId,
                             firstname = person.OrderNo,
                             lastname = person.OrderDate,
                                  m
                         };


            //if (!string.IsNullOrEmpty(feObject.CustId))
            //{
            //    joinOrder = joinOrder.Where(ocd => ocd.oc.CustId == feObject.CustId);
            //}
            //if (!string.IsNullOrEmpty(feOrder.Ostatus))
            //{
            //    joinOrder = joinOrder.Where(ocd => ocd.oc.Ostatus == feOrder.Ostatus);
            //}

            //joinOrder = joinOrder.OrderByDescending(ocd => ocd.oc.OrderDate);


            return result.ToList();
        }



    }
}
