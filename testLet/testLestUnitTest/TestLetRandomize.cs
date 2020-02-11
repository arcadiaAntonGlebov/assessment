using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestLestUnitTest.Helper;
using TestLet.Exceptions;
using TestLet.Models;

namespace TestLestUnitTest
{
    [TestFixture]
    public class TestLetRandomize
    {
        const string testId = "testId";

        [Test]
        public void CreateWithSameItemID()
        {
            var currentItems = ItemHelper.CreateItems();
            currentItems[1].ItemId = "1";
            var testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CreateWithIncorrectPretestCount()
        {
            var currentItems = ItemHelper.CreateItems();
            currentItems[9].ItemType = ItemTypeEnum.Pretest;
            var testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CheckFirstTwoIsPretest()
        {
            var currentItems = ItemHelper.CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void CheckRandomItemsIsRandomAndDoNotDuplicate()
        {
            var currentItems = ItemHelper.CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            // no items repeat in randomize
            var distinct = randomizeItems.Distinct();
            Assert.IsTrue(distinct.Count() == 10);
            var itemsComparer = new ItemsComparer();
            distinct = randomizeItems.Distinct(itemsComparer);
            Assert.IsTrue(distinct.Count() == 10);
        }

        [Test]
        public void CheckRandomItemOnCallAndNewElemet()
        {
            var currentItems = ItemHelper.CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            //items should be random
            var newRandomItems = testLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(ItemHelper.IntersectItems(randomizeItems, newRandomItems) < 10);

            var newTestLet = new Testlet(testId, currentItems);
            newRandomItems = newTestLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(ItemHelper.IntersectItems(randomizeItems, newRandomItems) < 10);
        }

        

        
    }
}
