using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniTask_API.Dtos;
using MiniTask_API.Models;
using MiniTask_API.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniTask_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryRepository.GetCategoriesByUserAsync(userId);

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Ok(categoryDtos);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryRepository.GetCategoryByIdAsync(id, userId);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDto);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var category = new Category
            {
                Name = categoryDto.Name,
                UserId = userId
            };

            await _categoryRepository.CreateCategoryAsync(category);

            categoryDto.Id = category.Id;
            return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.Id }, categoryDto);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id != categoryDto.Id)
            {
                return BadRequest("Category ID mismatch");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            category.Name = categoryDto.Name;

            await _categoryRepository.UpdateCategoryAsync(category);

            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryRepository.GetCategoryByIdAsync(id, userId);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            await _categoryRepository.DeleteCategoryAsync(category);
            return NoContent();
        }
    }
}