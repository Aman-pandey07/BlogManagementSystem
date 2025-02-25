using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos.CategoryDtos;
using BlogManagementSystem.Mappers;
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
        public async Task<ActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var blogModels = await _db.Blogs
                                           .Where(b => createCategoryDto.BlogModelIds.Contains(b.BlogId))
                                           .ToListAsync();

            var newCategory = createCategoryDto.ToCreateCategoryModel(blogModels);
            _db.Categories.Add(newCategory);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory.ToGetAllCategoryDto());
        }


        // Get all categories
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                return NotFound();

            category.UpdateCategoryModel(updateCategoryDto);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        // Get category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllCategoryDto>> GetCategoryById(int id)
        {
            var category = await _db.Categories
                                         .Include(c => c.BlogModel)
                                         .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                return NotFound();

            return Ok(category.ToGetAllCategoryDto());
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
