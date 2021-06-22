using DataAccessLibrary;
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
            var optionsBuilder = new DbContextOptionsBuilder<GoodsContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Server=.\SQLEXPRESS;Database=GSSDB;Trusted_Connection=True;")
                    .Options;
            using (GoodsContext db = new GoodsContext(options))
            {
                var goods = db.goods.ToList();
                var sales = db.sales.ToList();
                var departments = db.departments.ToList();
                //var supplies = db.supplies.ToList();
            }
        }
    }
}