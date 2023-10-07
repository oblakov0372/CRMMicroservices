using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRM.Account.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CRM.Account.JWT
{
  public class JwtAuthenticationManager : IJwtAuthenticationManager
  {
    private readonly JwtSettings _jwtSettings;

    public JwtAuthenticationManager(JwtSettings jwtSettings)
    {
      _jwtSettings = jwtSettings;
    }

    public string GenerateToken(AccountEntity user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
          }),
        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
        Issuer = _jwtSettings.Issuer,
        Audience = _jwtSettings.Audience,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }


  }
}