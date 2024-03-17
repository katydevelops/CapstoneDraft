using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CapstoneDraft.Models;

namespace CapstoneDraft.Data
{
    // Safety Net uses SQLite with EntityFramework with ASP.NET's Identity API due to the extensive built-in methods and functionality 
    public class  CapstoneContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public static async Task Initialize(CapstoneContext databaseConnection, UserManager<User> userManager)
    }
}
