namespace HealthCareSystem_MVC.DAL.Models;

/// <summary>
/// Join entity between <see cref="Patient"/> and <see cref="Doctor"/>.
/// Its composite key allows repeated meetings on different dates but blocks an exact duplicate.
/// </summary>
public class Appointment
{
    public int PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }
}
