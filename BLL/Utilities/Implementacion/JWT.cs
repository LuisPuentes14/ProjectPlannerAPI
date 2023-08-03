using BLL.ModelsAppsettings;
using BLL.Utilities.Interfaces;
using Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Utilities.Implementacion
{
    public class JWT: IJWT
    {
        private readonly AppSettings _appSettings;

        public JWT(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string generateToken(User in_user, List<string> in_userProfiles, int in_timeLifeMinutes)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);


            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,in_user.UserId.ToString() ),
                new Claim(ClaimTypes.Email, in_user.UserEmail ),
                new Claim(ClaimTypes.Name, in_user.UserName ),
                         new Claim(ClaimTypes.Role, JsonSerializer.Serialize(in_userProfiles))
            };

            var ff = DateTime.Now;

            // Fecha de emisión del token (ahora)
            DateTime issuedAt = DateTime.UtcNow;

            // Fecha de expiración del token (por ejemplo, 1 hora después de la emisión)
            DateTime expiresAt = issuedAt.AddMinutes(in_timeLifeMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Subject = new ClaimsIdentity(claims),
                NotBefore = issuedAt,
               // Expires = DateTime.UtcNow.AddMinutes(in_timeLifeMinutes),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }

    }
}
