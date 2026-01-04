using System;
using AutoMapper;
using Azure;
using BloggingPlatform.Data;
using BloggingPlatform.DTOs.BlogDTOs;
using BloggingPlatform.DTOs.CategoryDTOs;
using BloggingPlatform.Exceptions;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Services;

public class CategoryService(AppDbContext context, IMapper mapper) : ICategoryService
{
    public record BlogBrief(int Id, string Title, IEnumerable<string> Tags);
    public record FullCategory(int Id, string Title, IEnumerable<BlogBrief> Blogs);
    private const int DefaultCategoryId = 1;
    public async Task<IEnumerable<ReadCategoryDto>> GetCategories()
    {
        var blogs = await context.Categories
        .AsNoTracking()
        .ToListAsync();
        
        var result = mapper.Map<IEnumerable<ReadCategoryDto>>(blogs);
        return result;
    }

    public async Task<FullCategory> GetCategoryById(int id)
    {
        Category? category = await context.Categories
            .AsNoTracking()
            .Include(x => x.Blogs)!
            .ThenInclude(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
        {
            throw new NotFoundException("Category not found.");
        }

        IEnumerable<BlogBrief> blogs = category.Blogs!
        .Select(x => new BlogBrief(x.Id, x.Title, x.Tags.Select(x => x.TagId)));

        FullCategory result = new (category.Id, category.Title, blogs);
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
