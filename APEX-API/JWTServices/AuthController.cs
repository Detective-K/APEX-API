using APEX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APEX_API.JWTServices
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IConfiguration _configuration { get; set; }
        private readonly web2Context _web2Context;

        public AuthController(IConfiguration config , web2Context context)
        {
            _configuration = config;
            _web2Context = context;
        }

        [HttpGet("login")]
        public ActionResult Login(string username, string password)
        {

            int memberCount = _web2Context.Sales.Where(x => x.SalesId == username && x.Pwd == password && x.ClaimLevel != 0).Count();
            if (memberCount > 0)
            {
                var claims = new[]
                {
                    new Claim("userId", username)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication")["SecurityKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Authentication")["Issure"],
                    audience: _configuration.GetSection("Authentication")["Audience"],
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(30),
                    claims: claims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { code = 200, message = "登入成功", data = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest(new { code = 400, message = "登入失敗，帳號或密碼為空" });
        }
    }
}
