using System;
using AutoMapper;
using BloggingPlatform.DTOs.CategoryDTOs;
using BloggingPlatform.Models;

namespace BloggingPlatform.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, ReadCategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

    }
}
