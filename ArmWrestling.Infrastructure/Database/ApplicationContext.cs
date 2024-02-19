using ArmWrestling.Domain.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=armwrestling.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Competitions)
                .WithMany(competition => competition.Categories)
                .UsingEntity(j => j.ToTable(nameof(CategoryInCompetition)));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryGroup> CategoryGroups { get; set; }
        public DbSet<CategoryInCompetition> CategoryInCompetitions { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Duel> Duels { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ResultPerson> ResultPersons { get; set; }
        public DbSet<ResultTeam> ResultTeams { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
