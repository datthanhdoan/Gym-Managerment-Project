using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for Page_DT.xaml
    /// </summary>
    public partial class Page_DT : Page
    {
        private SQLiteConnection connection;
        private string databaseName = "D:/database/gym.db";
        public Page_DT()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadData();
        }
        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source= {databaseName} ;Version=3;");
            connection.Open();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void LoadData()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM hoadon", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Hoadon> hoadons = new List<Hoadon>();
            while (reader.Read())
            {
                hoadons.Add(new Hoadon()
                {
                    id = reader.GetInt32(0),
                    tenkh = reader.GetString(1),
                    sdt = reader.GetString(2),
                    goitap = reader.GetString(3),
                    ngayDK = reader.GetString(4),
                    tienphaitra = reader.GetInt32(5),

                });
            }
            myHoadon.ItemsSource = hoadons;
            reader.Close();


        }
        public class Hoadon
        {
            public int id { get; set; }
            public string tenkh { get; set; }
            public string sdt { get; set; }
            public string goitap { get; set; }
            public string ngayDK { get; set; }
            public int tienphaitra { get; set; }
        }

    }
}
