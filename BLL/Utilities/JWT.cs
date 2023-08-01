using Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public class JWT
    {
        public static string generateToken(User in_user, List<string> in_userProfiles, string in_secret, string in_audience, string in_issuer, int in_timeLifeMinutes) {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(in_secret);

           


            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,in_user.UserId.ToString() ),
                new Claim(ClaimTypes.Email, in_user.UserEmail ),
                new Claim(ClaimTypes.Name, in_user.UserName ),
                         new Claim(ClaimTypes.Role, JsonSerializer.Serialize(in_userProfiles))
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = in_audience,
                Issuer = in_issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(in_timeLifeMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }

    }
}
