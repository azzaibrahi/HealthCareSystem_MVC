using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

public interface IPatientService
{
    Task<Result<PaginatedList<Patient>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize);

    Task<Result<Patient>> GetByIdAsync(int id);

    Task<Result<Patient>> CreateAsync(Patient patient);

    Task<Result<Patient>> UpdateAsync(Patient patient);

    Task<Result<Patient>> DeleteAsync(int id);
}
