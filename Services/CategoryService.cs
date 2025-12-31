using System;
using AutoMapper;
using BloggingPlatform.Data;
using BloggingPlatform.DTOs.CategoryDTOs;
using BloggingPlatform.Exceptions;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Services;

public class CategoryService(AppDbContext context, IMapper mapper) : ICategoryService
{
    private const int DefaultCategoryId = 1;
    public async Task<IEnumerable<ReadCategoryDto>> GetCategories()
    {
        var blogs = await context.Categories.ToListAsync();
        var result = mapper.Map<IEnumerable<ReadCategoryDto>>(blogs);
        return result;
    }

    public async Task<ReadCategoryDto> GetCategoryById(int id)
    {
        var category = await context.Categories
            .Include(x => x.Blogs)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
        {
            throw new NotFoundException("Category not found.");
        }

        var result = mapper.Map<ReadCategoryDto>(category);
        return result;
    }

    public async Task<ReadCategoryDto> CreateCategory(CreateCategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        var result = mapper.Map<ReadCategoryDto>(category);
        return result;
    }

    public async Task<ReadCategoryDto> UpdateCategory(int id, UpdateCategoryDto categoryDto)
    {
        if (id != categoryDto.Id)
        {
            throw new BadRequestException("Id doesn't match the category's id");
        }
        Category? categoryToUpdate = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (categoryToUpdate is null)
        {
            throw new NotFoundException("Category not found");
        }

        categoryToUpdate.Title = categoryDto.Title;
        await context.SaveChangesAsync();

        var updatedCategory = mapper.Map<ReadCategoryDto>(categoryToUpdate);
        return updatedCategory;
    }

    public async Task DeleteCategory(int id)
    {
        var categoryToDelete = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoryToDelete is null)
        {
            throw new NotFoundException("Category not found");
        }

        await context.Database.ExecuteSqlRawAsync(
            "UPDATE Blogs SET CategoryId = {0} WHERE CategoryId = {1}",
            DefaultCategoryId,
            id
        );

        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();
    }
}
