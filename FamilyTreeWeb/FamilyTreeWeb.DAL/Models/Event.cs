

namespace FamilyTreeWeb.DAL.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public ICollection<Photo> Photos { get; set; } = null!;
        public ICollection<SpecialRecord> SpecialRecords { get; set; } = null!;
    }
}
