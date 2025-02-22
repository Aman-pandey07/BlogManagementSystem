using Microsoft.AspNetCore.Identity;

namespace BlogManagementSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
    }
}
