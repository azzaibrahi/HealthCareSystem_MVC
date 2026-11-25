using System.ComponentModel.DataAnnotations;

namespace HealthCareSystem_MVC.ViewModels;

public class PatientViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(150, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}
