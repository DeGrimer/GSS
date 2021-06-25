using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.models
{
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public int general_storage { get; set; }
        public int cold_storage { get; set; }
        public int freezer_storage { get; set; }

        public List<Goods> Goods { get; set; } = new List<Goods>();

        public override string ToString()
        {
            return name;
        }
    }
}