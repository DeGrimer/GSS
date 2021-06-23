using DataAccessLibrary.models;

namespace GSS
{
    public class GoodBatch
    {
        public readonly Goods Good;
        public int Amount { get; set; }

        public GoodBatch(Goods good)
        {
            Good = good;
        }
    }
}