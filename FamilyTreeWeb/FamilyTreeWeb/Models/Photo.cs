namespace FamilyTreeWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Photo
    {
        [Key]
        public int Id { get; set; }

        public string FilePath { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly? Date { get; set; }

        public string? Place { get; set; }

        public bool? MainPhoto { get; set; }

        public int PersonId { get; set; }

        public virtual Person? Person { get; set; }

        public int EventId { get; set; }

        public virtual Event? Event { get; set; }
    }
}
