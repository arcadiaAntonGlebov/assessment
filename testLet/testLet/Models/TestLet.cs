using System;
using System.Collections.Generic;
using System.Linq;
using Testlet.Exceptions;
using Testlet.Helper;

namespace Testlet.Models
{
    public class Testlet
    {
        public string TestletId { get; }
        private readonly List<Item> Items;
        private readonly RandomizeItem randomizeItem;

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

            randomizeItem = new RandomizeItem();
            TestletId = testletId;
            Items = new List<Item>(items);
        }

        public List<Item> Randomize()
        {
            randomizeItem.VerifyData(4, 10, Items);

            var random = new Random();
            var temp = new List<Item>(Items);
            var result = new List<Item>(10);

            //get all 4 pretest element to put in begin
            var pretested = temp.Where(_ => _.ItemType == ItemTypeEnum.Pretest).ToList();

            //random select first two pretest element and remove it from items left
            var item = pretested[random.Next(4)];
            result.Add(item);
            pretested.Remove(item);
            item = pretested[random.Next(3)];
            result.Add(item);
            result.ForEach(_ => temp.Remove(_));

            //randomly fill 7 from 8 items and after put last item to end
            for(int i= 0; i < 7; i++)
            {
                item = temp[random.Next(8 - i)];
                result.Add(item);
                temp.Remove(item);
            }
            result.Add(temp[0]);

            return result;
        }
    }
}
