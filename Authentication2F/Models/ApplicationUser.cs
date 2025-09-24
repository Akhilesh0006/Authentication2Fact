using Microsoft.AspNetCore.Identity;

namespace Authentication2F.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
