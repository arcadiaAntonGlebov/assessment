using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testlet.Exceptions;
using Testlet.Models;

namespace Testlet.Helper
{
    public class RandomizeItem
    {

        public RandomizeItem()
        {
        }

        public void VerifyData(int? pretestCount,int? totalCount,List<Item> items)
        {
            if (items.Any(_ => _ == null))
            {
                throw new IncorrectItemsException("Items contain null element");
            }

            if (pretestCount.HasValue)
            {
                if (items.Where(_ => _.ItemType == ItemTypeEnum.Pretest).Count() != pretestCount)
                {
                    throw new IncorrectItemsException($"Items should contain {pretestCount} pretested elements");
                }
            }

            var itemComparer = new ItemComparer();

            if(items.Distinct(itemComparer).Count() != items.Count())
            {
                throw new IncorrectItemsException("Item shoud not contain duplicate elements");
            }

            if(totalCount.HasValue)
            {
                if(items.Count() != totalCount)
                {
                    throw new IncorrectItemsException($"Items should contain {totalCount} elements");
                }
            }
        }
    }
}
