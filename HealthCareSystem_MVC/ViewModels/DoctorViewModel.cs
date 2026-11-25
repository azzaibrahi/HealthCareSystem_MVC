using System.ComponentModel.DataAnnotations;

namespace HealthCareSystem_MVC.ViewModels;

public class DoctorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(150, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialization is required.")]
    [StringLength(150, MinimumLength = 3)]
    public string Specialization { get; set; } = string.Empty;
}
