using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using testLet.Models;

namespace testLestUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Create_TestLet()
        {
            //Correct id correct items
            const string testId = "testId";
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

        private List<Item> correctItems = new List<Item>
        {
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