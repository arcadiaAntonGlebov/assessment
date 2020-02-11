using System;
using System.Collections.Generic;
using System.Text;

namespace TestLet.Models
{
    public class Testlet
    {
        public string TestletId;
        private List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            return null;
        }
    }
}
