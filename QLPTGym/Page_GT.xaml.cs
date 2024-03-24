using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using static QLPTGym.Page_GT;

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for Page_GT.xaml
    /// </summary>\
    /// 

    public partial class Page_GT : Page
    {
        private SQLiteConnection connection;
        private string databaseName = "D:/database/gym.db";
        public class Goitap
        {
            public int MaGT { get; set; }
            public string TenGT { get; set; }
            public string ThoiGian { get; set; }
            public int Gia { get; set; }
            public int GiamGia { get; set; }
            public string NgayBD { get; set; }
            public string NgayKT { get; set; }
            public float GiaCuoi
            {
                get
                {
                    // Nếu ngày bắt đầu hoặc ngày kết thúc bằng null thì không thể parse
                    // Có cả ngày BD và KT
                    if (NgayBD != null && NgayKT != null)
                    {
                        if (DateTime.Now >= DateTime.Parse(NgayBD)
                            && DateTime.Now <= DateTime.Parse(NgayKT))
                        {
                            return Gia - Gia * GiamGia / 100;
                        }
                    }
                    // Chỉ có ngày BD
                    if ((NgayBD != null && NgayKT == null))
                    {
                        if (DateTime.Now >= DateTime.Parse(NgayBD))
                        {
                            return Gia - Gia * GiamGia / 100;
                        }
                    }
                    // Chỉ có ngày KT
                    if (NgayBD == null && NgayKT != null)
                    {
                        if (DateTime.Now <= DateTime.Parse(NgayKT))
                        {
                            return Gia - Gia * GiamGia / 100;
                        }
                    }
                    // Không có cả ngày BD và KT
                    if (NgayBD == null && NgayKT == null)
                    {
                        return Gia - Gia * GiamGia / 100;
                    }


                    return Gia;

                }
            }
        }

        public Page_GT()
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

        private void LoadData()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM goitap", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Goitap> goitaps = new List<Goitap>();
            while (reader.Read())
            {
                goitaps.Add(new Goitap()
                {
                    MaGT = reader.GetInt32(0),
                    TenGT = reader.GetString(1),
                    ThoiGian = reader.GetString(2),
                    Gia = reader.GetInt32(3),
                    GiamGia = reader.GetInt32(4),
                    NgayBD = reader.IsDBNull(5) ? null : reader.GetString(5),
                    NgayKT = reader.IsDBNull(6) ? null : reader.GetString(6)
                });
            }

            myListView.ItemsSource = goitaps;


        }

        private void SortMaGT()
        {

        }

        private void UpdateGridLayout()
        {
            myListView.UpdateLayout();
            foreach (var column in myListView.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myListView.SelectedItem != null)
            {

                Goitap selectedGoitap = (Goitap)myListView.SelectedItem;
                txtTenGT.Text = selectedGoitap.TenGT;
                txtThoiGian.Text = selectedGoitap.ThoiGian.ToString();
                txtGia.Text = selectedGoitap.Gia.ToString();
                txtGiamGia.Text = selectedGoitap.GiamGia.ToString();

                if (selectedGoitap.NgayBD != null)
                {
                    datepick_NgayBD.SelectedDate = DateTime.Parse(selectedGoitap.NgayBD);
                }
                else
                {
                    datepick_NgayBD.SelectedDate = null;
                }
                if (selectedGoitap.NgayKT != null)
                {
                    datepick_NgayKT.SelectedDate = DateTime.Parse(selectedGoitap.NgayKT);
                }
                else
                {
                    datepick_NgayKT.SelectedDate = null;

                }

            }
        }

        private bool CheckInput()
        {
            if (txtTenGT.Text == "")
            {
                MessageBox.Show("Tên gói tập không được để trống"); return false;
            }
            if (txtGia.Text == "")
            {
                MessageBox.Show("Giá không được để trống"); return false;
            }
            if (txtThoiGian.Text == "")
            {
                MessageBox.Show("Thời gian không được để trống"); return false;
            }
            if (txtGiamGia.Text == "")
            {
                MessageBox.Show("Giảm giá không được để trống (từ 0 -> 100 phần trăm)"); return false;
            }
            return true;
        }

        private void Them(object sender, RoutedEventArgs e)
        {

            if (CheckInput())
            {
                string query = "INSERT INTO goitap(TenGoiTap, ThoiGianGoiTap, Gia, GiamGia, NgayBD, NgayKT) VALUES(@TenGT, @ThoiGian, @Gia, @GiamGia, @NgayBD, @NgayKT)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@TenGT", txtTenGT.Text);
                command.Parameters.AddWithValue("@ThoiGian", txtThoiGian.Text);
                command.Parameters.AddWithValue("@Gia", txtGia.Text);
                command.Parameters.AddWithValue("@GiamGia", txtGiamGia.Text);
                command.Parameters.AddWithValue("@NgayBD", datepick_NgayBD.SelectedDate);
                command.Parameters.AddWithValue("@NgayKT", datepick_NgayKT.SelectedDate);
                command.ExecuteNonQuery();

                ClearAllTextBox();
                LoadData();
            }
        }

        private void ClearAllTextBox()
        {
            txtTenGT.Text = "";
            txtThoiGian.Text = "";
            txtGia.Text = "";
            txtGiamGia.Text = "";
            datepick_NgayBD.SelectedDate = null;
            datepick_NgayKT.SelectedDate = null;
        }
        private void Sua(object sender, RoutedEventArgs e)
        {
            if (myListView.SelectedItem != null)
            {
                if (CheckInput())
                {
                    Goitap selectedGoitap = (Goitap)myListView.SelectedItem;
                    string query = "UPDATE goitap SET TenGoiTap = @TenGT, ThoiGianGoiTap = @ThoiGian, Gia = @Gia, GiamGia = @GiamGia, NgayBD = @NgayBD, NgayKT = @NgayKT WHERE MaGoiTap = @MaGT";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@MaGT", selectedGoitap.MaGT);
                    command.Parameters.AddWithValue("@TenGT", txtTenGT.Text);
                    command.Parameters.AddWithValue("@ThoiGian", txtThoiGian.Text);
                    command.Parameters.AddWithValue("@Gia", txtGia.Text);
                    command.Parameters.AddWithValue("@GiamGia", txtGiamGia.Text);
                    command.Parameters.AddWithValue("@NgayBD", datepick_NgayBD.SelectedDate);
                    command.Parameters.AddWithValue("@NgayKT", datepick_NgayKT.SelectedDate);
                    command.ExecuteNonQuery();

                    LoadData();
                    ClearAllTextBox();
                }
            }
            else
            {
                MessageBox.Show("Chọn gói tập cần sửa");
            }
        }
        private void Xoa(object sender, RoutedEventArgs e)
        {
            if (myListView.SelectedItem != null)
            {
                Goitap selectedGoitap = (Goitap)myListView.SelectedItem;
                string query = "DELETE FROM goitap WHERE MaGoiTap = @MaGT";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@MaGT", selectedGoitap.MaGT);
                command.ExecuteNonQuery();

                LoadData();
                ClearAllTextBox();
            }
            else
            {
                MessageBox.Show("Chọn gói tập cần xóa");
            }
        }

        private void TimKiem(object sender, RoutedEventArgs e)
        {
            string tenGT = txtTenGT.Text;
            string thoiGian = txtThoiGian.Text;
            string gia = txtGia.Text;
            string giamGia = txtGiamGia.Text;

            SQLiteCommand command = new SQLiteCommand("SELECT * FROM goitap", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Goitap> goitaps = new List<Goitap>();
            while (reader.Read())
            {
                goitaps.Add(new Goitap()
                {
                    MaGT = reader.GetInt32(0),
                    TenGT = reader.GetString(1),
                    ThoiGian = reader.GetString(2),
                    Gia = reader.GetInt32(3),
                    GiamGia = reader.GetInt32(4),
                    NgayBD = reader.IsDBNull(5) ? null : reader.GetString(5),
                    NgayKT = reader.IsDBNull(6) ? null : reader.GetString(6)
                });
            }

            // Kiểm tra nếu tất cả các TextBox đều rỗng
            if (string.IsNullOrWhiteSpace(tenGT) && string.IsNullOrWhiteSpace(thoiGian) && string.IsNullOrWhiteSpace(gia) && string.IsNullOrWhiteSpace(giamGia))
            {
                MessageBox.Show("Vui lòng nhập ít nhất một từ khóa tìm kiếm.");
                return;
            }

            List<Goitap> results = goitaps
                .Where(g =>
                    (string.IsNullOrWhiteSpace(tenGT) || g.TenGT.Contains(tenGT)) &&
                    (string.IsNullOrWhiteSpace(thoiGian) || g.ThoiGian.ToString().Contains(thoiGian)) &&
                    (string.IsNullOrWhiteSpace(gia) || g.Gia.ToString().Contains(gia)) &&
                    (string.IsNullOrWhiteSpace(giamGia) || g.GiamGia.ToString().Contains(giamGia))
                )
                .ToList();

            // Hiển thị kết quả tìm kiếm
            myListView.ItemsSource = results;
            if (results.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp.");
            }
            LoadData();
        }
        private void LamMoi(object sender, RoutedEventArgs e)
        {
            LoadData();
            ClearAllTextBox();
        }
    }
}