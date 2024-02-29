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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Btn_Dn_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }
        private void Window_Move(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}
