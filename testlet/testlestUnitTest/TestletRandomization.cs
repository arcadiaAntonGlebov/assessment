using NUnit.Framework;
using TestletUnitTests.Helper;
using Testlet.Models;
using System;

namespace TestletUnitTests
{
    [TestFixture]
    public class TestletRandomization
    {
        const string TestId = "testId";

        [Test]
        public void FailsWithLessPretestCountItems()
        {
            var currentItems = TestData.CreateItems();
            currentItems[1].ItemType = ItemTypeEnum.Operational;
            currentItems[3].ItemType = ItemTypeEnum.Operational;
            currentItems[5].ItemType = ItemTypeEnum.Operational;
            var testlet = new Testlet.Models.Testlet(TestId, currentItems);
            Assert.Throws<InvalidOperationException>(() => testlet.Randomize());
        }

        [Test]
        public void CheckFirstTwoItemsArePretest()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems);

            var randomizeItems = testlet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void ItemsRandomizeDifferentlyEachCall()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems);

            var randomizeItems = testlet.Randomize();

            //items should be randomize
            var newRandomItems = testlet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(TestData.CountIntersectedItems(randomizeItems, newRandomItems) < randomizeItems.Count);
        }
    }
}
