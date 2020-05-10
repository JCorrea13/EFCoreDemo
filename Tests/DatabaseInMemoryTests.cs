using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseInMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");

            using var context = new SamuraiTestDataContext(builder.Options);
            // IN MEMORY will handle the Delte and Creation of the DB; We don't need to care about that in most cases
            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();
            var samurai = new Samurai();
            context.Samurais.Add(samurai);

            Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
        }
    }
}
