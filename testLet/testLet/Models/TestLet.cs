using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestLet.Exceptions;
using TestLet.Helper;

namespace TestLet.Models
{
    public class Testlet
    {
        public string TestletId;
        private List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            //Verify id
            if (String.IsNullOrWhiteSpace(testletId))
            {
                throw new ArgumentException("testLetId must be filled");
            }
            //Verify items
            if (items.Count != 10)
            {
                throw new ArgumentException("items should have 10 element");
            }

            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            VerifyItems();

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

        private void VerifyItems()
        {
            if(Items.Any(_=> _ == null))
            {
                throw new IncorrectItemsException();
            }

            var itemComparer = new ItemComparer();
            if(Items.Distinct(itemComparer).Count()!= 10)
            {
                throw new IncorrectItemsException();
            }

            if(Items.Where(_=>_.ItemType == ItemTypeEnum.Pretest).Count() != 4)
            {
                throw new IncorrectItemsException();
            }
        }
    }
}
