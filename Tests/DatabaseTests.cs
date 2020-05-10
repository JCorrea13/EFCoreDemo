using EfCore.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            using var context = new SamuraiTestDataContext();
        }
    }
}
