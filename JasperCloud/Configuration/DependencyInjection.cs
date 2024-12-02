using Azure.Storage.Blobs;
using JasperCloud.Data;
using JasperCloud.Repository;
using JasperCloud.Service;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Configuration;

public static class DependencyInjection
{
    /// <summary>
    /// Add infrastructure services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Configuration from builder.</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("JasperCloudDb")));

        services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("JasperCloudBlob")));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    /// <summary>
    /// Add repositories to scope.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        
        return services;
    }

    /// <summary>
    /// Add services to scope.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}