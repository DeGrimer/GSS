using DataAccessLibrary;
using DataAccessLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GSS
{
    public partial class SuppliesWindow : Window
    {
        private readonly CollectionView _view;
        private readonly GoodsContext _goodsContext = new GoodsContext();
        private readonly List<Supplie> _supplies;

        public SuppliesWindow()
        {
            InitializeComponent();

            using (_goodsContext)
            {
                var goods = _goodsContext.goods.ToList();
                _supplies = _goodsContext.supplies.ToList();
            }

            SuppliesView.ItemsSource = _supplies;
            _view = (CollectionView) CollectionViewSource.GetDefaultView(SuppliesView.ItemsSource);
        }

        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            // каким-то образом выбрать товар и количество
            var good = new Goods();
            var amount = 10;


            var supply = new Supplie(good, amount);

            using (_goodsContext)
            {
                _goodsContext.supplies.Add(supply);
                _goodsContext.SaveChanges();
            }

            _supplies.Add(supply);
            _view.Refresh();
        }
    }
}