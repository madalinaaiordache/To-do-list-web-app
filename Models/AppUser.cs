
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public List<Task> Tasks { get; set; } = new List<Task>();
}