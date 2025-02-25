using BlogManagementSystem.Dtos.BlogsDtos;
using BlogManagementSystem.Models;
using System.Runtime.CompilerServices;

namespace BlogManagementSystem.Mappers
{
    public static class BlogDtosMapper
    {
        public static GetBlogDto ToGetAllBlogDto(this BlogModel blogModel)
        {
            return new GetBlogDto
            {
                BlogTitle = blogModel.BlogTitle,
                BlogContent = blogModel.BlogContent,
                BlogImage = blogModel.BlogImage,
                CreatedAt = blogModel.CreatedAt,
                UserId = blogModel.UserId,
                CategoryId = blogModel.CategoryId
            };
        }

        public static BlogModel ToCreateBlogModel(this CreateBlogDto createBlogDto, UserModel userModel, CategoryModel categoryModel)
        {
            return new BlogModel
            {
                BlogTitle = createBlogDto.BlogTitle,
                BlogContent = createBlogDto.BlogContent,
                BlogImage = createBlogDto.BlogImage,
                CreatedAt = createBlogDto.CreatedAt,
                UserId = createBlogDto.UserId,
                CategoryId = createBlogDto.CategoryId,
                UserModel = userModel,
                CategoryModel = categoryModel
            };
        }

        public static void UpdateBlogModel(this BlogModel blogModel, UpdateBlogDto updateBlogDto)
        {
            blogModel.BlogTitle = updateBlogDto.BlogTitle;
            blogModel.BlogContent = updateBlogDto.BlogContent;
            blogModel.BlogImage = updateBlogDto.BlogImage;
            blogModel.UserId = updateBlogDto.UserId;
            blogModel.CategoryId = updateBlogDto.CategoryId;
        }
    }
}
