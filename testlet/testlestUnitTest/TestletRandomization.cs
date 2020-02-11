using NUnit.Framework;
using TestletUnitTests.Helper;
using Testlet.Models;
using System;
using Testlet.Helper;

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
            var testlet = new Testlet.Models.Testlet(TestId, currentItems, new Randomization(2));
            Assert.Throws<InvalidOperationException>(() => testlet.Randomize());
        }

        [Test]
        public void CheckFirstTwoItemsArePretest()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems, new Randomization(2));

            var randomizeItems = testlet.Randomize();

            // first two items are pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void ItemsRandomizeDifferentlyEachCall()
        {
            var currentItems = TestData.CreateItems();

            var testlet = new Testlet.Models.Testlet(TestId, currentItems, new Randomization(2));

            var randomizeItems = testlet.Randomize();

            //items should be randomized
            var newRandomItems = testlet.Randomize();
            //we have limited items and it is possible that some items are in the same order but hopefully not all of them
            Assert.IsTrue(TestData.CountIntersectedItems(randomizeItems, newRandomItems) < randomizeItems.Count);
        }
    }
}
