namespace FamilyTreeWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Relationship
    {
        [Key]
        public int Id { get; set; }

        public int PersonId1 { get; set; }

        public int PersonId2 { get; set; }

        public string RelationshipType { get; set; } = null!;

        public virtual Person PersonId1Navigation { get; set; } = null!;

        public virtual Person PersonId2Navigation { get; set; } = null!;
    }
}
