using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiAppDataContext _context = new SamuraiAppDataContext();
        public static async Task Main(string[] args)
        {
            var affectedLines = await QueryDatabaseStoreProcedure(5);

        }

        public static async Task InsertMultipleSamurais()
        {
            Samurai[] samurais = {
                new Samurai { Name = "Rojo" },
                new Samurai { Name = "Azul" },
                new Samurai { Name = "Amarillo" },
                new Samurai { Name = "Verde" },
            };

            _context.Samurais.AddRange(samurais);
            await _context.SaveChangesAsync();
        }
        public static async Task InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Rojo" };
            var clan = new Clan { ClanName = "Square" };

            _context.AddRange(samurai, clan);
            await _context.SaveChangesAsync();
        }
        public static async Task PrintSamurais(string text)
        {
            var samurais = await _context.Samurais.ToListAsync();

            Console.WriteLine($"Samurais - {text}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine($"Samurai: {samurai.Name}");
            }
        }
        public static async Task InsertBattle()
        {
            await _context.Battles.AddAsync(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }
        public static async Task QueryAndUpdate_Disconnected()
        {
            var battle = await _context.Battles.AsNoTracking().FirstOrDefaultAsync();
            battle.EndDate = new DateTime(1560, 06, 30);

            using var newContext = new SamuraiAppDataContext();
            newContext.Battles.Update(battle);
            await newContext.SaveChangesAsync();
        }
        public static async Task InsertingRelatedData()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you" }
                }
            };

            _context.Samurais.Add(samurai);
            await _context.SaveChangesAsync();
        }
        public static Task<List<Samurai>> EagerLoadSamuraiWithQuotes() =>
         _context.Samurais.Include(x => x.Quotes).ToListAsync();

        public static Task<List<object>> ProjectingRelatedData() =>
            _context.Samurais
                .Select(x => new { x.Name, x.Quotes.Count, x.Horse } as object)
                .ToListAsync();

        public static async Task LoadDataExplicity()
        {
            var samurai = _context.Samurais.Find(13);
            //Loading a collection use Collection
            await _context.Entry(samurai).Collection(x => x.Quotes).LoadAsync();

            //Loading a sigle entry use Reference
            await _context.Entry(samurai).Reference(x => x.Horse).LoadAsync();

            //NOTE: use the respective method depending on the kind of data to retrive
        }

        public static async Task GetManyToMany()
        {
            await _context.Samurais.Select(x => new
            {
                x.Name,
                Battles = x.SamuraiBattles.Select(y => y.Battle),
            })
            .ToListAsync();
        }

        public static Task<List<Samurai>> QueryUsingRawSql() =>
            _context.Samurais.FromSqlRaw("SELECT * FROM Samurais WHERE 1 = 1")
                .Where(x => x.Name.Length > 5)
                .Include(x => x.Quotes).ToListAsync();

        public static Task<List<Samurai>> QueryUsingSqlInterpolated() {
            var name = "Rojo";
            return _context.Samurais.FromSqlInterpolated($"SELECT * FROM Samurais WHERE Name = {name}")
                .Where(x => x.Name.Length > 5)
                .Include(x => x.Quotes).ToListAsync();
        }

        public static Task<List<Samurai>> QueryStoreProcedure()
        {
            var text = "Happy";
            return _context.Samurais.FromSqlRaw("EXEC dbo.SamuraisWhoSaidAWord {0}", text)
                .ToListAsync();
        }

        public static Task<int> QueryDatabaseStoreProcedure(int id) =>
            _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.DeleteQuotesForSamurai {id}");
        
    }
}
