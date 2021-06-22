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
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GSSDB.mdf;Integrated Security=True;Connect Timeout=30")
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