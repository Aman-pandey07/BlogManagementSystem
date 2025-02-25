using BlogManagementSystem.Dtos.CategoryDtos;
using BlogManagementSystem.Dtos.CommentsDtos;
using BlogManagementSystem.Models;
using System.Runtime.CompilerServices;

namespace BlogManagementSystem.Mappers
{
    public static class CategoryDtoMapper
    {
        public static GetAllCategoryDto ToGetAllCategoryDto(this CategoryModel catModel)
        {
            return new GetAllCategoryDto
            {
                CategoryId = catModel.CategoryId,
                CategoryName = catModel.CategoryName,
                CategoryDescription = catModel.CategoryDescription ?? string.Empty
            };
        }

        public static CategoryModel ToCreateCategoryModel(this CreateCategoryDto createCategoryDto, ICollection<BlogModel> blogModels)
        {
            return new CategoryModel
            {
                CategoryName = createCategoryDto.CategoryName,
                CategoryDescription = createCategoryDto.CategoryDescription,
                BlogModel = blogModels
            };
        }

        public static void UpdateCategoryModel(this CategoryModel categoryModel, UpdateCategoryDto updateCategoryDto)
        {
            categoryModel.CategoryName = updateCategoryDto.CategoryName;
            categoryModel.CategoryDescription = updateCategoryDto.CategoryDescription ?? categoryModel.CategoryDescription;

        }
    }
}
