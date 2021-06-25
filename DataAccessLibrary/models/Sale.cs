using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.models
{
    public class Sale
    {
        public int id { get; set; }
        [Column("good_id")] public int GoodId { get; set; }

        [Column("good_count")] public int SoldAmount { get; set; }

        [Column("date_sold")] public DateTime DateSold { get; set; }
        public Goods Good { get; set; }

        public override string ToString()
        {
            return $"[{DateSold}] {Good}: {SoldAmount}";
        }
    }
}