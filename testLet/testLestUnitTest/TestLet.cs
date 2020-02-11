using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using testLet.Exceptions;
using testLet.Models;

namespace testLestUnitTest
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
    }
}