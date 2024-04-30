namespace FamilyTreeWeb.Context
{
    using FamilyTreeWeb.Models;
    using Microsoft.EntityFrameworkCore;

    public class FamilyTreeDbContext : DbContext
    {
        public FamilyTreeDbContext(DbContextOptions<FamilyTreeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Tree)
                .WithMany(t => t.People)
                .HasForeignKey(p => p.IdTree);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.PersonId1Navigation)
                .WithMany(p => p.RelationshipPersonId1Navigations)
                .HasForeignKey(r => r.PersonId1);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.PersonId2Navigation)
                .WithMany(p => p.RelationshipPersonId2Navigations)
                .HasForeignKey(r => r.PersonId2);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Tree)
                .WithMany(t => t.Permissions)
                .HasForeignKey(p => p.TreeId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.UserLoginNavigation)
                .WithMany(u => u.Permissions)
                .HasForeignKey(p => p.UserLogin);

            modelBuilder.Entity<Event>()
                .HasOne(sr => sr.Person)
                .WithMany(e => e.Events)
                .HasForeignKey(sr => sr.PersonId);

            modelBuilder.Entity<Photo>()
                .HasOne(sr => sr.Person)
                .WithMany(e => e.Photos)
                .HasForeignKey(sr => sr.PersonId);

        }
    }

}
