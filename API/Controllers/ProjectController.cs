using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsAppsettings;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        public readonly IServiceProject _serviceProject;

        public ProjectController(ILogger<AuthenticationController> logger ,IServiceProject serviceProject, IMapper mapper) { 
            _serviceProject = serviceProject;
            _mapper = mapper;    
            _logger = logger;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAll() {

            try
            {                

                GenericResponse<ResponseProject> genericResponse = new GenericResponse<ResponseProject>();
                List<ResponseProject> list = _mapper.Map<List<ResponseProject>>(await _serviceProject.GetAll());

                return StatusCode(StatusCodes.Status200OK, new { data = list });

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }    

        }
    }
}
