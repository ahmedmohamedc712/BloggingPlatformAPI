using System;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class UpdateBlogDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int CategoryId { get; set; }
    public ICollection<BlogTag> Tags { get; set; } = null!;
}
