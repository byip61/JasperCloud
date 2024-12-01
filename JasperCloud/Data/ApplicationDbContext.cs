using JasperCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<User> Users { get; set; }
    public DbSet<Models.File> Files { get; set; }
    public DbSet<FileMetadata> FileMetadatas { get; set; }

    public ApplicationDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("JasperCloudDb"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}