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
            
            var samurai = new Samurai { Name = "Marisol" };
            Context.Samurais.Add(samurai);
            await Context.SaveChangesAsync();
            
            await PrintSamurais("After Insert ..");
            Console.ReadKey();
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
