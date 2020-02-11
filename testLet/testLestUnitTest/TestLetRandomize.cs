using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var currentItems = CreateItems();
            currentItems[1].ItemId = "1";
            var testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CreateWithIncorrectPretestCount()
        {
            var currentItems = CreateItems();
            currentItems[9].ItemType = ItemTypeEnum.Pretest;
            var testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        public void CheckFirstTwoIsPretest()
        {
            var currentItems = CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);
        }

        [Test]
        public void CheckRandomItemsIsRandomAndDoNotDuplicate()
        {
            var currentItems = CreateItems();

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
            var currentItems = CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            //items should be random
            var newRandomItems = testLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(intersectItems(randomizeItems, newRandomItems) < 10);

            var newTestLet = new Testlet(testId, currentItems);
            newRandomItems = newTestLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(intersectItems(randomizeItems, newRandomItems) < 10);
        }

        private int intersectItems(List<Item> list1, List<Item> list2)
        {
            int countOfIntersection = 0;
            for (int i = 0; i < 10; i++)
            {
                if (list1[i].ItemId == list2[i].ItemId)
                {
                    countOfIntersection++;
                }
            }
            return countOfIntersection;
        }

        private List<Item> CreateItems()
        {
            return new List<Item> {
                new Item{ ItemId = "1", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "2", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "3", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "4", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "5", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "6", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "7", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "8", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "9", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "10", ItemType = ItemTypeEnum.Operational},
            };
        }

        private class ItemsComparer : IEqualityComparer<Item>
        {
            public bool Equals(Item x, Item y)
            {
                if (Object.Equals(x, null) || Object.Equals(y, null))
                {
                    return false;
                }
                else
                {
                    return x.ItemId == y.ItemId;
                }
            }

            public int GetHashCode(Item obj)
            {
                return obj.ItemId.GetHashCode();
            }
        }
    }
}
