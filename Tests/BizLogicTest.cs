using ConsoleApp;
using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

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
    }
}
