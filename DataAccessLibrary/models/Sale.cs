using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.models
{
    public class Sale
    {
        public int id { get; set; }
        public int good_id { get; set; }
        public int good_count { get; set; }
        public DateTime date_sold { get; set; }
    }
}
