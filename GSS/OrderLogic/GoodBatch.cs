using DataAccessLibrary.models;

namespace GSS
{
    public class GoodBatch
    {
        public readonly Goods Good;
        public int Amount { get; set; }

        public double StorageRating => Amount * (double) Good.price * Good.Popularity * Good.StorageEase;

        public GoodBatch(Goods good, int amount)
        {
            Good = good;
            Amount = amount;
        }
    }
}