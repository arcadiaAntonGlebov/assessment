using NUnit.Framework;
using System;
using TestletUnitTests.Helper;

namespace TestletUnitTests
{
    [TestFixture]
    public class TestletCreation
    {
        const string TestId = "testId";

        [Test]
        public void CreateAndCheckTestletId()
        {
            var correctItems = TestData.CreateItems();
            var testLet = new Testlet.Models.Testlet(TestId, correctItems);
            Assert.AreEqual(TestId, testLet.TestletId);
        }

        [Test]
        public void CreationFailsWithEmptyTestletId()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("", TestData.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet(null, TestData.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("    ", TestData.CreateItems()); });
        }

        [Test]
        public void CreationFailsWhenItemsArgumentIsNul()
        {
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet(TestId, null); });
        }
    }
}
