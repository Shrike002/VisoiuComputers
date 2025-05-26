using Microsoft.AspNetCore.Mvc;
using VisoiuComputers.Core.Dtos.Common;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Core.Services;
using VisoiuComputers.Core.Exceptions;

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
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetComponent([FromQuery] GetComponentsRequest? request = null)
        {
            try
            {
                _logger.LogInformation("Getting components with filters: Name={Name}, CategoryId={CategoryId}, SortBy={SortBy}, SortOrder={SortOrder}",
                    request?.Name, request?.CategoryId, request?.SortBy, request?.SortOrder);

                IEnumerable<ComponentDto> result;

                if (request != null && (!string.IsNullOrEmpty(request.Name) || request.CategoryId.HasValue || !string.IsNullOrEmpty(request.SortBy)))
                {
                    result = await _componentService.GetComponentsAsync(request);
                }
                else
                {
                    result = await _componentService.GetComponentsAsync();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting components");
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

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateComponent(int id, [FromBody] UpdateComponentRequest request)
        {
            try
            {
                _logger.LogInformation("Updating component with id {ComponentId}", id);

                await _componentService.UpdateComponentAsync(id, request);

                return Ok("Component updated successfully");
            }
            catch (ComponentNotFoundException ex)
            {
                _logger.LogWarning(ex, "Component with id {ComponentId} not found", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating component with id {ComponentId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating component");
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