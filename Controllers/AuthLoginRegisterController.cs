using BlogManagementSystem.Data;
using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthLoginRegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly string _jwtKey;
        private readonly string _JwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _JwtExpiry;

        public AuthLoginRegisterController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Key is missing in configuration");
            _JwtIssuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Issuer is missing in configuration");
            _jwtAudience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Audience is missing in configuration");
            _JwtExpiry = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:ExpiryMinutes is missing in configuration"));
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null
                || string.IsNullOrEmpty(registerModel.Name)
                || string.IsNullOrEmpty(registerModel.Email)
                || string.IsNullOrEmpty(registerModel.Password))
            {
                return BadRequest("Invalid registration details");
            }

            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if (existingUser != null)
            {
                return Conflict("Email already Exist");
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Name = registerModel.Name
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User Created Successfully");

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Invalid username or password" });
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { success = false, message = "Invalid username or password" });
            }

            var token = GeneratedJwtToken(user);
            return Ok(new { success = true, token });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return Ok("User logged out successfully.");
        }


        private string GeneratedJwtToken(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(_jwtKey))
            {
                throw new InvalidOperationException("JWT key is not configured.");
            }
            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub ,user.Id),
                        new Claim(JwtRegisteredClaimNames.Email ,user.Email ?? string.Empty),
                        new Claim("Name" , user.Name),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: _jwtIssuer,
                //audience: _jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_JwtExpiry),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
