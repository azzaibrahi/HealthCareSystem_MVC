using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Services;

/// <inheritdoc />
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result<Doctor>> CreateAsync(Doctor doctor)
    {
        await _unitOfWork.Doctors.CreateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
        return Result<Doctor>.Success(doctor);
    }

    public async Task<Result<Doctor>> DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);
        if (!result.Successeded || result.Data is null)
            return result;

        _unitOfWork.Doctors.Delete(result.Data);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Result<PaginatedList<Doctor>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize)
    {
        var doctors = string.IsNullOrWhiteSpace(searchValue)
            ? await _unitOfWork.Doctors.GetAllAsync()
            : await _unitOfWork.Doctors.GetAllAsync(searchValue);

        return Result<PaginatedList<Doctor>>.Success(
            PaginatedList<Doctor>.Create(doctors, pageIndex, pageSize));
    }

    public async Task<Result<Doctor>> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetAsync(id);
        return doctor is null
            ? Result<Doctor>.Failure("Doctor is not found.")
            : Result<Doctor>.Success(doctor);
    }

    public async Task<Result<Doctor>> UpdateAsync(Doctor doctor)
    {
        _unitOfWork.Doctors.Update(doctor);
        await _unitOfWork.SaveChangesAsync();
        return Result<Doctor>.Success(doctor);
    }
}
