using JasperCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.File> Files { get; set; }
    public DbSet<FileMetadata> FileMetadatas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}