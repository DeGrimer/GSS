using System;
using System.Collections.Generic;

namespace GSS
{
    public class Order
    {
        public readonly List<GoodBatch> Goods;
        public readonly DateTime DateCreated;

        public Order()
        {
            Goods = new List<GoodBatch>();
            DateCreated = DateTime.Today;
        }
    }
}