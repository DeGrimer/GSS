using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.models
{
    public class Supplie
    {
        public int id { get; set; }
        [Column("good_id")] public int GoodId { get; set; }
        public DateTime date_supplied { get; set; }
        public DateTime date_sold { get; set; }
        public int remaining_qty { get; set; }

        public Goods Good { get; set; }
        
        [NotMapped] public bool IsExpired => date_supplied.AddDays(Good.expiration_days) < DateTime.Now;
    }
}