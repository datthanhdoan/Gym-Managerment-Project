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

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            
        }
        public void BtnNV(object sender, RoutedEventArgs e)
        {
            MainUI.Content = new Page_NV();
        }
        public void BtnKH(object sender, RoutedEventArgs e)
        {
            MainUI.Content = new Page_KH();
        }
        public void BtnGT(object sender, RoutedEventArgs e)
        {
            MainUI.Content = new Page_GT();
        }
        public void BtnDT(object sender, RoutedEventArgs e)
        {
            MainUI.Content = new Page_DT();
        }
        public void BtnDong(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
