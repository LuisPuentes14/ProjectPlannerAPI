using API.Models.RequestModels;
using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsAppsettings;
using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IServiceAuthentication _serviceLogin;

        public AuthenticationController(IServiceAuthentication serviceLogin, IMapper mapper)
        {
            _serviceLogin = serviceLogin;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin rMLogin)
        {
            GenericResponse<ResponseLogin> gResponse = new GenericResponse<ResponseLogin>();

            try
            {
                string token = await _serviceLogin.Login(_mapper.Map<User>(rMLogin));

                ResponseLogin responseLogin = new ResponseLogin() { Token = token };

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

        [HttpPost]
        [Route("SendEmailResetPassword")]
        public async Task<IActionResult> SendEmailResetPassword([FromBody] RequestSendEmailResetPassword resetPassword)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {               
                await _serviceLogin.SendEmailResetPassword(_mapper.Map<User>(resetPassword));
                gResponse.Status = true;
                gResponse.Message = "Corre enviado exitosa mente.";

                return StatusCode(StatusCodes.Status200OK, gResponse);

            }
            catch (Exception ex)
            {
                gResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] RequestResetPassword requestResetPassword)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Status = true;
                gResponse.Message = "Contraseña actualizada correctamente.";

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
