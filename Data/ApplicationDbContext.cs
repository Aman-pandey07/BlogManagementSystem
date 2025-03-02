using BlogManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;

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
            base.OnModelCreating(modelBuilder);

            // When a Blog is deleted, all related Comments should also be deleted
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.BlogModel)
                .WithMany(b => b.CommentModels)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // When a User is deleted, all their Blogs should be deleted
            modelBuilder.Entity<BlogModel>()
                .HasOne(b => b.UserModel)
                .WithMany(u => u.BlogModels)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // When a User is deleted, all their Comments should be deleted
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.UserModel)
                .WithMany(u => u.CommentModels)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // When a Category is deleted, all related Blogs should be deleted
            modelBuilder.Entity<BlogModel>()
                .HasOne(b => b.CategoryModel)
                .WithMany(c => c.BlogModel)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}