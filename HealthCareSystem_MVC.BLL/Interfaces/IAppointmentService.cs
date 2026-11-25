using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

public interface IAppointmentService
{
    Task<Result<PaginatedList<Appointment>>> GetAllAsync(int pageIndex, int pageSize);

    Task<Result<Appointment>> GetByIdAsync(int patientId, int doctorId, DateTime appointmentDate);

    Task<Result<Appointment>> CreateAsync(Appointment appointment);

    Task<Result<Appointment>> UpdateAsync(Appointment appointment);

    Task<Result<Appointment>> DeleteAsync(int patientId, int doctorId, DateTime appointmentDate);
}
