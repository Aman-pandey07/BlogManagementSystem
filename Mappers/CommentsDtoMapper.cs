using BlogManagementSystem.Dtos.BlogsDtos;
using BlogManagementSystem.Dtos.CommentsDtos;
using BlogManagementSystem.Models;

namespace BlogManagementSystem.Mappers
{
    public static class CommentsDtoMapper
    {
        public static GetCommentDto ToGetAllCommentDtos(this CommentModel commentModel)
        {
            return new GetCommentDto
            {
                CommentId = commentModel.CommentId,
                CommentContent = commentModel.CommentContent,
                CommentCreatedAt = commentModel.CommentCreatedAt,
                BlogId = commentModel.BlogId,
                UserId = commentModel.UserId,
            };
        }

        public static CommentModel ToCreateCommentModel(this CreateCommentDto createCommentDto,UserModel userModel ,BlogModel blogModel)
        {
            return new CommentModel
            {
                CommentContent = createCommentDto.CommentContent,
                CommentCreatedAt = DateTime.Now,
                UserId =userModel.UserId,
                UserModel = userModel,
                BlogId = blogModel.BlogId,
                BlogModel = blogModel
            };
        }

        public static void UpdateCommentModel(this CommentModel commentModel, UpdateCommentDto updateCommentDto)
        {
            commentModel.CommentContent = updateCommentDto.CommentContent;
        }

    }
}
