namespace FamilyTreeWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SpecialRecord
    {
        [Key]
        public int Id { get; set; }

        public string RecordType { get; set; } = null!;

        public int? HouseNumber { get; set; }

        public string Record { get; set; } = null!;

        public int EventId { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
