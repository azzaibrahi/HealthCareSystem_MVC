namespace HealthCareSystem_MVC.BLL.Interfaces;

/// <summary>One unit of work for all health-care repositories.</summary>
public interface IUnitOfWork
{
    IPatientRepository Patients { get; }

    IDoctorRepository Doctors { get; }

    IAppointmentRepository Appointments { get; }

    Task<int> SaveChangesAsync();
}
