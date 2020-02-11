using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Testlet.Models;

namespace Testlet.Helper
{
    public class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals([AllowNull] Item x, [AllowNull] Item y)
        {
            if(Object.Equals(x,null) || Object.Equals(y,null))
            {
                return false;
            }
            else
            {
                return x.ItemId == y.ItemId;
            }
        }

        public int GetHashCode([DisallowNull] Item obj)
        {
            return obj.ItemId.GetHashCode();
        }
    }
}
