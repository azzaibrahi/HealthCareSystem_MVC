using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.DAL.Data;
using HealthCareSystem_MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem_MVC.BLL.Implementations;

/// <inheritdoc />
public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync(string name)
        => await _dbSet
            .Where(doctor => doctor.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

    public async Task<IEnumerable<Doctor>> GetAllWithAppointmentsAsync()
        => await _dbSet
            .Include(doctor => doctor.Appointments)
            .ToListAsync();
}
