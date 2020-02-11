using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestLet.Exceptions;
using TestLet.Models;

namespace TestLestUnitTest
{
    public class Tests
    {
        const string testId = "testId";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        //Check create test let positive ang negative scenario
        //I expect filled testletId and only 10 items no less no more
        public void Create_TestLet()
        {
            //Correct id correct items
            var correctItems = CreateItems();
            var testLet = new Testlet(testId, correctItems);
            Assert.AreEqual(testId, testLet.TestletId);

            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet("", correctItems); });
            Assert.Throws<ArgumentException>(() => { new Testlet(null, correctItems); });

            // items should be 10
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, correctItems.Take(5).ToList()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, correctItems.Concat(correctItems).ToList()); });

            Assert.Pass();
        }

        [Test]
        //Here we will create test let with some incorrect data
        // duplicate ItemId, incorrect count of pretest, or any item null
        public void Randomize_negative()
        {
            var currentItems = CreateItems();
            currentItems[1].ItemId = "1";
            var testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());

            currentItems = CreateItems();
            currentItems[9].ItemType = ItemTypeEnum.Pretest;
            testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());

            currentItems = CreateItems();
            currentItems[5] = null;
            testLet = new Testlet(testId, currentItems);
            Assert.Throws<IncorrectItemsException>(() => testLet.Randomize());
        }

        [Test]
        //Here we pass correct data and expect correct result of randomize
        // first two item always pretest
        // item should be randomize 
        public void Randomize_positive()
        {
            var currentItems = CreateItems();

            var testLet = new Testlet(testId, currentItems);

            var randomizeItems = testLet.Randomize();

            // first two item pretest
            Assert.IsTrue(randomizeItems[0].ItemType == ItemTypeEnum.Pretest);
            Assert.IsTrue(randomizeItems[1].ItemType == ItemTypeEnum.Pretest);

            // no items repeat in randomize
            var distinct = randomizeItems.Distinct();
            Assert.IsTrue(distinct.Count() == 10);
            var itemsComparer = new ItemsComparer();
            distinct = randomizeItems.Distinct(itemsComparer);
            Assert.IsTrue(distinct.Count() == 10);

            //items should be random
            var newRandomItems = testLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(intersectItems(randomizeItems, newRandomItems) < 10);

            var newTestLet = new Testlet(testId, currentItems);
            newRandomItems = newTestLet.Randomize();
            //we have limited items and it is possible that some items in same order but it should be less than 10
            Assert.IsTrue(intersectItems(randomizeItems, newRandomItems) < 10);
        }

        private List<Item> CreateItems()
        {
            return new List<Item> {
                new Item{ ItemId = "1", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId= "2", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "3", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId= "4", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "5", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId= "6", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "7", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId= "8", ItemType = ItemTypeEnum.Pretest},
                new Item{ ItemId = "9", ItemType = ItemTypeEnum.Operational},
                new Item{ ItemId = "10", ItemType = ItemTypeEnum.Operational},
            };
        }

        private int intersectItems(List<Item> list1, List<Item> list2)
        {
            int countOfIntersection = 0;
            for (int i=0; i < 10; i++)
            {
                if(list1[i].ItemId == list2[i].ItemId)
                {
                    countOfIntersection++;
                }
            }
            return countOfIntersection;
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