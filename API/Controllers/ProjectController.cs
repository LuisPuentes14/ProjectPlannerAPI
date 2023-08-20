using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IServiceProject _serviceProject;

        public ProjectController(IServiceProject serviceProject, IMapper mapper) { 
            _serviceProject = serviceProject;
            _mapper = mapper;        
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
