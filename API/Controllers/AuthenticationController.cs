﻿using API.Models.RequestModels;
using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Excepcion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IServiceAuthentication _serviceLogin;

        public AuthenticationController(ILogger<AuthenticationController> logger,IServiceAuthentication serviceLogin, IMapper mapper)
        {
            _logger = logger;
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
                _logger.LogInformation("REQUEST: " + JsonSerializer.Serialize(rMLogin));

                string token = await _serviceLogin.Login(_mapper.Map<User>(rMLogin));

                ResponseLogin responseLogin = new ResponseLogin() { Token = token };

                gResponse.Status = true;
                gResponse.Object = responseLogin;

                _logger.LogInformation("RESPONSE: " + JsonSerializer.Serialize(gResponse));
                return StatusCode(StatusCodes.Status200OK, gResponse);               

            }
            catch (GeneralExcepcion ex)
            {                
                gResponse.Message = ex.Message;
                _logger.LogInformation("RESPONSE: " + JsonSerializer.Serialize(gResponse));
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("RESPONSE: Error en el servidor.");
                return StatusCode(StatusCodes.Status500InternalServerError);
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
            catch (GeneralExcepcion ex)
            {
                gResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                var userEmailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

                await _serviceLogin.ResetPassword(userEmailClaim, requestResetPassword.UserPassword);

                gResponse.Status = true;
                gResponse.Message = "Contraseña actualizada correctamente.";

                return StatusCode(StatusCodes.Status200OK, gResponse);

            }
            catch (GeneralExcepcion ex)
            {
                gResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status200OK, gResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        


    }
}
