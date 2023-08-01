using Api.Models.Configurations;
using Api.Utilities.Encrypt;
using API.Models.RequestModels;
using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IServiceLogin _serviceLogin;
        private readonly AppSettings _appSettings;

        public AuthenticationController(IServiceLogin serviceLogin, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _serviceLogin = serviceLogin;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin rMLogin)
        {
            GenericResponse<ResponseLogin> gResponse = new GenericResponse<ResponseLogin>();

            try
            {
                List<string> userProfiles = new List<string>();
                rMLogin.UserPassword = Encrypt.GetSHA256(rMLogin.UserPassword);

                User user = _serviceLogin.Login(_mapper.Map<User>(rMLogin), out userProfiles);


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString() ),
                         new Claim(ClaimTypes.Email, user.UserEmail ),
                         new Claim(ClaimTypes.Name, user.UserName ),
                         new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userProfiles))
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _appSettings.Audience,
                    Issuer = _appSettings.Issuer,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMonths(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);               
                ResponseLogin responseLogin = new ResponseLogin() { Token = tokenHandler.WriteToken(token) };

                gResponse.Status = true;
                gResponse.Object = responseLogin;

                return StatusCode(StatusCodes.Status200OK, gResponse);

            }
            catch (Exception ex)

            {             
                
                gResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }



        }


    }
}
