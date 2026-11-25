using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.DAL.Data;
using HealthCareSystem_MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem_MVC.BLL.Implementations;

/// <inheritdoc />
public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(string name)
        => await _dbSet
            .Where(patient => patient.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

    public async Task<IEnumerable<Patient>> GetAllWithAppointmentsAsync()
        => await _dbSet
            .Include(patient => patient.Appointments)
            .ToListAsync();
}
