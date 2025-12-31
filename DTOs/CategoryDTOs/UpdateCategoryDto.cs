using System;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.DTOs.CategoryDTOs;

public class UpdateCategoryDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
}
