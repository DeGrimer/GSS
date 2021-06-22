using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.models
{
    public class Supplie
    {
        public int id { get; set; }
        public int good_id {get; set;}
        public DateTime date_supplied { get; set; }
        public DateTime date_sold { get; set; }
        public int remaining_qty { get; set; }

    }
}
