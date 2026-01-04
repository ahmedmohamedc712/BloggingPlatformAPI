using System;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class ReadBlogDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}
