namespace FamilyTreeWeb.Models
{
    using System.ComponentModel.DataAnnotations;
    public partial class Permission
    {
        [Key]
        public int Id { get; set; }

        public string UserLogin { get; set; } = null!;

        public int TreeId { get; set; }

        public string AccessType { get; set; } = null!;

        public virtual Tree Tree { get; set; } = null!;

        public virtual User UserLoginNavigation { get; set; } = null!;
    }
}
