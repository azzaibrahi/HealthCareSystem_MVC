using HealthCareSystem_MVC.BLL.Implementations;
using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.BLL.Services;
using HealthCareSystem_MVC.DAL.Data;
using HealthCareSystem_MVC.Helper;
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

        #region Session 03

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion

        #region Session 04

        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        services.AddAutoMapper(config => config.AddMaps(typeof(HealthCareProfile).Assembly));

        #endregion

        return services;
    }
}
