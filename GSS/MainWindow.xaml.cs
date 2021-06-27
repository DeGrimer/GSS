using DataAccessLibrary;
using GSS.StorageLogic;
using System.IO;
using System.Linq;
using System.Windows;

namespace GSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string OrderDirectoryPath = @".\orders";

        public MainWindow()
        {
            if (!Directory.Exists(OrderDirectoryPath))
            {
                Directory.CreateDirectory(OrderDirectoryPath);
            }

            InitializeComponent();
            using (var db = new GoodsContext())
            {
                var goods = db.goods.ToList();
                var sales = db.sales.ToList();
                var departments = db.departments.ToList();
                //var supplies = db.supplies.ToList();
            }
            var list = new ListAvailableGoods();
        }

        private void GoodsListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sw = new StorageWindow();
            sw.Show();
        }

        private void OrderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ow = new OrderWindow(OrderDirectoryPath);
            ow.Show();
        }

        private void SupplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sw = new SuppliesWindow();
            sw.Show();
        }

        private void SaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sw = new SalesWindow();
            sw.Show();
        }
    }
}