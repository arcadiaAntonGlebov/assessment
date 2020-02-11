using System;
using System.Collections.Generic;
using TestLet.Models;

namespace TestLestUnitTest.Helper
{
    public class ItemsComparer : IEqualityComparer<Item>
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
