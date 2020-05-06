using EfCore.Model;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data
{
    public partial class SamuraiAppDataContext : DbContext
    {

        public DbSet<Clan> Clans { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(x => new { x.SamuraiId, x.BattleId });
            modelBuilder.Entity<Horse>().ToTable("Horses");
        }

    }
}
