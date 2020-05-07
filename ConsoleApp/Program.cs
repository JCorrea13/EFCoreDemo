using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiAppDataContext Context = new SamuraiAppDataContext();
        public static async Task Main(string[] args)
        {
            await PrintSamurais("Before Insert ..");
            await InsertVariousTypes();
            await PrintSamurais("After Insert ..");

            var samurai = Context.Samurais.Find(2);
            Console.Write("Id 2: ");
            Console.WriteLine(samurai.Name);
            Console.ReadKey();
        }

        public static async Task InsertMultipleSamurais()
        {
            Samurai[] samurais = {
                new Samurai { Name = "Rojo" },
                new Samurai { Name = "Azul" },
                new Samurai { Name = "Amarillo" },
                new Samurai { Name = "Verde" },
            };

            Context.Samurais.AddRange(samurais);
            await Context.SaveChangesAsync();
        }

        public static async Task InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Rojo" };
            var clan = new Clan { ClanName = "Square" };

            Context.AddRange(samurai, clan);
            await Context.SaveChangesAsync();
        }

        public static async Task PrintSamurais(string text)
        {
            var samurais = await Context.Samurais.ToListAsync();

            Console.WriteLine($"Samurais - {text}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine($"Samurai: {samurai.Name}");
            }
        }
    }
}
