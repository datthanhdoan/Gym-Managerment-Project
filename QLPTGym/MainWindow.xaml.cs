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
using System.Data.SQLite;
using Microsoft.SqlServer.Server;

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string databaseName = "D:/database/UserLogin.db";
        private SQLiteConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            connection.Open();
        }
        public void Btn_Dn_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from userLogin where UserName='" + this.username.Text + "' and Password='" + this.password.Password + "' ";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            cmd.ExecuteNonQuery();
            SQLiteDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            if (count == 1)
            {
                Main main = new Main();
                this.Visibility = Visibility.Hidden;
                main.Show();
            }
            if (count < 1)
            {
                MessageBox.Show("UserName hoặc Password không đúng");
            }
            
        }
        private void Window_Move(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
        public void Btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không?", "Xác nhận thoát",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}
