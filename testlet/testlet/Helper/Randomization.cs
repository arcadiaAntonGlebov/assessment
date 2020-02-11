using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testlet.Models;

namespace Testlet.Helper
{
    public class Randomization
    {
        private readonly int startsWithNPretests;

        public Randomization(int startsWithNPretests)
        {
            this.startsWithNPretests = startsWithNPretests;
        }

        public List<Item> Randomize(List<Item> items)
        {
            VerifyData(startsWithNPretests, items);
            var randomOrder = RandomizeOrder(items);

            if (startsWithNPretests > 0)
            {
                foreach (var item in randomOrder.Where(_ => _.Item.ItemType == ItemTypeEnum.Pretest).Take(startsWithNPretests))
                {
                    item.Order = -1;
                }
            }

            return randomOrder
                .OrderBy(_ => _.Order)
                .Select(_ => _.Item)
                .ToList();
        }

        private void VerifyData(int minimumPretestCount,List<Item> items)
        {
            if (items.Where(_ => _.ItemType == ItemTypeEnum.Pretest).Count() < minimumPretestCount)
            {
                throw new InvalidOperationException($"Items should contain {minimumPretestCount} pretested elements");
            }
        }

        private List<OrderedItem<Item>> RandomizeOrder(List<Item> items)
        {
            var random = new Random();
            var count = items.Count;
            return items.ConvertAll(_ => new OrderedItem<Item> { Item = _, Order = random.Next(count) });
        }

        private class OrderedItem<TItem>
        {
            public TItem Item { get; set; }

            public int Order { get; set; }
        }
    }
}
