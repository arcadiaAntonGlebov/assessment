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
            return null;
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
