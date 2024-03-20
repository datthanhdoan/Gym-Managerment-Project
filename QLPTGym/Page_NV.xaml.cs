using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace QLPTGym
{
     public partial class Page_NV : Page
    {
        private SQLiteConnection connection;
        private string databaseName = "D:/QLPTGym/gym.db";


        public Page_NV()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadData();

        }

        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            connection.Open();
        }

        private void LoadData()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT* FROM nhanvien", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Nhanvien> nv = new List<Nhanvien>();
            while (reader.Read())
            {
                nv.Add(new Nhanvien()
                {
                    id = reader.GetInt32(0),
                    ten_nv = reader.GetString(1),
                    ngay_sinh = reader.GetString(2),
                    gioi_tinh = reader.GetString(3),
                    sdt = reader.GetString(4),
                    chuc_vu = reader.GetString(5),
                    muc_luong = reader.GetString(6),
                });
            }
            myNhanvien.ItemsSource = nv;
            reader.Close();

        }



        public void btnReset(object sender, RoutedEventArgs e)
        {
            nvTen.Text = "";
            gtNam.IsChecked = false;
            gtNu.IsChecked = false;
            nvNgaysinh.Text = "";
            nvChucvu.Text = "";
            nvMucluong.Text = "";
            nvSdt.Text = "";
        }
        public void clear()
        {
            nvTen.Text = "";
            gtNam.IsChecked = false;
            gtNu.IsChecked = false;
            nvSdt.Text = "";
            nvNgaysinh.Text = "";
            nvChucvu.Text = "";
            nvMucluong.Text = "";
            search.Text = "";
        }
        public void btnThem(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nvTen.Text) && !string.IsNullOrEmpty(nvChucvu.Text) && !string.IsNullOrEmpty(nvMucluong.Text))
            {
                string gioiTinh = "";
                if (gtNam.IsChecked == true)
                {
                    gioiTinh = "Nam";
                }
                else if (gtNu.IsChecked == true)
                {
                    gioiTinh = "Nữ";
                }

                string query = "INSERT INTO nhanvien(ten_nv, ngay_sinh, gioi_tinh, sdt, chuc_vu, muc_luong) VALUES(@ten_nv, @ngay_sinh, @gioi_tinh, @sdt, @chuc_vu, @muc_luong)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ten_nv", nvTen.Text);
                command.Parameters.AddWithValue("@ngay_sinh", nvNgaysinh.Text);
                command.Parameters.AddWithValue("@gioi_tinh", gioiTinh);
                command.Parameters.AddWithValue("@sdt", nvSdt.Text);
                command.Parameters.AddWithValue("@chuc_vu", nvChucvu.Text);
                command.Parameters.AddWithValue("@muc_luong", nvMucluong.Text);
                command.ExecuteNonQuery();
                LoadData();
                clear();
            }
            else
            {
                MessageBox.Show("Chưa đủ thông tin");
            }
        }
        public void btnSua(object sender, RoutedEventArgs e)
        {
            if (myNhanvien.SelectedItem != null)
            {
                string gioiTinh = "";
                if (gtNam.IsChecked == true)
                {
                    gioiTinh = "Nam";
                }
                else if (gtNu.IsChecked == true)
                {
                    gioiTinh = "Nữ";
                }
                Nhanvien selectedNV = (Nhanvien)myNhanvien.SelectedItem;
                string query = "UPDATE nhanvien SET ten_nv = @ten_nv, ngay_sinh = @ngay_sinh, gioi_tinh = @gioi_tinh, sdt = @sdt, chuc_vu = @chuc_vu, muc_luong = @muc_luong WHERE id = @id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@ten_nv", nvTen.Text);
                command.Parameters.AddWithValue("@ngay_sinh", nvNgaysinh.Text);
                command.Parameters.AddWithValue("@gioi_tinh", gioiTinh);
                command.Parameters.AddWithValue("@sdt", nvSdt.Text);
                command.Parameters.AddWithValue("@chuc_vu", nvChucvu.Text);
                command.Parameters.AddWithValue("@muc_luong", nvMucluong.Text);
                command.Parameters.AddWithValue("@id", selectedNV.id);
                command.ExecuteNonQuery();
                LoadData();
                clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên");
            }

        }
        public void btnXoa(object sender, RoutedEventArgs e)
        {
            if (myNhanvien.SelectedItem != null)
            {
                Nhanvien selectedNV = (Nhanvien)myNhanvien.SelectedItem;
                string query = "DELETE FROM nhanvien WHERE id = @id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@id", selectedNV.id);
                command.ExecuteNonQuery();
                LoadData();
                clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên");
            }
        }
        public class Nhanvien
        {
            public int id { get; set; }
            public string ten_nv { get; set; }
            public string ngay_sinh { get; set; }
            public string gioi_tinh { get; set; }
            public string sdt { get; set; }
            public string chuc_vu { get; set; }
            public string muc_luong { get; set; }

        }
        private void myNhanvien_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (myNhanvien.SelectedItem != null)
            {
                Nhanvien selectedNV = (Nhanvien)myNhanvien.SelectedItem;
                nvTen.Text = selectedNV.ten_nv;
                nvNgaysinh.Text = selectedNV.ngay_sinh;
                if(selectedNV.gioi_tinh == "Nam"){gtNam.IsChecked = true;}
                else if(selectedNV.gioi_tinh == "Nữ"){gtNu.IsChecked = true; }
                nvSdt.Text = selectedNV.sdt;
                nvChucvu.Text = selectedNV.chuc_vu;
                nvMucluong.Text = selectedNV.muc_luong;
            }
        }
        private void nvSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = search.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(myNhanvien.ItemsSource);

            if (!string.IsNullOrEmpty(keyword))
            {
                view.Filter = item =>
                {
                    Nhanvien nhanVien = item as Nhanvien;
                    return nhanVien != null && (nhanVien.ten_nv.ToLower().Contains(keyword) ||
                                                  nhanVien.ngay_sinh.ToLower().Contains(keyword) ||
                                                  nhanVien.gioi_tinh.ToLower().Contains(keyword) ||
                                                  nhanVien.sdt.ToLower().Contains(keyword) ||
                                                  nhanVien.chuc_vu.ToLower().Contains(keyword) ||
                                                  nhanVien.muc_luong.ToLower().Contains(keyword)); 
                };
            }
            else
            {
                view.Filter = null;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton checkedRadioButton = sender as RadioButton;
            if (checkedRadioButton != null)
            {
                string gioiTinh = checkedRadioButton.Content.ToString();
            }
        }
    }
}
