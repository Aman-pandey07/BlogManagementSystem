using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos.BlogsDtos;
using BlogManagementSystem.Mappers;
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
        public async Task<ActionResult<IEnumerable<GetBlogDto>>> GetAllBlogs()
        {
            var blogs = await _db.Blogs
                .Include(b => b.UserModel)
                .Include(b => b.CategoryModel)
                .ToListAsync();

            var blogDtos = blogs.Select(blog => blog.ToGetAllBlogDto()).ToList();
            return Ok(blogDtos);
        }

        //Get the blog by id
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBlogDto>> GetBlogById(int id)
        {
            var blog = await _db.Blogs
                 .Include(b => b.UserModel)
                 .Include(b => b.CategoryModel)
                 .FirstOrDefaultAsync(b => b.BlogId == id);

            if (blog == null)
                return NotFound($"Blog with ID {id} not found.");

            var blogDto = blog.ToGetAllBlogDto();
            return Ok(blogDto);
        }

        //Create new blog
        [HttpPost]
        public async Task<IActionResult> CreateNewBlog([FromBody] CreateBlogDto newBlog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch UserModel from the UserModels table
            var userModel = await _db.User.FirstOrDefaultAsync(u => u.UserId == newBlog.UserId);
            if (userModel == null)
                return NotFound($"User with ID {newBlog.UserId} not found.");


            // Fetch CategoryModel from Categories table
            var categoryModel = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == newBlog.CategoryId);
            if (categoryModel == null)
                return NotFound($"Category with ID {newBlog.CategoryId} not found.");


            // Map DTO to BlogModel
            var blog = newBlog.ToCreateBlogModel(userModel, categoryModel);

            // Add and Save Blog
            _db.Blogs.Add(blog);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.BlogId }, blog);
        }

        //Update blog details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogDetails(int id, [FromBody] UpdateBlogDto updatedBlog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blog = await _db.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound($"Blog with ID {id} not found.");

            blog.UpdateBlogModel(updatedBlog);

            _db.Blogs.Update(blog);
            await _db.SaveChangesAsync();

            return NoContent();
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
