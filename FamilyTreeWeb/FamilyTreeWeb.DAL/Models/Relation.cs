

namespace FamilyTreeWeb.DAL.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int RelatedPerson1Id { get; set; }
        public Person RelatedPerson1 { get; set; } = null!;

        public int RelatedPerson2Id { get; set; }
        public Person RelatedPerson2 { get; set; } = null!;
    }
}
