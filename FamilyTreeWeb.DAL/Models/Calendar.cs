

namespace FamilyTreeWeb.DAL.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
