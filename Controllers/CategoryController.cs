using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        // Create new category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = new CategoryModel
            {
                CategoryName = dto.CategoryName,
                CategoryDescription = dto.CategoryDescription
            };

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }
        

        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _db.Categories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription
                }).ToListAsync();

            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _db.Categories
                 .Where(c => c.CategoryId == id)
                 .Select(c => new CategoryDto
                 {
                     CategoryId = c.CategoryId,
                     CategoryName = c.CategoryName,
                     CategoryDescription = c.CategoryDescription
                 }).FirstOrDefaultAsync();

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // Update category
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            category.CategoryName = dto.CategoryName;
            category.CategoryDescription = dto.CategoryDescription;
            await _db.SaveChangesAsync();
            return Ok(category);
        }

        //// Delete category
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var category = await _db.Categories.FindAsync(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.Categories.Remove(category);
        //    await _db.SaveChangesAsync();

        //    return NoContent();
        //}


    }
    
}
