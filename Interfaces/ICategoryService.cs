using System;
using BloggingPlatform.DTOs.CategoryDTOs;

namespace BloggingPlatform.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<ReadCategoryDto>> GetCategories();
    Task<ReadCategoryDto> GetCategoryById(int id);
    Task<ReadCategoryDto> CreateCategory(CreateCategoryDto categoryDto);
    Task<ReadCategoryDto> UpdateCategory(int id, UpdateCategoryDto categoryDto);
    Task DeleteCategory(int id);
}
