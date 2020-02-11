using System.Collections.Generic;
using Testlet.Models;

namespace TestletUnitTest.Helper
{
    public static class TestData
    {
        public static List<Item> CreateItems()
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

        public static int IntersectItems(List<Item> list1, List<Item> list2)
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
    }
}
