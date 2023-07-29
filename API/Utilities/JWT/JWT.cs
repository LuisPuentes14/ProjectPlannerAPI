using Api.Models.Configurations;
using Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.JWT
{
    public class JWT
    {
        private readonly AppSettings _appSettings;

        public JWT(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


         private string getToken(UserProfile userProfile)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(

                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,userProfile.User.UserId.ToString() ),
                        new Claim(ClaimTypes.Email, userProfile.User.UserEmail ),
                        new Claim(ClaimTypes.Name, userProfile.User.UserName ),
                        new Claim(ClaimTypes.Name, userProfile.Profile.ProfileName)
                       
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)


            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
