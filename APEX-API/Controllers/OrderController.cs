using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APEX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        //// GET: api/<OrderController>
        //[Route("")]
        //[Route("Home")]
        //[Route("[controller]/[action]")]
        //public string Index()
        //{
        //    return Convert.ToString("yes");
        //}

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<OrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return Convert.ToString(id);
        //}

        //// GET api/<OrderController>/5
        //[HttpGet ("[action]")]
        //public string Get2()
        //{
        //    return Convert.ToString("get2");
        //}

        //// POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public string Delete(int id)
        //{
        //    return Convert.ToString(id);
        //}


        [HttpGet("noauth")]
        [AllowAnonymous]
        public string Get()
        {
            return "这个方法不需要权限校验";
        }

        [HttpGet("auth")]
        public string Get2()
        {
            return "这个方法需要权限校验";
        }

    }

}
