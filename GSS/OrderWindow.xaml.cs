using DataAccessLibrary;
using GSS.StorageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GSS
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
            using(var db = new GoodsContext())
            {
                var goods = db.goods.ToList();
                var sales = db.sales.ToList();
                var supplies = db.supplies.ToList();
                var depts = db.departments.ToList();
                var storageParams = StorageCalculate.CalculateStorageParameters(depts);
                var order = OrderCalculator.MakeOrder(depts, sales, storageParams);
            }
        }
    }
}
