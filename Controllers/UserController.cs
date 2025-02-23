using BlogManagementSystem.Data;
using BlogManagementSystem.Dtos;
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


        //we have to check once delete functionality after all controller ends research for the delete related functionalities and apply to all the controllers
        //Till then the delete functionalitiies of all the controller is commented


        //Get All users 
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.User.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
                UserPhoneNumber = u.UserPhoneNumber,
                UserDp = u.UserDp ?? Array.Empty<byte>(),
                UserIsAuthor = u.UserIsAuthor,
                UserCreatedAt = u.UserCreatedAt
            }).ToListAsync();

            return Ok(users);
        }


        //Get User by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.User.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
                UserPhoneNumber = u.UserPhoneNumber,
                UserDp = u.UserDp,
                UserIsAuthor = u.UserIsAuthor,
                UserCreatedAt = u.UserCreatedAt
            }).FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Register new user
        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] CreateUserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new UserModel
            {
                UserName = newUser.UserName,
                UserEmail = newUser.UserEmail,
                UserPhoneNumber = newUser.UserPhoneNumber,
                UserDp = newUser.UserDp,
                UserIsAuthor = newUser.UserIsAuthor,
                UserCreatedAt = DateTime.UtcNow
            };

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
