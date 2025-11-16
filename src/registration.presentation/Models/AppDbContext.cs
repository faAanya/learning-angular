using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

class AppDbContext : IdentityDbContext
{
    DbSet<AppUser> AppUsers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}