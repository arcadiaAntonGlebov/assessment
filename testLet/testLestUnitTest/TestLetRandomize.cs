using NUnit.Framework;
using System.Linq;
using TestLestUnitTest.Helper;
using TestLet.Exceptions;
using TestLet.Models;

namespace TestLestUnitTest
{
    [TestFixture]
    public class TestLetRandomize
    {
        const string TestId = "testId";

        [Test]
        public void CreateWithIncorrectCountItem()
        {
            // items should be 10
            Assert.Throws<IncorrectItemsException>(() => {
                var testLet = new Testlet(TestId, ItemHelper.CreateItems().Take(5).ToList());
                testLet.Randomize();
            });
            Assert.Throws<IncorrectItemsException>(() => { 
                var testLet = new Testlet(TestId, ItemHelper.CreateItems().Concat(ItemHelper.CreateItems()).ToList());
                testLet.Randomize();
            });
        }

        [Test]
        public void CreateWithSameItemID()
        {
            var currentItems = ItemHelper.CreateItems();
            currentItems[1].ItemId = "1";
            var testLet = new Testlet(TestId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CreateWithIncorrectPretestCount()
        {
            var currentItems = ItemHelper.CreateItems();
            currentItems[9].ItemType = ItemTypeEnum.Pretest;
            var testLet = new Testlet(TestId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CheckFirstTwoIsPretest()
        {
            var currentItems = ItemHelper.CreateItems();

            var testLet = new Testlet(TestId, currentItems);

            var randomizeItems = testLet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void CheckRandomItemsIsRandomAndDoNotDuplicate()
        {
            var currentItems = ItemHelper.CreateItems();

            var testLet = new Testlet(TestId, currentItems);

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

            var testLet = new Testlet(TestId, currentItems);

            var randomizeItems = testLet.Randomize();

            //items should be random
            var newRandomItems = testLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(ItemHelper.IntersectItems(randomizeItems, newRandomItems) < 10);

            var newTestLet = new Testlet(TestId, currentItems);
            newRandomItems = newTestLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(ItemHelper.IntersectItems(randomizeItems, newRandomItems) < 10);
        }
    }
}
