using NUnit.Framework;

using Testlet.Helper;
using Testlet.Models;

namespace TestletUnitTest
{
    [TestFixture]
    public class TestItemComparer
    {
        [Test]
        public void CompareOneItem()
        {
            var item = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var comparer = new ItemComparer();

            Assert.IsTrue(comparer.Equals(item, item));
        }

        [Test]
        public void CompareItemWithEqualId()
        {
            var item = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var item2 = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };

            var comparer = new ItemComparer();

            Assert.IsTrue(comparer.Equals(item, item2));
        }

        [Test]
        public void CompareItemWithDifferentId()
        {
            var item = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var item2 = new Item { ItemId = "ItemId2", ItemType = ItemTypeEnum.Pretest };
            var comparer = new ItemComparer();

            Assert.IsFalse(comparer.Equals(item, item2));
        }

        [Test]
        public void CompareNullAndWithNull()
        {
            var item = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var comparer = new ItemComparer();

            Assert.IsFalse(comparer.Equals(null, null));
            Assert.IsFalse(comparer.Equals(item, null));
            Assert.IsFalse(comparer.Equals(null, item));
        }

        [Test]
        public void ComparerGetHashCode()
        {
            var item = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var item2 = new Item { ItemId = "ItemId", ItemType = ItemTypeEnum.Pretest };
            var comparer = new ItemComparer();

            Assert.AreEqual(comparer.GetHashCode(item), comparer.GetHashCode(item2));
        }
    }
}
