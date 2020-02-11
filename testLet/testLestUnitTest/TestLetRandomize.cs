using NUnit.Framework;
using System.Linq;
using TestletUnitTest.Helper;
using Testlet.Exceptions;
using Testlet.Helper;
using Testlet.Models;

namespace TestletUnitTest
{
    [TestFixture]
    public class TestletRandomize
    {
        const string TestId = "testId";

        [Test]
        public void CreateWithIncorrectCountItem()
        {
            // items should be 10
            Assert.Throws<IncorrectItemsException>(() => {
                var testlet = new Testlet.Models.Testlet(TestId, TestData.CreateItems().Take(5).ToList());
                testlet.Randomize();
            });
            Assert.Throws<IncorrectItemsException>(() => { 
                var testlet = new Testlet.Models.Testlet(TestId, TestData.CreateItems().Concat(TestData.CreateItems()).ToList());
                testlet.Randomize();
            });
        }

        [Test]
        public void CreateWithSameItemID()
        {
            var currentItems = TestData.CreateItems();
            currentItems[1].ItemId = "1";
            var testlet = new Testlet.Models.Testlet(TestId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testlet.Randomize());
        }

        [Test]
        public void CreateWithIncorrectPretestCount()
        {
            var currentItems = TestData.CreateItems();
            currentItems[9].ItemType = ItemTypeEnum.Pretest;
            var testlet = new Testlet.Models.Testlet(TestId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testlet.Randomize());
        }

        [Test]
        public void CheckFirstTwoIsPretest()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems);

            var randomizeItems = testlet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void CheckRandomItemsIsRandomAndDoNotDuplicate()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems);

            var randomizeItems = testlet.Randomize();

            // no items repeat in randomize
            var distinct = randomizeItems.Distinct();
            Assert.IsTrue(distinct.Count() == 10);
            var itemsComparer = new ItemComparer();
            distinct = randomizeItems.Distinct(itemsComparer);
            Assert.IsTrue(distinct.Count() == 10);
        }

        [Test]
        public void CheckRandomItemOnCallAndNewElemet()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems);

            var randomizeItems = testlet.Randomize();

            //items should be random
            var newRandomItems = testlet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(TestData.IntersectItems(randomizeItems, newRandomItems) < 10);

            var newtestlet = new Testlet.Models.Testlet(TestId, currentItems);
            newRandomItems = newtestlet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(TestData.IntersectItems(randomizeItems, newRandomItems) < 10);
        }
    }
}
