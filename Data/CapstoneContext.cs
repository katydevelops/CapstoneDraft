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
        public static async Task Initialize(CapstoneContext databaseConnection, UserManager<UserModel> userManager)
        {
            databaseConnection.Database.Migrate();
        }
    }
}
