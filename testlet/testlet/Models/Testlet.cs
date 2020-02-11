using System;
using System.Collections.Generic;
using System.Linq;
using Testlet.Helper;

namespace Testlet.Models
{
    public class Testlet
    {
        public string TestletId { get; }
        private readonly List<Item> Items;
        private readonly Randomization randomizeItem;

        public Testlet(string testletId, List<Item> items)
        {
            //Verify id
            if (String.IsNullOrWhiteSpace(testletId))
            {
                throw new ArgumentException("argument must be filled",nameof(testletId));
            }

            if(Equals(items, null))
            {
                throw new ArgumentException("argument null", nameof(items));
            }

            randomizeItem = new Randomization();
            TestletId = testletId;
            Items = new List<Item>(items);
        }

        public List<Item> Randomize()
        {
            var result = randomizeItem.Randomize(2, Items);

            return result;
        }
    }
}
