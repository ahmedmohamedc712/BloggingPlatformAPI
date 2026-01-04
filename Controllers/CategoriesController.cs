using BloggingPlatform.DTOs.CategoryDTOs;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using BloggingPlatform.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCategoryDto>>> GetAll()
        {
            var categories = await service.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryService.FullCategory>> GetById(int id)
        {
            var category = await service.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ReadCategoryDto>> CreateCategory(CreateCategoryDto categoryDto)
        {
            var category = await service.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetById), new {category.Id} , category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadCategoryDto>> UpdateCategory(int id, UpdateCategoryDto categoryDto)
        {
            var category = await service.UpdateCategory(id, categoryDto);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await service.DeleteCategory(id);
            return NoContent();
        }
    }
}
