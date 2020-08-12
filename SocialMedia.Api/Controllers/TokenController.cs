
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        [HttpPost]
        public IActionResult Authentication(UserLogin userLogin)
        {
            //Si es un usuario valido
            if (IsValidUser(userLogin))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();

        }
        private bool IsValidUser(UserLogin login)
        {
            return true;
        }
        private string GenerateToken()
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secretkey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims (Informacion a enviar)

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"Jose Caraballo"),
                new Claim(ClaimTypes.Email,"jcaraballo74@hotmail.com"),
                new Claim(ClaimTypes.Role,"Administrator"),
            };

            //Playload

            var playload = new JwtPayload(

                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)

            );

            //Signature

            var token = new JwtSecurityToken(header, playload);
            return new JwtSecurityTokenHandler().WriteToken(token);

            //return string.Empty;
        }
    }
}
