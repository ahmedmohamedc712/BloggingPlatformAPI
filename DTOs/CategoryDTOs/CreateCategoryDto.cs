using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.DTOs.CategoryDTOs;

public class CreateCategoryDto
{
    [Required]
    public string  Title { get; set; } = null!;
}
