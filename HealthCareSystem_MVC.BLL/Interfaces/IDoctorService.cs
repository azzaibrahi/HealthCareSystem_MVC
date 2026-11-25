using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

public interface IDoctorService
{
    Task<Result<PaginatedList<Doctor>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize);

    Task<Result<Doctor>> GetByIdAsync(int id);

    Task<Result<Doctor>> CreateAsync(Doctor doctor);

    Task<Result<Doctor>> UpdateAsync(Doctor doctor);

    Task<Result<Doctor>> DeleteAsync(int id);
}
