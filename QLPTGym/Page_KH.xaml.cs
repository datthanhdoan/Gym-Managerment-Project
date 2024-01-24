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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for Page_KH.xaml
    /// </summary>
    public partial class Page_KH : Page
    {
        public Page_KH()
        {
            InitializeComponent();
            List<KhachHang> people = new List<KhachHang>
            {
            new KhachHang { hoTen = "Nguyễn Văn A", ngSinh = new DateTime(1990, 1, 15), gioiTinh = "Nam", sdt = "01234264518", goiTap = "Vip", thGianTap = "Còn hạn" },
            new KhachHang { hoTen = "Trần Thị B", ngSinh = new DateTime(1995, 5, 20), gioiTinh = "Nữ", sdt = "01236475284", goiTap = "Normal", thGianTap = "Hết hạn"},
            };
            myListView.ItemsSource = people;
        }
        public class KhachHang
        {
            public string hoTen { get; set; }
            public DateTime ngSinh { get; set; }
            public string gioiTinh { get; set; }
            public string sdt { get; set; }
            public string goiTap { get; set; }
            public string thGianTap { get; set; }

        }

    }
}
