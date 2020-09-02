﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public TokenController(IConfiguration Configuration,ISecurityService securityService)
        {
            _securityService = securityService;
            _configuration = Configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            //Si es un usuario valido
            var validation = await IsValidUser(login);

            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }
            return NotFound();

        }
        private async Task<(bool,Security)> IsValidUser(UserLogin login)
        {
            var user = await _securityService.GetLoginByCredentials(login);

            return (user != null, user);
        }
        private string GenerateToken(Security security)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secretkey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims (Informacion a enviar)

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                //new Claim(ClaimTypes.Email,"jcaraballo74@hotmail.com"),
                new Claim("user",security.User),
                new Claim(ClaimTypes.Role,security.Role.ToString()),
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
