using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.BLL.Models;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Services;

/// <inheritdoc />
public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result<Patient>> CreateAsync(Patient patient)
    {
        await _unitOfWork.Patients.CreateAsync(patient);
        await _unitOfWork.SaveChangesAsync();
        return Result<Patient>.Success(patient);
    }

    public async Task<Result<Patient>> DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);
        if (!result.Successeded || result.Data is null)
            return result;

        _unitOfWork.Patients.Delete(result.Data);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Result<PaginatedList<Patient>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize)
    {
        var patients = string.IsNullOrWhiteSpace(searchValue)
            ? await _unitOfWork.Patients.GetAllAsync()
            : await _unitOfWork.Patients.GetAllAsync(searchValue);

        return Result<PaginatedList<Patient>>.Success(
            PaginatedList<Patient>.Create(patients, pageIndex, pageSize));
    }

    public async Task<Result<Patient>> GetByIdAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetAsync(id);
        return patient is null
            ? Result<Patient>.Failure("Patient is not found.")
            : Result<Patient>.Success(patient);
    }

    public async Task<Result<Patient>> UpdateAsync(Patient patient)
    {
        _unitOfWork.Patients.Update(patient);
        await _unitOfWork.SaveChangesAsync();
        return Result<Patient>.Success(patient);
    }
}
