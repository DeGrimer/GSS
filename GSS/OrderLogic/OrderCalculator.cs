using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLibrary;
using DataAccessLibrary.models;

namespace GSS
{
    public class OrderCalculator
    {
        private const int SaleDayCount = 7;
        private const double OrderAmountMargin = 0.1;

        public static Order MakeOrder(List<Department> depts, List<Sale> sales, StorageParameters parameters)
        {
            // сбор всех товаров, которые нужно заказать, и их предполагаемого количества
            var goodsToRestock = new List<GoodBatch>();
            foreach (var dept in depts)
                goodsToRestock.AddRange(GetGoodsToRestock(dept, sales));

            // распределение товаров по складам
            if (AllStorageAvailable(parameters, goodsToRestock))
                return new Order(goodsToRestock);
            else
                return DistributeAvailableStorage(goodsToRestock);
        }

        private static Order DistributeAvailableStorage(List<GoodBatch> goodsToRestock)
        {
            goodsToRestock.Sort(new StorageRatingComparer());
            var goodsByStorage = goodsToRestock.GroupBy(g => g.Good.storage_kind);

            foreach (var group in goodsByStorage)
            {
                // заполняем каждый вид склада товарами, начиная от самых выгодных 
            }
            
            throw new NotImplementedException();
        }

        private static bool AllStorageAvailable(StorageParameters parameters, List<GoodBatch> goods)
        {
            var generalGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.General);
            var coldGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.Cold);
            var freezerGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.Freezer);
            return parameters[StorageRequirements.General] >= generalGoodAmounts &&
                   parameters[StorageRequirements.Cold] >= coldGoodAmounts &&
                   parameters[StorageRequirements.Freezer] >= freezerGoodAmounts;
        }

        private static int GetRequiredStorageAmount(List<GoodBatch> goods, StorageRequirements reqs)
        {
            return goods.Where(g => g.Good.storage_kind == reqs).Select(g => g.Amount).Sum();
        }

        // собираем все товары отдела, которые нужно заказать
        private static List<GoodBatch> GetGoodsToRestock(Department dept, List<Sale> allSales)
        {
            var toRestock = new List<GoodBatch>();
            var allRecentSales = allSales.Where(s => DateTime.Now.AddDays(-SaleDayCount) < s.DateSold).ToList();

            foreach (var article in dept.Goods)
                CheckAddToRestockList(article, allRecentSales, toRestock);

            return toRestock;
        }

        // вычисляем предполагаемое количество, если на складе не хватает - добавляем в список
        private static void CheckAddToRestockList(Goods good, List<Sale> allRecentSales, List<GoodBatch> toRestock)
        {
            var recentGoodSales = good.GetRecentSales(SaleDayCount);
            var expectedAmount = GetExpectedOrderAmount(recentGoodSales);

            var goodBatch = new GoodBatch(good);
            if (goodBatch.Good.AvailableAmount < expectedAmount)
            {
                goodBatch.Amount = expectedAmount - goodBatch.Good.AvailableAmount;
                good.Popularity = GetGoodPopularity(recentGoodSales, allRecentSales);
                toRestock.Add(goodBatch);
            }
        }

        private static double GetGoodPopularity(List<Sale> recentGoodSales, List<Sale> allRecentSales)
        {
            var itemSoldAmount = recentGoodSales.Sum(s => s.SoldAmount);
            var allSoldAmount = allRecentSales.Sum(s => s.SoldAmount);
            var result = (double) itemSoldAmount / allSoldAmount;
            return Math.Round(result, 2);
        }

        // среднее количество проданного товара + N %
        private static int GetExpectedOrderAmount(List<Sale> recentSales)
        {
            var avg = recentSales.Average(s => s.SoldAmount);
            var orderAmount = (int) Math.Ceiling(avg);
            var margin = (int) Math.Ceiling(orderAmount * OrderAmountMargin);
            return orderAmount + margin;
        }
    }
}