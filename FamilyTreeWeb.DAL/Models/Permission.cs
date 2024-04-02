

namespace FamilyTreeWeb.DAL.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int GenealogyTreeId { get; set; }
        public GenealogyTree GenealogyTree { get; set; } = null!;
    }
}
