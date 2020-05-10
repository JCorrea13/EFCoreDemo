using ConsoleApp;
using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BizLogicTest
    {
        [TestMethod]
        public void InsertMultipleSamuraisTest()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using var context = new SamuraiTestDataContext(builder.Options);

            var bizLogic = new BusinessLogic(context);
            var nameList = new string[] { "1", "2", "3" };

            var result = bizLogic.AddMultipleSamurais(nameList).GetAwaiter().GetResult();
            Assert.AreEqual(nameList.Length, result);
        }

        [TestMethod]
        public void CanInsertSigleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (var context = new SamuraiTestDataContext(builder.Options))
            {
                var bizLogic = new BusinessLogic(context);
                bizLogic.InsertNewSamurai(new Samurai());
            }

            using var context2 = new SamuraiTestDataContext(builder.Options);
            Assert.AreEqual(1, context2.Samurais.CountAsync().GetAwaiter().GetResult());
        }
    }
}
