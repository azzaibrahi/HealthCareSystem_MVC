using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.DAL.Data;
using HealthCareSystem_MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem_MVC.BLL.Implementations;

/// <inheritdoc />
public class AppointmentRepository : IAppointmentRepository
{
    private readonly DbSet<Appointment> _dbSet;

    public AppointmentRepository(ApplicationDbContext dbContext)
        => _dbSet = dbContext.Set<Appointment>();

    public async Task CreateAsync(Appointment appointment)
        => await _dbSet.AddAsync(appointment);

    public void Delete(Appointment appointment)
        => _dbSet.Remove(appointment);

    public async Task<Appointment?> GetAsync(int patientId, int doctorId, DateTime appointmentDate)
        => await _dbSet
            .Include(appointment => appointment.Patient)
            .Include(appointment => appointment.Doctor)
            .FirstOrDefaultAsync(appointment =>
                appointment.PatientId == patientId &&
                appointment.DoctorId == doctorId &&
                appointment.AppointmentDate == appointmentDate);

    public async Task<IEnumerable<Appointment>> GetAllAsync()
        => await _dbSet
            .Include(appointment => appointment.Patient)
            .Include(appointment => appointment.Doctor)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId)
        => await _dbSet
            .Include(appointment => appointment.Patient)
            .Include(appointment => appointment.Doctor)
            .Where(appointment => appointment.PatientId == patientId)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId)
        => await _dbSet
            .Include(appointment => appointment.Patient)
            .Include(appointment => appointment.Doctor)
            .Where(appointment => appointment.DoctorId == doctorId)
            .ToListAsync();

    public void Update(Appointment appointment)
        => _dbSet.Update(appointment);
}
