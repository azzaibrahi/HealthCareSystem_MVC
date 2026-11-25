using AutoMapper;

namespace HealthCareSystem_MVC.Helper;

/// <summary>AutoMapper maps between the health-care entities and their MVC view models.</summary>
public class HealthCareProfile : Profile
{
    public HealthCareProfile()
    {
        CreateMap<Patient, PatientViewModel>().ReverseMap();
        CreateMap<Doctor, DoctorViewModel>().ReverseMap();
        CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
    }
}
