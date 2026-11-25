using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

/// <summary>Patient repository with the health-care search and include variants.</summary>
public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<IEnumerable<Patient>> GetAllAsync(string name);

    Task<IEnumerable<Patient>> GetAllWithAppointmentsAsync();
}
