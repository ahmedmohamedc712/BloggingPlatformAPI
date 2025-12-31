using System;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class ReadBlogDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public ICollection<BlogTag> Tags { get; set; } = null!;
}
