using HealthCareSystem_MVC.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCareSystem_MVC.Extensions;

/// <summary>
/// Registers the MVC application services. More regions are added session by session.
/// </summary>
public static class ApplicationServiceExtension
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Session 01

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        #endregion

        return services;
    }
}
