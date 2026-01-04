using System;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class UpdateBlogDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<string> TagList { get; set; } = new ();
}
