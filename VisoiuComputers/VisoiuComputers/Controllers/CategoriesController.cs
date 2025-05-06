using Microsoft.AspNetCore.Mvc;
using VisoiuComputers.Core.Dtos.Common;
using VisoiuComputers.Core.Dtos.Requests;
using VisoiuComputers.Core.Services;

namespace VisoiuComputers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategory()
        {
            try
            {
                _logger.LogInformation("Getting all categories");
                var result = await _categoryService.GetCategoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            try
            {
                _logger.LogInformation("Getting category with id {CategoryId}", id);

                var category = await _categoryService.GetCategoriesAsync(id);

                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category with id {CategoryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
        {
            await _categoryService.AddCategoryAsync(request);

            return Ok("Category added successfully");
        }
    }
}
