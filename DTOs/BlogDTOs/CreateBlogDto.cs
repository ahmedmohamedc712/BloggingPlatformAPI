using System;

namespace BloggingPlatform.DTOs.BlogDTOs;

public class CreateBlogDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}
