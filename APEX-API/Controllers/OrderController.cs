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

        public OrderController(web2Context context, OrderService orderService)
        {
            _web2Context = context;
            _orderService = orderService;
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
            var ks = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => new {x.ClaimLevel, x.Pwd , x.SalesId}).ToList();
            var ls = Jil.JSON.Deserialize<dynamic>(Utf8Json.JsonSerializer.ToJsonString(ks));
            //if (MemberCount > 0)
            //{
            //    return Ok(new { code = 200, message = "登入成功"});
            //}
            return BadRequest(new { code = 400, message = "登入失敗，帳號或密碼為空" });
        }


        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public string OrderList(string feStr)
        {
            var joinList = _orderService.OrderList(feStr);
            return System.Text.Json.JsonSerializer.Serialize(joinList);
            //return JsonSerializer.Serialize(joinList);
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public string CustSearchInfo (string CustId)
        {
            var joinList = _orderService.GetCustInfo(CustId);
            return System.Text.Json.JsonSerializer.Serialize(joinList);
        }

        // POST api/<OrderController>
        [HttpPost("[action]")]
        [EnableCors("CorsPolicy")]
        public ActionResult OrderList([FromBody] JsonElement feStr)
        {
            //Utf8Json.JsonSerializer.Deserialize<dynamic>(feStr.ToString())["SaleInfo"].ToString() == "[]"
            if(Convert.ToString(System.Text.Json.JsonSerializer.Deserialize<Cust>(feStr.ToString()).CustId) == "BAI060")
            {
                return BadRequest(new { code = 400, message = "Add Error" });
            }
            string TempOD = _orderService.GetOrderID(feStr.ToString());
            return BadRequest(new { code = 400, message = TempOD });
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return Convert.ToString(id);
        }

        // GET api/<OrderController>/5
        [HttpGet("[action]")]
        public string Get2()
        {
            return Convert.ToString("get2");
        }

        // POST api/<OrderController>
        [HttpPost("Login")]
        [EnableCors("CorsPolicy")]
        public ActionResult Post([FromBody] JsonElement feStr)
        {
            List<Cust> CustInfo = _orderService.CheckCustsMember(feStr.ToString());
            List<Sale> SaleInfo = _orderService.CheckSalesMember(feStr.ToString());
            if (CustInfo.Count == 0) CustInfo.Add( new Cust { CustId = ""   });
            if (SaleInfo.Count == 0) SaleInfo.Add(new Sale { SalesId = "" });

            if ((CustInfo.Count > 0) || (SaleInfo.Count > 0))
            {
                return Ok(new { code = 200, message = "Login successful", CustInfo = CustInfo, SaleInfo = SaleInfo });
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
