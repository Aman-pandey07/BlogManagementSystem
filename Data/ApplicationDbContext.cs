using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BlogManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Comment -> User Relationship
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.UserModel)
                .WithMany(u => u.CommentModels)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent multiple cascade paths

            // Comment -> Blog Relationship
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.BlogModel)
                .WithMany(b => b.CommentModels)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent multiple cascade paths
        }
    }
}
