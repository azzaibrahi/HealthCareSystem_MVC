using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

/// <summary>
/// Appointment repository. It is separate from <see cref="IGenericRepository{TEntity}"/>
/// because Appointment has a composite key and does not inherit <see cref="BaseEntity"/>.
/// </summary>
public interface IAppointmentRepository
{
    Task CreateAsync(Appointment appointment);

    void Update(Appointment appointment);

    void Delete(Appointment appointment);

    Task<Appointment?> GetAsync(int patientId, int doctorId, DateTime appointmentDate);

    Task<IEnumerable<Appointment>> GetAllAsync();

    Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId);

    Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId);
}
