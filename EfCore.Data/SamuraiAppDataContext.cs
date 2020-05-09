using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EfCore.Model;
using EFCore.Model;

namespace EfCore.Data
{
    public partial class SamuraiAppDataContext : DbContext
    {

        public DbSet<Clan> Clans { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(ConsoleLoggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(x => new { x.SamuraiId, x.BattleId });
            modelBuilder.Entity<Horse>().ToTable("Horses");
            modelBuilder.Entity<SamuraiBattleStat>().HasNoKey().ToView("SamuraiBattleStats");
        }

    }
}
