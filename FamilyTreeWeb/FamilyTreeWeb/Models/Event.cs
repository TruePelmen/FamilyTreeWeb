namespace FamilyTreeWeb.Models
{
#nullable enable

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Event
    {
        [Key]
        public int Id { get; set; }

        public string EventType { get; set; } = null!;

        public DateOnly? EventDate { get; set; }

        public string? EventPlace { get; set; }

        public int PersonId { get; set; }

        public string? Description { get; set; }

        public int? Age { get; set; }

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<SpecialRecord> SpecialRecords { get; set; } = new List<SpecialRecord>();

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        [NotMapped]
        public string Type
        {
            get 
            { 
                switch (EventType)
                {
                    case "birth":
                        return "Народження";
                    case "death":
                        return "Смерть";
                    case "marriage":
                        return "Весілля";
                    default:
                        return "Невідомо";
                }
            }

        }
    }
}