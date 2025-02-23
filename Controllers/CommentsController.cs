using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Controllers
{
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
        [HttpGet("blog/{BlogId}")]
        public async Task<IActionResult> GetCommentsByBlog(int blogId)
        {
            var comments = await _db.Comments
                 .Where(c => c.BlogId == blogId)
                 .Select(c => new CommentDto
                 {
                     CommentId = c.CommentId,
                     CommentContent = c.CommentContent,
                     CommentCreatedAt = c.CommentCreatedAt,
                     UserId = c.UserId,
                     BlogId = c.BlogId
                 }).ToListAsync();

            return Ok(comments);
        }

        // Get Comment by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _db.Comments
                 .Where(c => c.CommentId == id)
                 .Select(c => new CommentDto
                 {
                     CommentId = c.CommentId,
                     CommentContent = c.CommentContent,
                     CommentCreatedAt = c.CommentCreatedAt,
                     UserId = c.UserId,
                     BlogId = c.BlogId
                 }).FirstOrDefaultAsync();

            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // Add Comment
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch User and Blog Models
            var applicationUser = await _db.Users.FindAsync(dto.UserId);
            var blog = await _db.Blogs.FindAsync(dto.BlogId);

            if (applicationUser == null || blog == null)
                return NotFound("User or Blog not found.");

            // Manually map ApplicationUser to UserModel
            var user = new UserModel
            {
                UserId = int.Parse(applicationUser.Id), // Convert string to int
                UserName = applicationUser.UserName ?? string.Empty, // Handle possible null reference
                UserEmail = applicationUser.Email ?? string.Empty // Handle possible null reference
                // Map other necessary properties
            };

            var comment = new CommentModel
            {
                CommentContent = dto.CommentContent,
                CommentCreatedAt = DateTime.Now,
                UserId = dto.UserId,
                BlogId = dto.BlogId,
                UserModel = user,      // Use the mapped UserModel
                BlogModel = blog
            };

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, comment);
        }

        // Update Comment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto dto)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            comment.CommentContent = dto.CommentContent;
            await _db.SaveChangesAsync();
            return Ok(comment);
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
