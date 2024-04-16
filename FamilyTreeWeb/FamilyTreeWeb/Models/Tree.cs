namespace FamilyTreeWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tree
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int PrimaryPerson { get; set; }

        public string Type { get; set; } = null!;

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}
