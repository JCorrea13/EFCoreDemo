using EfCore.Data;
using EfCore.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            using var context = new SamuraiTestDataContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var samurai = new Samurai();
            context.Samurais.Add(samurai);
            Debug.WriteLine($"Before Save: {samurai.Id}");

            context.SaveChanges();
            Debug.WriteLine($"After Save: {samurai.Id}");

            Assert.AreNotEqual(0, samurai.Id);
        }
    }
}
