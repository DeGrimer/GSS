using DataAccessLibrary;
using GSS.StorageLogic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
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
            var ow = new OrderWindow();
            ow.Show();
        }
    }
}