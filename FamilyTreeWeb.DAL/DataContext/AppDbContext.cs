using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FamilyTreeWeb.DAL.Models;

namespace FamilyTreeWeb.DAL.DataContext
{
    public class AppDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<SpecialRecord> SpecialRecords { get; set; }
        public DbSet<GenealogyTree> GenealogyTrees { get; set; }
        public DbSet<Relation> Relations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Relations)
                .WithMany()
                .UsingEntity(j => j.ToTable("PersonRelations"));
            modelBuilder.Entity<Relation>()
               .HasOne(r => r.RelatedPerson1)
               .WithMany()
               .HasForeignKey(r => r.RelatedPerson1Id);
            modelBuilder.Entity<Relation>()
                .HasOne(r => r.RelatedPerson2)
                .WithMany()
                .HasForeignKey(r => r.RelatedPerson2Id);
        }

    }
}
