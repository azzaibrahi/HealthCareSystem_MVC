namespace HealthCareSystem_MVC.DAL.Models;

/// <summary>A patient who can book appointments with doctors.</summary>
public class Patient : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
