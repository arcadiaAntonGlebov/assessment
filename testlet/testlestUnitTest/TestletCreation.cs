using NUnit.Framework;
using System;
using Testlet.Helper;
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
            var testLet = new Testlet.Models.Testlet(TestId, correctItems, new Randomization(2));
            Assert.AreEqual(TestId, testLet.TestletId);
        }

        [Test]
        public void CreationFailsWithEmptyTestletId()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("", TestData.CreateItems(), new Randomization(2)); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet(null, TestData.CreateItems(), new Randomization(2)); });
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet("    ", TestData.CreateItems(), new Randomization(2)); });
        }

        [Test]
        public void CreationFailsWhenItemsArgumentIsNul()
        {
            Assert.Throws<ArgumentException>(() => { new Testlet.Models.Testlet(TestId, null, new Randomization(2)); });
        }
    }
}
