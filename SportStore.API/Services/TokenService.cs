using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Interfaces;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace SportStore.Application.Services;

public class TokenService : ITokenService
{

    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));   
    }

    public string CreateToken(string UserLogin)
    {
      //  var claims =  new List<Claim>{
      //    new Claim(JwtRegisteredClaimNames.Name, UserLogin)
      //  };

      //  var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      //  var tokenDecriptor = new SecurityTokenDescriptor(){
      //    Subject = new ClaimsIdentity(claims),
      //    Expires = DateTime.UtcNow.AddDays(7),
      //    SigningCredentials = creds
      //  };

      //  var tokenHandler = new JwtSecurityTokenHandler();
      //  var token = tokenHandler.CreateToken(tokenDecriptor);
      //  return tokenHandler.WriteToken(token);
//
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "user") };
        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:TokenKey"]!));

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            // issuer: _config["Jwt:Issuer"],
            // audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(365)),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);

    }
}