
namespace FamilyTreeWeb.DAL.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string BirthPlace { get; set; }
        public string DeathPlace { get; set; }

        public ICollection<Event> Events { get; set; } = null!; 
        public ICollection<Photo> Photos { get; set; } = null!;
        public ICollection<SpecialRecord> SpecialRecords { get; set; } = null!;
        public ICollection<Relation> Relations { get; set; } = null!;
    }
}
