using NUnit.Framework;
using System;
using TestlestUnitTest.Helper;

namespace TestLestUnitTest
{
    [TestFixture]
    public class TestLetCreate
    {
        const string TestId = "testId";

        [Test]
        public void Create()
        {
            var correctItems = TestData.CreateItems();
            var testLet = new Testlet.Models.Testlet(TestId, correctItems);
            Assert.AreEqual(TestId, testLet.TestletId);
        }

        [Test]
        public void CreateWithEmptyTestletId()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("", TestData.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet(null, TestData.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("    ", TestData.CreateItems()); });
        }
    }
}
