using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Application.Interfaces;
using SportStore.Domain;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace SportStore.Application.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
          _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));   
        }

        public string CreateToken(string userName)
        {
           var claims =  new List<Claim>{
             new Claim(JwtRegisteredClaimNames.Name, userName)
           };

           var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

           var tokenDecriptor = new SecurityTokenDescriptor(){
             Subject = new ClaimsIdentity(claims),
             Expires = DateTime.UtcNow.AddDays(7),
             SigningCredentials = creds
           };

           var tokenHandler = new JwtSecurityTokenHandler();
           var token = tokenHandler.CreateToken(tokenDecriptor);
           return tokenHandler.WriteToken(token);

        }
    }
}