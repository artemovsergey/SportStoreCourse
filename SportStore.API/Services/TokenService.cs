using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using SportStore.API.Interfaces;

namespace SportStore.API.Services;

public class TokenService : ITokenService
{

    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
    }

    public string CreateToken(string UserLogin)
    {
       var claims =  new List<Claim>{
         new Claim(JwtRegisteredClaimNames.Name, UserLogin)
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