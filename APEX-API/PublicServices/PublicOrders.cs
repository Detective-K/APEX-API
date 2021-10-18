using APEX_API.Services;
using APEX_API.TopprodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APEX_API.PublicServices
{
    public class PublicOrders
    {
        private readonly OrderService _orderService;
        public PublicOrders(OrderService orderService)
        {
            _orderService = orderService;
        }
        public void GBResult(dynamic OData, string type, string addbuy)
        {
            string MS = string.Empty;
            List<TcOekFile>  MortorInfo = _orderService.GetMotorInfoDetail(OData["Motor"]);
            List<Reducer1Order> ReducerInfo = _orderService.GetReducerInfo(OData["GearBox"]);
            if (Convert.ToString(type) == "2")
            {
                if (MortorInfo.Count() > 0)
                {
                    MS = Convert.ToDecimal(MortorInfo.FirstOrDefault().TcOek09).ToString();
                }
            }

            if (Convert.ToString(type) == "1")
            {
                //20180807 AFX220 AXFR220 只有日本和業務可下可以下 , 全世界都關
                if ((Convert.ToString(OData["custId"]) == "BAJ002" || Convert.ToString(OData["isSale"]) == "Y") && ( "R19A,R20A".Contains(Convert.ToString(OData["GearBox"]["GBSeries"]))))
                {
                    ReducerInfo = ReducerInfo.Where(r => ("R19A,R19B,R20A").Contains(r.TcMmd01.Substring(0, 4))).ToList();
                }
                else if ("R19,R20".Contains(Convert.ToString(OData["GearBox"]["GBSeries"])))
                {
                    g_sql = "SELECT * FROM Reducer1_Order where substr(tc_mmd01,1,4) NOT in ('R19A','R19B','R20A') AND tc_mmd03 = '" + GBType + "' and tc_mmd04 = " + Convert.ToSingle(Ratio);
                    ReducerInfo = ReducerInfo.Where(r => !("R19A,R19B,R20A").Contains(r.TcMmd01.Substring(0, 4))).ToList();
                }
                else
                {
                    g_sql = "SELECT * FROM Reducer1_Order where tc_mmd03 = '" + GBType + "' and tc_mmd04 = " + Convert.ToSingle(Ratio);
                }

                //ViewState["mysql_1"] = g_sql;
            }

        }
    }
}
