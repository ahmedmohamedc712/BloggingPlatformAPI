using System;
using BloggingPlatform.DTOs.BlogDTOs;
using BloggingPlatform.Models;

namespace BloggingPlatform.DTOs.CategoryDTOs;

public class ReadCategoryDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
}
