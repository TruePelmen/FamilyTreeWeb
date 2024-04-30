using System.ComponentModel.DataAnnotations;

namespace FamilyTreeWeb.Models
{
    public class User
    {
        [Key]
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
