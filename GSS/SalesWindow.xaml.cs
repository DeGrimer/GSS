using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DataAccessLibrary;
using DataAccessLibrary.models;

namespace GSS
{
    public partial class SalesWindow : Window
    {
        private readonly GoodsContext _goodsContext = new GoodsContext();
        private List<Sale> _sales;
        private readonly CollectionView _view;

        public SalesWindow()
        {
            InitializeComponent();

            using (_goodsContext)
            {
                var goods = _goodsContext.goods.ToList();
                _sales = _goodsContext.sales.ToList();
            }

            SalesView.ItemsSource = _sales;
            _view = (CollectionView) CollectionViewSource.GetDefaultView(SalesView.ItemsSource);
        }

        private void AddSaleButton_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}