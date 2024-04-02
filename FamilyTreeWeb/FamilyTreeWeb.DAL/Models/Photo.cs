
namespace FamilyTreeWeb.DAL.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
