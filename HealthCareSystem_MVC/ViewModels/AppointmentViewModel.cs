using System.ComponentModel.DataAnnotations;
using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.ViewModels;

public class AppointmentViewModel
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime AppointmentDate { get; set; }

    public Patient? Patient { get; set; }

    public Doctor? Doctor { get; set; }
}
