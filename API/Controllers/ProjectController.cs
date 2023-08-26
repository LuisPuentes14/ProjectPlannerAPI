using API.Models.RequestModels;
using API.Models.Response;
using API.Models.ResponseModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsAppsettings;
using Entity;
using Excepcion;
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] RequestProjectAdd requestProject)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {
                bool is_add = await _serviceProject.Add(_mapper.Map<Project>(requestProject));

                genericResponse.Status = true;
                genericResponse.Message = "proyecto agregado";

                return StatusCode(StatusCodes.Status200OK, genericResponse);

            }
            catch (GeneralExcepcion ex)
            {
                genericResponse.Message = ex.Message;
                _logger.LogInformation("RESPONSE: " + JsonSerializer.Serialize(genericResponse));
                return StatusCode(StatusCodes.Status200OK, genericResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] RequestProjectEdit requestProject)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {

                bool is_edit = await _serviceProject.Edit(_mapper.Map<Project>(requestProject));

                genericResponse.Status = true;
                genericResponse.Message = "proyecto editado";

                return StatusCode(StatusCodes.Status200OK, genericResponse);

            }
            catch (GeneralExcepcion ex)
            {
                genericResponse.Message = ex.Message;
                _logger.LogInformation("RESPONSE: " + JsonSerializer.Serialize(genericResponse));
                return StatusCode(StatusCodes.Status200OK, genericResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] RequestProjectDelete requestProject)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {               
                bool is_edit = await _serviceProject.Delete(_mapper.Map<Project>(requestProject));

                genericResponse.Status = true;
                genericResponse.Message = "proyecto eliminado.";


                return StatusCode(StatusCodes.Status200OK, genericResponse);

            }
            catch (GeneralExcepcion ex)
            {
                genericResponse.Message = ex.Message;
                _logger.LogInformation("RESPONSE: " + JsonSerializer.Serialize(genericResponse));
                return StatusCode(StatusCodes.Status200OK, genericResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
