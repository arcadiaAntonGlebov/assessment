using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var correctItems = CreateItems();
            var testLet = new Testlet(testId, correctItems);
            Assert.AreEqual(testId, testLet.TestletId);
        }

        [Test]
        public void CreateWithEmpty()
        {
            //id should be filled
            Assert.Throws<ArgumentException>(() => { new Testlet("", CreateItems()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(null, CreateItems()); });
        }

        [Test]
        public void CreateIncorrectItems()
        {
            // items should be 10
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, CreateItems().Take(5).ToList()); });
            Assert.Throws<ArgumentException>(() => { new Testlet(testId, CreateItems().Concat(CreateItems()).ToList()); });
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
    }
}
