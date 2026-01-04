using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BloggingPlatform.Data;
using BloggingPlatform.DTOs.BlogDTOs;
using BloggingPlatform.DTOs.CategoryDTOs;
using BloggingPlatform.Exceptions;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Services;

public class BlogService(AppDbContext context, IMapper mapper) : IBlogService
{
    private const int DEFAULT_CATEGORY = 1;
    public async Task<IEnumerable<ReadBlogDto>> GetBlogs(int categoryId, string tag)
    {
        Category? category = await context.Categories
            .AsNoTracking()
            .Include(x => x.Blogs)!
            .ThenInclude(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == categoryId);

        IEnumerable<Blog> blogs = new List<Blog>();
        if (category is not null)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                blogs = category.Blogs!
                .Where(b => b.Tags.Select(y => y.TagId)
                .Contains(tag, StringComparer.OrdinalIgnoreCase));

                if (blogs.Count() == 0)
                {
                    throw new NotFoundException("There are no blogs with this tag.");
                }
            }
            else
            {
                blogs = category.Blogs!;
            }
        }
        else
        {
            throw new NotFoundException("Category not found.");
        }

        var result = mapper.Map<IEnumerable<ReadBlogDto>>(blogs);
        for (int i = 0; i < result.Count(); i++)
        {
            result.ElementAt(i).Tags = blogs.ElementAt(i).Tags.Select(x => x.TagId).ToList();
        }
        return result;
    }

    public async Task<ReadBlogDto> GetBlogById(int id)
    {
        Blog? blog = await context.Blogs
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (blog is null)
        {
            throw new NotFoundException("Blog not found.");
        }

        var blogDto = mapper.Map<ReadBlogDto>(blog);
        blogDto.Tags = blog.Tags.Select(x => x.TagId).ToList();
        return blogDto;
    }

    public async Task<ReadBlogDto> CreateBlog(CreateBlogDto blogDto, int categoryId)
    {
        if (categoryId != DEFAULT_CATEGORY)
        {
            bool isCategoryFound = await context.Categories.AnyAsync(x => x.Id == categoryId);
            if (!isCategoryFound)
            {
                throw new NotFoundException("Category not found.");
            }
        }

        var tags = new List<Tag>();
        foreach (var tag in blogDto.TagList ?? Enumerable.Empty<string>())
        {
            var t = await context.Tags.FindAsync(tag);
            if (t is null)
            {
                t = new Tag { TagId = tag };
                await context.AddAsync(tag);
            }
            tags.Add(t);
        }
        await context.SaveChangesAsync();

        Blog newBlog = mapper.Map<Blog>(blogDto);
        newBlog.CategoryId = categoryId;
        newBlog.CreatedAt = DateTime.UtcNow;
        newBlog.UpdatedAt = DateTime.UtcNow;
        await context.Blogs.AddAsync(newBlog);

        await context.BlogTags.AddRangeAsync(
            tags.Select(x => new BlogTag { Blog = newBlog, Tag = x })
        );

        await context.SaveChangesAsync();
        var result = mapper.Map<ReadBlogDto>(newBlog);
        result.Tags = blogDto.TagList!;

        return result;
    }

    public async Task<ReadBlogDto> UpdateBlog(int id, UpdateBlogDto blogDto)
    {
        if (id != blogDto.Id)
            throw new BadRequestException("Blog id doesn't match the provided id.");

        var categoryExists = await context.Categories
            .AnyAsync(c => c.Id == blogDto.CategoryId);

        if (!categoryExists)
            throw new NotFoundException("Category not found.");

        var blogToUpdate = await context.Blogs
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (blogToUpdate is null)
            throw new NotFoundException("Blog not found.");

        blogToUpdate.Title = blogDto.Title;
        blogToUpdate.Content = blogDto.Content;
        blogToUpdate.CategoryId = blogDto.CategoryId;
        blogToUpdate.UpdatedAt = DateTime.UtcNow;

        var requestedTags = blogDto.TagList ?? new List<string>();

        var existingDbTags = await context.Tags
            .Where(t => requestedTags.Contains(t.TagId))
            .Select(t => t.TagId)
            .ToListAsync();

        var newTagIds = requestedTags.Except(existingDbTags).ToList();

        if (newTagIds.Any())
        {
            await context.Tags.AddRangeAsync(
                newTagIds.Select(t => new Tag { TagId = t })
            );
        }

        await context.BlogTags
            .Where(bt => bt.BlogId == blogToUpdate.Id && !requestedTags.Contains(bt.TagId))
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();

        var result = mapper.Map<ReadBlogDto>(blogToUpdate);
        result.Tags = blogToUpdate.Tags.Select(t => t.TagId).ToList();
        return result;
    }

    public async Task DeleteBlog(int id)
    {
        Blog? blogToDelete = await context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blogToDelete is null)
            throw new NotFoundException("Blog not found.");

        context.Blogs.Remove(blogToDelete);
        await context.SaveChangesAsync();
    }
}