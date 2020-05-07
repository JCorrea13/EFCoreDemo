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
            await InsertMultipleSamurais();
            await PrintSamurais("After Insert ..");
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
