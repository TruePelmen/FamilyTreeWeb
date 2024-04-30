namespace FamilyTreeWeb.Models
{
#nullable enable

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Person
    {
        [Key]
        public int Id { get; set; }

        public string Gender { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MaidenName { get; set; }

        public string? FirstName { get; set; }

        public string? OtherNameVariants { get; set; }

        public DateOnly? BirthDate { get; set; }

        public DateOnly? DeathDate { get; set; }

        public string? BirthPlace { get; set; }

        public string? DeathPlace { get; set; }

        public int IdTree { get; set; }

        public virtual Tree Tree { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public virtual ICollection<Relationship> RelationshipPersonId1Navigations { get; set; } = new List<Relationship>();

        public virtual ICollection<Relationship> RelationshipPersonId2Navigations { get; set; } = new List<Relationship>();

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public string PossessiPveronoun
        {
            get
            {
                return (Gender == "male") ? "Його" : "Її";
            }
        }

        [NotMapped]
        public List<Person> Siblings { get; set; } = new List<Person>();

        [NotMapped]
        public List<Person> Children { get; set; } = new List<Person>();

        [NotMapped]
        public Person? Father { get; set; }

        [NotMapped]
        public Person? Mother { get; set; }

        [NotMapped]
        public Person? Spouse { get; set; }

        [NotMapped]
        public Photo? MainPhoto
        {
            get
            {
                return Photos.FirstOrDefault(p => p.MainPhoto == true);
            }
        }
    }
}
