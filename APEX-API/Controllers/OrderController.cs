using APEX_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utf8Json;
using Jil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APEX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly web2Context _web2Context;

        public OrderController(web2Context context)
        {
            _web2Context = context;
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
        public string Get()
        {
            //var cd = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => x.Pwd).FirstOrDefault();
            var cd = _web2Context.Sales.Where(x => x.SalesId == "D00482").Count();
            var ks = _web2Context.Sales.Where(x => x.SalesId == "D00482").Select(x => new {x.ClaimLevel, x.Pwd , x.SalesId}).ToList();
            var ls = Jil.JSON.Deserialize<dynamic>(Utf8Json.JsonSerializer.ToJsonString(ks));
            return Utf8Json.JsonSerializer.ToJsonString(ks);
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
        [HttpPost]
        public void Post([FromBody] string value)
        {
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


        [HttpGet("noauth")]
        //[AllowAnonymous]
        public string noauth()
        {
            return [{"Mes":"这个方法不需要权限校验" }]
            ;
        }

        [HttpGet("auth")]
        public string auth()
        {
            return "这个方法需要权限校验";
        }

    }

}
