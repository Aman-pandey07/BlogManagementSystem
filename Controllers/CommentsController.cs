using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos.CommentsDtos;
using BlogManagementSystem.Mappers;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Controllers
{
    //[Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CommentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Get All Comments for a Blog
        [HttpGet("ByBlog/{blogId}")]
        public async Task<ActionResult<IEnumerable<GetCommentDto>>> GetCommentsByBlogId(int blogId)
        {
            var comments = await _db.Comments
                                         .Where(c => c.BlogId == blogId)
                                         .ToListAsync();

            if (comments == null || !comments.Any())
                return NotFound("No comments found for the given Blog Id.");

            var commentDtos = comments.Select(c => c.ToGetAllCommentDtos());
            return Ok(commentDtos);
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<GetCommentDto>> GetCommentById(int commentId)
        {
            var comment = await _db.Comments
                                        .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (comment == null)
                return NotFound("Comment not found.");

            return Ok(comment.ToGetAllCommentDtos());
        }

        // Add Comment
        [HttpPost("Add")]
        public async Task<ActionResult<GetCommentDto>> AddCommentToBlog(CreateCommentDto createCommentDto)
        {
            var blog = await _db.Blogs
                                     .FirstOrDefaultAsync(b => b.BlogId == createCommentDto.BlogId);

            if (blog == null)
                return NotFound("Blog not found.");

            var user = await _db.User
                                     .FirstOrDefaultAsync(u => u.UserId == createCommentDto.UserId);

            if (user == null)
                return NotFound("User not found.");

            var newComment = createCommentDto.ToCreateCommentModel(user, blog);

            _db.Comments.Add(newComment);
            await _db.SaveChangesAsync();

            var createdCommentDto = newComment.ToGetAllCommentDtos();
            return CreatedAtAction(nameof(GetCommentById), new { commentId = newComment.CommentId }, createdCommentDto);
        }

        // Update Comment
        [HttpPut("Update/{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, UpdateCommentDto updateCommentDto)
        {
            var existingComment = await _db.Comments
                                                .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (existingComment == null)
                return NotFound("Comment not found.");

            existingComment.UpdateCommentModel(updateCommentDto);

            _db.Entry(existingComment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        //// Delete Comment
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComment(int id)
        //{
        //    var comment = await _db.Comments.FindAsync(id);
        //    if (comment == null)
        //        return NotFound();

        //    _db.Comments.Remove(comment);
        //    await _db.SaveChangesAsync();
        //    return NoContent();
        //}

    }
}
