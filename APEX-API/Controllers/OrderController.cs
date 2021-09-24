using APEX_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utf8Json;
//using Jil;
using System.Text.Json;
using System.Text.Json.Serialization;
using APEX_API.Services;
using Microsoft.AspNetCore.Cors;
using APEX_API.PublicServices;
using APEX_API.TopprodModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APEX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly web2Context _web2Context;
        private readonly OrderService _orderService;
        private readonly PublicFunctions _publicFunction;

        public OrderController(web2Context context, OrderService orderService, PublicFunctions publicFunctions)
        {
            _web2Context = context;
            _orderService = orderService;
            _publicFunction = publicFunctions;
        }

        // GET: api/<OrderController>
        [Route("")]
        [Route("Home")]
        [Route("[controller]/[action]")]
        public string Index()
        {
            return Convert.ToString("yes");
        }

        [HttpGet]
        public ActionResult Get(string jsonStr)
        {
            //var cd = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => x.Pwd).FirstOrDefault();
            //var jsonObject = JSON.DeserializeDynamic(jsonStr);
            //string username = jsonObject.username;
            //string password = jsonObject.password;
            //int MemberCount  = _web2Context.Sales.Where(x => x.SalesId == username && x.Pwd == password && x.ClaimLevel !=0).Count();
            var ks = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => new { x.ClaimLevel, x.Pwd, x.SalesId }).ToList();
            var ls = Jil.JSON.Deserialize<dynamic>(Utf8Json.JsonSerializer.ToJsonString(ks));
            //if (MemberCount > 0)
            //{
            //    return Ok(new { code = 200, message = "登入成功"});
            //}
            return BadRequest(new { code = 400, message = "登入失敗，帳號或密碼為空" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult GearBoxInit(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                var MortorInfo = _orderService.GetMotorInfo(Convert.ToString(OData["isSale"]));
                var ModelInfo = !string.IsNullOrEmpty(Convert.ToString(OData["tcOek01"]))?_orderService.GetModelInfo(OData):"";
                var GearBoxInfo = _orderService.GetGearBoxInfo(OData);
                return Ok(new { code = 200, MortorInfo = MortorInfo , ModelInfo = ModelInfo , GearBoxInfo = GearBoxInfo });
            }
           
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult GearBoxDetail(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                List<TcOekFile> MortorInfo = _orderService.GetMotorInfoDetail(OData);
                var InertiaApp = _publicFunction.FCheck_Single(OData["InertiaApp"]);
                List<Reducer1Order> ReducerInfo = new List<Reducer1Order> { };
                if (("R14,R26,R28,R30,R32,R57,R58,R59,R60,RB6,RB8,RC1,RC3,RC7,RC8,RC9,RC5").Contains(OData["GBSeries"]))
                {
                     ReducerInfo = _orderService.GetReducer(OData, Convert.ToDecimal(MortorInfo.SingleOrDefault().TcOek05), Convert.ToDecimal(MortorInfo.SingleOrDefault().TcOek04), Convert.ToDecimal(MortorInfo.SingleOrDefault().TcOek08));
                }

                //var ModelInfo = !string.IsNullOrEmpty(Convert.ToString(OData["tcOek01"])) ? _orderService.GetModelInfo(OData) : "";
                //var GearBoxInfo = _orderService.GetGearBoxInfo(OData);
                return Ok(new { code = 200, ReducerInfo = ReducerInfo });
            }

            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult CustSearchInfo(string CustId)
        {
            var joinList = _orderService.GetCustInfo(CustId);
            return Ok(new { code = 200, CustInfo = joinList });
        }


        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult ProductPrice(string feStr)
        {
            Double BasePrice = 0;
            Double SellingPrice = 0;
            Double Discount = 0;
            Double DiscountPrice = 0;
            Double FinalCharge = 0;
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                switch (Convert.ToString(OData["OrderType"]))
                {
                    case "Rack":
                        BasePrice = _orderService.GetRackBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _orderService.GetRackLargeDiscount(Convert.ToString(OData["Spec"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["CustId"]));
                        DiscountPrice = System.Math.Round(BasePrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        break;
                    case "Pinion":
                        BasePrice = _orderService.GetRackBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _orderService.GetPinionLargeDiscount(Convert.ToString(OData["Spec"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["CustId"]));
                        DiscountPrice = System.Math.Round(BasePrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        break;
                    default:
                        BasePrice = _orderService.GetBasePrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        Discount = _orderService.GetDiscount(Convert.ToString(OData["Currency"]), BasePrice, Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["PartNo"]));
                        SellingPrice = _orderService.GetSellingPrice(Convert.ToString(OData["CustId"]), Convert.ToString(OData["PartNo"]), Convert.ToString(OData["Currency"]), Convert.ToInt32(OData["Qty"]));
                        DiscountPrice = System.Math.Round(SellingPrice * (1 - Discount), 0, MidpointRounding.AwayFromZero);
                        FinalCharge = DiscountPrice;
                        if (("A,C").ToString().IndexOf(Convert.ToString(OData["PartNo"]).Substring(0, 1)) != -1)
                        {
                            double ChangeOilPrice = _orderService.GetChangeOilPrice(Convert.ToString(OData["PartNo"]), DiscountPrice, Convert.ToString(OData["Lubrication"]), Convert.ToString(OData["CustId"]), Convert.ToString(OData["Currency"]), Convert.ToString(OData["Spec"]));
                            if (ChangeOilPrice != 0)
                            {
                                FinalCharge = ChangeOilPrice;
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(OData["AdapterCus"])))
                            {
                                if (("G4,G5").ToString().IndexOf(Convert.ToString(OData["PartNo"]).Substring(0, 1)) == -1)
                                {
                                    if (Convert.ToString(OData["AdapterCus"]).Substring(0, 1) == "O")
                                    {
                                        FinalCharge = 0;//20190214 O品號要另外報價
                                    }
                                }
                            }
                        }
                        else
                        {
                            DiscountPrice = 0;
                        }

                        break;
                }
                return Ok(new { code = 200, Discount = Discount, FinalCharge = FinalCharge });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult ApexDiscount(string feStr)
        {
            if (!string.IsNullOrEmpty(feStr))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString());
                double Discount = _orderService.GetDiscount(Convert.ToString(OData["Currency"]), Convert.ToString(OData["UnitPrice"]), Convert.ToInt16(OData["Qty"]), Convert.ToString(OData["PartNo"]));
                return Ok(new { code = 200, Discount = Discount });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }


        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public string OrderList(string feStr)
        {
            var joinList = _orderService.OrderList(feStr);
            return System.Text.Json.JsonSerializer.Serialize(joinList);
            //return JsonSerializer.Serialize(joinList);
        }

        // POST api/<OrderController>
        [HttpPost("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult OrderList([FromBody] JsonElement feStr)
        {
            //Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["SaleInfo"].ToString() == "[]"
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                Order Value = Utf8Json.JsonSerializer.Deserialize<Order>(feStr.ToString());
                if (Value.CustId == "BAI060")
                {
                    return BadRequest(new { code = 400, message = "Add Error" });
                }
                string TempOD = _orderService.GetOrderID(Value);
                _orderService.InsertOrder(Value, TempOD);

                return Ok(new { code = 200, message = "Save Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        // PUT api/<OrderController>/5
        [HttpPut("OrderList")]
        [EnableCors("CorsPolicy")]
        public ActionResult OrderListPut([FromBody] JsonElement feStr)
        {
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                dynamic OData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["Order"];
                dynamic OdData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"];
                _orderService.UpdateOrderList(Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["Order"], Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"]);         
                return Ok(new { code = 200, message = "Save Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }


        // DELETE api/<OrderController>/5
        [HttpDelete("OrderList")]
        public ActionResult OrderListDelete([FromBody] JsonElement feStr)
        {

            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                dynamic OdData = Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"];
                _orderService.DeleteOrderList( Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["OrderDetail"]);
                return Ok(new { code = 200, message = "Delete Success" });
            }
            return BadRequest(new { code = 400, message = "Error Request" });
        }

        // POST api/<OrderController>
        [HttpPost("Login")]
        [EnableCors("CorsPolicy")]
        public ActionResult Post([FromBody] JsonElement feStr)
        {
            if (!string.IsNullOrEmpty(feStr.ToString()))
            {
                List<Cust> CustInfo = _orderService.CheckCustsMember(feStr.ToString());
                List<Sale> SaleInfo = _orderService.CheckSalesMember(feStr.ToString());
                if (CustInfo.Count == 0) CustInfo.Add(new Cust { CustId = "" });
                if (SaleInfo.Count == 0) SaleInfo.Add(new Sale { SalesId = "" });

                if ((CustInfo.Count > 0) || (SaleInfo.Count > 0))
                {
                    return Ok(new { code = 200, message = "Login successful", CustInfo = CustInfo, SaleInfo = SaleInfo });
                }
            }
            return BadRequest(new { code = 400, message = "Unable to login" });
        }

        public class SaveReportDetailInput
        {
            public string Username { set; get; }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return Convert.ToString(id);
        }
    }

}
