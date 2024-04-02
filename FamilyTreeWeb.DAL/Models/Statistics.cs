

namespace FamilyTreeWeb.DAL.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        public int GenealogyTreeId { get; set; }
        public GenealogyTree Tree { get; set; } = null!;
    }
}
