using Microsoft.AspNetCore.Mvc;
using VisoiuComputers.Core.Dtos.Common;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Core.Services;

namespace VisoiuComputers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly ComponentService _componentService;
        private readonly ILogger<ComponentsController> _logger;

        public ComponentsController(ComponentService componentService, ILogger<ComponentsController> logger)
        {
            _componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetComponent()
        {
            try
            {
                _logger.LogInformation("Getting all components");
                var result = await _componentService.GetComponentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting component");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            try
            {
                _logger.LogInformation("Getting component with id {ComponentId}", id);

                var component = await _componentService.GetComponentsAsync(id);

                if (component == null)
                {
                    return NotFound($"Component with ID {id} not found");
                }

                return Ok(component);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Component with id {ComponentId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPost("add-component")]
        public async Task<IActionResult> AddComponent([FromBody] AddComponentRequest request)
        {
            await _componentService.AddComponentAsync(request);

            return Ok("Component added successfully");
        }
    }
}
