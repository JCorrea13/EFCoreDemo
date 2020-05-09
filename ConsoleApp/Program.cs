using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiAppDataContext _context = new SamuraiAppDataContext();
        public static async Task Main(string[] args)
        {
            
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
                StartDate =  DateTime.Now.AddDays(-1),
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
        
    }
}
