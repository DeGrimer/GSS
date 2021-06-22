using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.models
{
    public class Goods
    {
        public int id { get; set; }
        public int dept_id { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public int storage_kind { get; set; }
        public int expiration_days { get; set; }
    }
}
