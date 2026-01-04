using System;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class CreateBlogDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<string>? TagList { get; set; }
}
