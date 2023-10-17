using ECommerce.Model.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Token
{
    public class JWTToken
    {
        public static string GenerateToken(User user, string secretKey,string duration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new ClaimsIdentity(
                new Claim[]{
                    new Claim("UserId" , user.UserId.ToString()),
                    new Claim("FirstName",user.FirstName),
                    new Claim("LastName",user.LastName),
                    new Claim("Address",user.Address),
                    new Claim("Mobile",user.Mobile),
                    new Claim("Email" , user.Email),
                    new Claim("CreatedAt",user.CreatedAt),
                    new Claim("ModifiedAt",user.ModifiedAt)
                }) ;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "Test",
                Audience = "Test",
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(Int32.Parse(duration)),
                SigningCredentials = signInCredentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);


            return handler.WriteToken(token);
        }
    }
}
