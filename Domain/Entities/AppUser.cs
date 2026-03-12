using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }

    public ICollection<Company> Companies { get; set; }
}