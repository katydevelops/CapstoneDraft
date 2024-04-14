using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CapstoneDraft.Models;

namespace CapstoneDraft.Data
{
    // Safety Net uses SQLite with EntityFramework with ASP.NET's Identity API due to the extensive built-in methods and functionality 
    public class  CapstoneContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        // Register the Entity Framework database tables based on the Safety Net User, Post and Comment models
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        // Needed to include constructor that accepts DbContextOptions in order to properly register SQLite as underlying EF database that will be used in Safety Net
        public CapstoneContext(DbContextOptions<CapstoneContext> options) : base(options) { }


        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Override the Entity Framework ModelBuilder to ensure that the email and username stay unique throughout the application
            builder.Entity<UserModel>().HasIndex(unique => unique.Email).IsUnique();
            builder.Entity<UserModel>().HasIndex(unique => unique.UserName).IsUnique();
        }
    }
}

testtesttesttesttesttesttesttesttest