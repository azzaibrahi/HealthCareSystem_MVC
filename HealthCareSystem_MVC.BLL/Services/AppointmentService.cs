using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Services;

/// <inheritdoc />
public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result<Appointment>> CreateAsync(Appointment appointment)
    {
        var existing = await _unitOfWork.Appointments.GetAsync(
            appointment.PatientId,
            appointment.DoctorId,
            appointment.AppointmentDate);

        if (existing is not null)
            return Result<Appointment>.Failure("This appointment already exists.");

        await _unitOfWork.Appointments.CreateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();
        return Result<Appointment>.Success(appointment);
    }

    public async Task<Result<Appointment>> DeleteAsync(int patientId, int doctorId, DateTime appointmentDate)
    {
        var result = await GetByIdAsync(patientId, doctorId, appointmentDate);
        if (!result.Successeded || result.Data is null)
            return result;

        _unitOfWork.Appointments.Delete(result.Data);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Result<PaginatedList<Appointment>>> GetAllAsync(int pageIndex, int pageSize)
    {
        var appointments = await _unitOfWork.Appointments.GetAllAsync();
        return Result<PaginatedList<Appointment>>.Success(
            PaginatedList<Appointment>.Create(appointments, pageIndex, pageSize));
    }

    public async Task<Result<Appointment>> GetByIdAsync(int patientId, int doctorId, DateTime appointmentDate)
    {
        var appointment = await _unitOfWork.Appointments.GetAsync(patientId, doctorId, appointmentDate);
        return appointment is null
            ? Result<Appointment>.Failure("Appointment is not found.")
            : Result<Appointment>.Success(appointment);
    }

    public async Task<Result<Appointment>> UpdateAsync(Appointment appointment)
    {
        _unitOfWork.Appointments.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
        return Result<Appointment>.Success(appointment);
    }
}
