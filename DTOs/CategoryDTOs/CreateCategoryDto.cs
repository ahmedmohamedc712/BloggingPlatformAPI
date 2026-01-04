using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.DTOs.CategoryDTOs;

public class CreateCategoryDto
{
    public required string Title { get; set; }
}
