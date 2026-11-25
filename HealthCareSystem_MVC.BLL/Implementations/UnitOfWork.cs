using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.DAL.Data;

namespace HealthCareSystem_MVC.BLL.Implementations;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Lazy<IPatientRepository> _patientRepository;
    private readonly Lazy<IDoctorRepository> _doctorRepository;
    private readonly Lazy<IAppointmentRepository> _appointmentRepository;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _patientRepository = new Lazy<IPatientRepository>(() => new PatientRepository(_dbContext));
        _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(_dbContext));
        _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(_dbContext));
    }

    public IPatientRepository Patients => _patientRepository.Value;

    public IDoctorRepository Doctors => _doctorRepository.Value;

    public IAppointmentRepository Appointments => _appointmentRepository.Value;

    public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
