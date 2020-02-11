using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestLestUnitTest.Helper;
using TestLet.Models;

namespace TestLestUnitTest
{
    [TestFixture]
    public class TestLetCreate
    {
        const string testId = "testId";

        [Test]
        public void Create()
        {
            var correctItems = ItemHelper.CreateItems();
            var testLet = new Testlet(testId, correctItems);
            Assert.AreEqual(testId, testLet.TestletId);
        }

        [Test]
        public void CreateWithEmpty()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet("", ItemHelper.CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(null, ItemHelper.CreateItems()); });
        }

        [Test]
        public void CreateIncorrectItems()
        {
            // items should be 10
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, ItemHelper.CreateItems().Take(5).ToList()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, ItemHelper.CreateItems().Concat(ItemHelper.CreateItems()).ToList()); });
        }
    }
}
