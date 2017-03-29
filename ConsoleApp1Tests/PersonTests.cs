using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void Person_ToString_Is_Not_Null()
        {
            Person peron = new Person();
            bool isNullOrEmpty = string.IsNullOrEmpty(peron.GetAddress());
            Assert.IsFalse(isNullOrEmpty);
        }
    }
}
