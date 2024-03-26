using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;

namespace QLPTGym
{
    public partial class Page_DT : Page
    {
        private SQLiteConnection connection;
        private string databaseName = "D:/database/gym.db";

        public Page_DT()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadData();
            CalculateRevenue();
        }

        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source= {databaseName} ;Version=3;");
            connection.Open();
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
                    tienphaitra = reader.GetInt32(5)
                });
            }
            myHoadon.ItemsSource = hoadons;
            reader.Close();

            int totalCustomers = hoadons.Count;

            int currentCustomers = hoadons.Count(h => !string.IsNullOrEmpty(h.ngayDK));

            tbTotalCustomers.Text = $"Tổng số khách đã đăng ký: {totalCustomers}";
            tbCurrentCustomers.Text = $"Số khách hàng hiện đang đăng ký: {currentCustomers}";
        }

        private void CalculateRevenue()
        {
            List<Hoadon> hoadons = (List<Hoadon>)myHoadon.ItemsSource;
            if (hoadons != null && hoadons.Any())
            {
                int totalRevenue = hoadons.Sum(h => h.tienphaitra);
                tbTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue} VNĐ";
            }
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
