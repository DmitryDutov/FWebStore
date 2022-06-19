using Microsoft.AspNetCore.Identity;

namespace FWebStore.Domain.Entities.Identity;

public class Role : IdentityRole
{
    public string? Description { get; set; }
}

