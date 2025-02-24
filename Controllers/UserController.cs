using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos.UserDtos;
using BlogManagementSystem.Mappers;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }


        //Get All users 
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.User.ToListAsync();
            var userDtos = users.Select(u => u.ToGetAllUserDto());
            return Ok(userDtos);
        }


        //Get User by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToGetAllUserDto());
        }

        //Register new user
        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] CreateUserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = newUser.ToCreateUserDto();

            _db.User.Add(user);

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }



        //Update user details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDetails(int id , [FromBody] UpdateUserDto updatedUser)
        {
            var user = await _db.User.FindAsync(id);
            if (user == null)
                return NotFound();

            user.UserName = updatedUser.UserName;
            user.UserEmail = updatedUser.UserEmail;
            user.UserPhoneNumber = updatedUser.UserPhoneNumber;
            user.UserDp = updatedUser.UserDp;
            user.UserIsAuthor = updatedUser.UserIsAuthor;

            await _db.SaveChangesAsync();

            return Ok(user);
        }

        ////Delete user
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _db.User.FindAsync(id);
        //    if (user == null)
        //        return NotFound();

        //    _db.User.Remove(user);
        //    await _db.SaveChangesAsync();
        //    return NoContent();
        //}


    }
}
