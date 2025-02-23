using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }


        //Get all the blogs
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _db.Blogs
                .Select(b => new BlogDto
                {
                    BlogId = b.BlogId,
                    BlogTitle = b.BlogTitle,
                    BlogContent = b.BlogContent,
                    BlogImage = b.BlogImage,
                    CreatedAt = b.CreatedAt,
                    UserId = b.UserId
                }).ToListAsync();

            return Ok(blogs);
        }

        //Get the blog by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _db.Blogs
               .Where(b => b.BlogId == id)
               .Select(b => new BlogDto
               {
                   BlogId = b.BlogId,
                   BlogTitle = b.BlogTitle,
                   BlogContent = b.BlogContent,
                   BlogImage = b.BlogImage,
                   CreatedAt = b.CreatedAt,
                   UserId = b.UserId
               }).FirstOrDefaultAsync();

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        //Create new blog
        [HttpPost]
        public async Task<IActionResult> CreateNewBlog([FromBody] CreateBlogDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _db.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            var category = await _db.Categories.FirstOrDefaultAsync(); // Assuming you have a default category
            if (category == null)
                return NotFound("Category not found");

            var blog = new BlogModel
            {
                BlogTitle = dto.BlogTitle,
                BlogContent = dto.BlogContent,
                BlogImage = dto.BlogImage,
                UserId = dto.UserId,
                CreatedAt = DateTime.Now,
                UserModel = new UserModel
                {
                    UserId = int.Parse(user.Id),
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserPhoneNumber = long.TryParse(user.PhoneNumber, out var phoneNumber) ? phoneNumber : (long?)null,
                    UserDp = null, // Assuming you have a way to get the UserDp
                    UserIsAuthor = true, // Assuming the user is an author
                    UserCreatedAt = DateTime.Now // Assuming the user creation date is now
                },
                CategoryModel = category
            };

            _db.Blogs.Add(blog);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlogById), new { id = blog.BlogId }, blog);
        }

        //Update blog details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogDetails(int id , [FromBody] UpdateBlogDto dto)
        {
            var blog = await _db.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            blog.BlogTitle = dto.BlogTitle;
            blog.BlogContent = dto.BlogContent;
            blog.BlogImage = dto.BlogImage;
            await _db.SaveChangesAsync();
            return Ok(blog);
        }

        ////Delete Blog
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBlog(int id)
        //{
        //    var blog = await _db.Blogs.FindAsync(id);
        //    if (blog == null)
        //        return NotFound();

        //    _db.Blogs.Remove(blog);
        //    await _db.SaveChangesAsync();
        //    return NoContent();
        //}


    }
}
