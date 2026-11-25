using Microsoft.AspNetCore.Identity;

namespace HealthCareSystem_MVC.DAL.Models;

/// <summary>Identity account used by the MVC site.</summary>
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
