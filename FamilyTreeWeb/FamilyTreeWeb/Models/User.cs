using Microsoft.AspNetCore.Identity;

namespace FamilyTreeWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
