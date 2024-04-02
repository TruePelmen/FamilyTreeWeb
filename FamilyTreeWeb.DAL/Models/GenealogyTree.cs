

namespace FamilyTreeWeb.DAL.Models
{
    public class GenealogyTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Person> Persons { get; set; } = null!;
        public ICollection<Permission> Permissions { get; set; } = null!;
    }
}
