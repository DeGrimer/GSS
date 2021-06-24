using DataAccessLibrary;
using DataAccessLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.StorageLogic
{
    public class StorageCalculate
    {
        public static StorageParameters CalculateStorageParameters(List<Department> departments)
        {
            var gen = departments.Sum(s => 
            {
                var amount = s.Goods.Where(z => z.storage_kind == StorageRequirements.General).Sum(z => z.AvailableAmount);
                return s.general_storage - amount;
            });
            var frez = departments.Sum(s =>
            {
                var amount = s.Goods.Where(z => z.storage_kind == StorageRequirements.Freezer).Sum(z => z.AvailableAmount);
                return s.freezer_storage - amount;
            });
            var cold = departments.Sum(s => {
                var amount = s.Goods.Where(z => z.storage_kind == StorageRequirements.Cold).Sum(z => z.AvailableAmount);
                return s.cold_storage - amount;
            });
            return new StorageParameters(gen, cold, frez);
        }
    }
}
