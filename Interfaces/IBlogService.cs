using System;
using BloggingPlatform.DTOs.BlogDTOs;

namespace BloggingPlatform.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<ReadBlogDto>> GetBlogs(int categoryId, string tag);
    Task<ReadBlogDto> GetBlogById(int id);
    Task<ReadBlogDto> CreateBlog(CreateBlogDto blogDto, int categoryId);
    Task<ReadBlogDto> UpdateBlog(int id, UpdateBlogDto blogDto);
    Task DeleteBlog(int id);
}
