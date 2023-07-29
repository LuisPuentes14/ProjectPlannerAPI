using Api.Utilities.Response;
using API.Models.RequestModels;
using AutoMapper;
using BLL.Interfaces;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IServiceLogin _serviceLogin;

        public AuthenticationController(IServiceLogin serviceLogin, IMapper mapper) {         
            _serviceLogin = serviceLogin;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] RMLogin rMLogin) {

            GenericResponse<string> gResponse = new GenericResponse<string>();

            User user = await _serviceLogin.Login(_mapper.Map<User>(rMLogin));

            return StatusCode(StatusCodes.Status200OK, gResponse);

        }


    }
}
