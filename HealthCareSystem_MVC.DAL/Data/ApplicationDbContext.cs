using HealthCareSystem_MVC.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem_MVC.DAL.Data;

/// <summary>
/// Identity and health-care data in one database, as in the MVC reference project.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    public DbSet<Doctor> Doctors => Set<Doctor>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Patient>(entity =>
        {
            entity.Property(patient => patient.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasMany(patient => patient.Appointments)
                .WithOne(appointment => appointment.Patient)
                .HasForeignKey(appointment => appointment.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Doctor>(entity =>
        {
            entity.Property(doctor => doctor.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(doctor => doctor.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasMany(doctor => doctor.Appointments)
                .WithOne(appointment => appointment.Doctor)
                .HasForeignKey(appointment => appointment.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Appointment>(entity =>
        {
            entity.HasKey(appointment => new
            {
                appointment.PatientId,
                appointment.DoctorId,
                appointment.AppointmentDate
            });

            entity.Property(appointment => appointment.AppointmentDate)
                .IsRequired();

            // The composite key starts with PatientId; this index serves doctor lookups.
            entity.HasIndex(appointment => appointment.DoctorId);
        });
    }
}
