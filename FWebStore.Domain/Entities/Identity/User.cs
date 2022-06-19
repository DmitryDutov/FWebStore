using Microsoft.AspNetCore.Identity;

namespace FWebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string? AboutMyself { get; set; }
    }
}

