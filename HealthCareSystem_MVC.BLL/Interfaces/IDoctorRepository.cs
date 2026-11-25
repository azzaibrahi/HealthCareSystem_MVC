using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

/// <summary>Doctor repository with the health-care search and include variants.</summary>
public interface IDoctorRepository : IGenericRepository<Doctor>
{
    Task<IEnumerable<Doctor>> GetAllAsync(string name);

    Task<IEnumerable<Doctor>> GetAllWithAppointmentsAsync();
}
