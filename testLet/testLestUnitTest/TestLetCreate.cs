using NUnit.Framework;
using System;
using System.Linq;
using TestLestUnitTest.Helper;
using TestLet.Models;

namespace TestLestUnitTest
{
    [TestFixture]
    public class TestLetCreate
    {
        const string TestId = "testId";

        [Test]
        public void Create()
        {
            var correctItems = ItemHelper.CreateItems();
            var testLet = new Testlet(TestId, correctItems);
            Assert.AreEqual(TestId, testLet.TestletId);
        }

        [Test]
        public void CreateWithEmpty()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet("", ItemHelper.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(null, ItemHelper.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet("    ", ItemHelper.CreateItems()); });
        }
    }
}
