using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using w61905_PWO1.Models;

namespace w61905_PWO1.Data;

public class AnnouncementContext : IdentityDbContext<User>
{
    public AnnouncementContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Announcement> Announcements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole {Name = "Member", NormalizedName = "MEMBER"},
                new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"}
            );
    }
}