using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CapstoneDraft.Data
{
    // Safety Net uses SQLite with EntityFramework with ASP.NET's Identity API due to the extensive built-in methods and functionality 
    public class  CapstoneContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
    }
}
