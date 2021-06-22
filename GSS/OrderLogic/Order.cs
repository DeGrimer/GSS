using System;
using System.Collections.Generic;

namespace GSS
{
    public class Order
    {
        public List<GoodBatch> Goods;
        public DateTime DateCreated;

        public Order(List<GoodBatch> goods)
        {
            Goods = goods;
            DateCreated = DateTime.Today;
        }
    }
}