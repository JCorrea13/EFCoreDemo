using EfCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext context = new SamuraiContext();
        static async Task Main(string[] args)
        {
            await GetSamurais("Before Add:");
            await AddSamurai();
            await GetSamurais("After Add:");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static async Task GetSamurais(string text)
        {
            var samurais = await context.Samurais.ToListAsync();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static async Task AddSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };
            context.Samurais.Add(samurai);
            await context.SaveChangesAsync();
        }
    }
}
