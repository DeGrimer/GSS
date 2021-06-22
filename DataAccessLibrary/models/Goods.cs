using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GSS;

namespace DataAccessLibrary.models
{
    public class Goods
    {
        public int id { get; set; }
        [Column("dept_id")] public int DepartmentId { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public StorageRequirements storage_kind { get; set; }
        public int expiration_days { get; set; }

        public Department Department { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<Supplie> Supplies { get; set; } = new List<Supplie>();

        public const double HighPopularityThreshold = 0.25;
        public const int LongExpirationDaysCount = 90;

        [NotMapped] public double Popularity { get; set; }
        [NotMapped] public double StorageEase => GetStorageEase();
        [NotMapped] public int AvailableAmount => Supplies.Count(s => !s.IsExpired);

        private double GetStorageEase()
        {
            if (Popularity < HighPopularityThreshold)
                return expiration_days < LongExpirationDaysCount ? 0.6 : 0.8;
            return expiration_days < LongExpirationDaysCount ? 1.0 : 0.9;
        }
    }
}