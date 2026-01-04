using BloggingPlatform.DTOs.BlogDTOs;
using BloggingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IBlogService service) : ControllerBase
    {
        private const int DefaultCategory = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadBlogDto>>> GetAll(
            [FromQuery] string? tag,
            [FromQuery] int categoryId = DefaultCategory)
        {
            var result = await service.GetBlogs(categoryId, tag!);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadBlogDto>> GetById(int id)
        {
            var result = await service.GetBlogById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ReadBlogDto>> Create(
            [FromBody] CreateBlogDto blogDto,
            [FromQuery] int categoryId = DefaultCategory)
        {
            var result = await service.CreateBlog(blogDto, categoryId);
            return CreatedAtAction(nameof(GetById), new { result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadBlogDto>> Update(int id, UpdateBlogDto blogDto)
        {
            var result = await service.UpdateBlog(id, blogDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteBlog(id);
            return NoContent();
        }
    }
}
