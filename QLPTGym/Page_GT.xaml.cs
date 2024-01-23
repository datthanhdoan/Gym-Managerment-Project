using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static QLPTGym.Page_GT;

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for Page_GT.xaml
    /// </summary>\
    /// 
    public class GymViewModel
    {
        public ObservableCollection<GymPackages> Packages { get; set; }

        public GymViewModel()
        {
            Packages = new ObservableCollection<GymPackages>
            {
                new GymPackages { TenGoiTap = "normal", ThoiGian = "1 tháng", GiaTien = 100000 },
                new GymPackages { TenGoiTap = "sliver", ThoiGian = "3 tháng", GiaTien = 200000 },
                new GymPackages { TenGoiTap = "vip", ThoiGian = "6 tháng", GiaTien = 300000 }
            };
        }
    }
    public partial class Page_GT : Page
    {
        public Page_GT()
        {
            InitializeComponent();
            DataContext = new GymViewModel();
        }
        public class GymPackages
        {
            public string TenGoiTap { get; set; }
            public string ThoiGian { get; set; }
            public double GiaTien { get; set; }
        }
    }
}
