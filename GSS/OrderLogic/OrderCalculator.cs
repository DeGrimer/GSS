using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLibrary.models;

namespace GSS
{
    public class OrderCalculator
    {
        private const int SaleDayCount = 7;
        private const double OrderAmountMargin = 0.1;

        public static Order MakeOrder(List<Department> depts, List<Sale> sales)
        {
            // сбор всех товаров, которые нужно заказать, и их предполагаемого количества
            var goodsToRestock = new List<GoodBatch>();
            foreach (var dept in depts)
                goodsToRestock.AddRange(GetGoodsToRestock(dept, sales));

            // todo придумать, в каком классе вычисляются объемы трех видов хранилищ и как их передать
            var gen = 100;
            var cold = 100;
            var freezer = 100;

            // распределение товаров по складам
            if (AllStorageAvailable(gen, cold, freezer, goodsToRestock))
                return new Order(goodsToRestock);
            else
                return DistributeAvailableStorage(goodsToRestock);
        }

        private static Order DistributeAvailableStorage(List<GoodBatch> goodsToRestock)
        {
            // sort goodBatch list by desc rating

            // find genStorage, coldStorage, freezerStorage

            // do something to fill storage
            throw new NotImplementedException();
        }

        private static bool AllStorageAvailable(int genStorage, int coldStorage, int freezerStorage,
            List<GoodBatch> goods)
        {
            var generalGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.General);
            var coldGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.Cold);
            var freezerGoodAmounts = GetRequiredStorageAmount(goods, StorageRequirements.Freezer);
            return genStorage >= generalGoodAmounts && coldStorage >= coldGoodAmounts &&
                   freezerStorage >= freezerGoodAmounts;
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
        private static void CheckAddToRestockList(Goods article, List<Sale> allRecentSales, List<GoodBatch> toRestock)
        {
            var recentGoodSales = GetRecentGoodSales(article);
            var expectedAmount = GetExpectedOrderAmount(recentGoodSales);

            var goodBatch = new GoodBatch(article, expectedAmount);
            if (goodBatch.Good.AvailableAmount < goodBatch.Amount)
            {
                article.Popularity = GetGoodPopularity(recentGoodSales, allRecentSales);
                toRestock.Add(goodBatch);
            }
        }

        // todo maybe move method to Goods.cs
        private static double GetGoodPopularity(List<Sale> goodSales, List<Sale> allSales)
        {
            var itemSoldAmount = goodSales.Sum(s => s.SoldAmount);
            var allSoldAmount = allSales.Sum(s => s.SoldAmount);
            var p = (double) itemSoldAmount / allSoldAmount;
            return Math.Round(p, 2);
        }

        // продажи за последние N дней
        private static List<Sale> GetRecentGoodSales(Goods good)
        {
            return good.Sales
                .Where(s => s.Good == good && DateTime.Now.AddDays(-SaleDayCount) < s.DateSold)
                .ToList();
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