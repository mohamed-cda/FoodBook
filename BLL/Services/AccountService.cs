using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO.Entity;
using BLL.Services;
using BO.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimerNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using BO.DTO.Responses;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public Task<LoginResponse> Login(string username, string password)
        {// Rechercher dans la base de donnée l'utilisateur
            var loginResponse = new LoginResponse()
            {
                Username = username,
                AccessToken = GenerateJwtToken(username, new List<string>() { "Admin","User"})
               
            };
            return Task.FromResult(loginResponse);

        }

        private string GenerateJwtToken(string username, List<string> roles)
        {
           // Add User Infos
           var claims =new List<Claim>
           {
                new Claim(JwtRegisteredClaimerNames.Sub,username),
                new Claim(JwtRegisteredClaimerNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,username)
           };
            // Add Role
            roles.ForEach(role => 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            // Siging Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //Expiration time
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            //Creat JWT Token Object
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires:expires,
                signingCredentials:creds

                     );
            //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
