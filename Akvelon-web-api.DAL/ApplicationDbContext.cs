using Akvelon_web_api.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Akvelon_web_api.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
}