using System;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.DTOs.CategoryDTOs;

public class UpdateCategoryDto
{
    [Required]
    public int Id { get; set; }
    public required string Title { get; set; }
}
