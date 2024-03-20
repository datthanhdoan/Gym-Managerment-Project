﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SQLite;
using System.ComponentModel;

namespace QLPTGym
{
    /// <summary>
    /// Interaction logic for Page_KH.xaml
    /// </summary>
    public partial class Page_KH : Page
    {
        private SQLiteConnection connection;
        private string databaseName = "D:/Gym-Project/gym.db";


        public Page_KH()
        {
            InitializeComponent();
            ConnectToDatabase();
            UpdateAllValidities();
            //LoadData();

        }

        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            connection.Open();
        }

        private void LoadData()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT* FROM khachhang", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<KhachHang> kh = new List<KhachHang>();
            while (reader.Read())
            {
                kh.Add(new KhachHang()
                {
                    id = reader.GetInt32(0),
                    hoTen = reader.GetString(1),
                    ngSinh = reader.GetString(2),
                    gioiTinh = reader.GetString(3),
                    sdt = reader.GetString(4),
                    goiTap = reader.GetString(5),
                    ngDK = reader.GetString(6),
                    thGianTap = reader.GetString(7),
                });
            }
            myKhachHang.ItemsSource = kh;
            reader.Close();

        }

        private void UpdateAllValidities()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT id, goiTap, ngDK FROM khachhang", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string GT = reader.GetString(1);
                string DK = reader.GetString(2);

                DateTime registeredDate = DateTime.Parse(DK);

                int packageDurationInMonths = GetPackageDuration(GT);

                DateTime currentDate = DateTime.Now;
                string thGianTap;
                if ((currentDate.Year * 12 + currentDate.Month) - (registeredDate.Year * 12 + registeredDate.Month) == packageDurationInMonths
                    && (currentDate.Day > registeredDate.Day))
                {
                    thGianTap = "Hết hạn";
                }
                else if ((currentDate.Year * 12 + currentDate.Month) - (registeredDate.Year * 12 + registeredDate.Month) > packageDurationInMonths)
                {
                    thGianTap = "Hết hạn";
                }
                else
                {
                    thGianTap = "Còn hạn";
                }


                string query = "UPDATE khachhang SET thGianTap = @thGianTap WHERE id = @id";
                SQLiteCommand updateCmd = new SQLiteCommand(query, connection);
                updateCmd.Parameters.AddWithValue("@thGianTap", thGianTap);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();
            }

            reader.Close();
            LoadData();
        }


        public void btnClear(object sender, RoutedEventArgs e)
        {
            khName.Text = "";
            khGioitinh.SelectedIndex = -1;
            khNgsinh.Text = "";
            khSdt.Text = "";
            khGoitap.SelectedIndex = -1;
            khNgdk.Text = "";
            search.Text = "";
        }
        public void clear()
        {
            khName.Text = "";
            khGioitinh.SelectedIndex = -1;
            khNgsinh.Text = "";
            khSdt.Text = "";
            khGoitap.SelectedIndex = -1;
            khNgdk.Text = "";
        }
        public void btnThem(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(khName.Text) && !string.IsNullOrEmpty(khGoitap.Text) && !string.IsNullOrEmpty(khNgdk.Text))
            {
                string query = "INSERT INTO khachhang(hoTen, ngSinh, gioiTinh, sdt, goiTap, ngDK) VALUES(@hoTen, @ngSinh, @gioiTinh, @sdt, @goiTap, @ngDK)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@hoTen", khName.Text);
                command.Parameters.AddWithValue("@ngSinh", khNgsinh.Text);
                command.Parameters.AddWithValue("@gioiTinh", khGioitinh.Text);
                command.Parameters.AddWithValue("@sdt", khSdt.Text);
                command.Parameters.AddWithValue("@goiTap", khGoitap.Text);
                command.Parameters.AddWithValue("@ngDK", khNgdk.Text);
                command.ExecuteNonQuery();
                UpdateAllValidities();
                clear();
            }
            else
            {
                MessageBox.Show("Chưa đủ thông tin");
            }
        }
        public void btnSua(object sender, RoutedEventArgs e)
        {
            if (myKhachHang.SelectedItem != null)
            {
                KhachHang selectedKH = (KhachHang)myKhachHang.SelectedItem;
                string query = "UPDATE khachhang SET hoTen = @hoTen, ngSinh = @ngSinh, gioiTinh = @gioiTinh, sdt = @sdt, goiTap = @goiTap, ngDK = @ngDK WHERE id = @id";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@hoTen", khName.Text);
                command.Parameters.AddWithValue("@ngSinh", khNgsinh.Text);
                command.Parameters.AddWithValue("@gioiTinh", khGioitinh.Text);
                command.Parameters.AddWithValue("@sdt", khSdt.Text);
                command.Parameters.AddWithValue("@goiTap", khGoitap.Text);
                command.Parameters.AddWithValue("@ngDK", khNgdk.Text);
                command.Parameters.AddWithValue("@id", selectedKH.id);
                command.ExecuteNonQuery();
                LoadData();
                UpdateAllValidities();
                clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 khách hàng");
            }

        }
        public void btnXoa(object sender, RoutedEventArgs e)
        {
            if (myKhachHang.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn đã chắc chưa?", "Xác nhận xóa",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    KhachHang selectedKH = (KhachHang)myKhachHang.SelectedItem;
                    string query = "DELETE FROM khachhang WHERE id = @id";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@id", selectedKH.id);
                    command.ExecuteNonQuery();
                    LoadData();
                    clear();
                }
            }
            else { }
        }
        public class KhachHang
        {
            public int id { get; set; }
            public string hoTen { get; set; }
            public string ngSinh { get; set; }
            public string gioiTinh { get; set; }
            public string sdt { get; set; }
            public string goiTap { get; set; }
            public string ngDK { get; set; }
            public string thGianTap { get; set; }

        }
        private void myKhachHang_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (myKhachHang.SelectedItem != null)
            {
                KhachHang selectedKH = (KhachHang)myKhachHang.SelectedItem;
                khName.Text = selectedKH.hoTen;
                khGioitinh.Text = selectedKH.gioiTinh;
                khGoitap.Text = selectedKH.goiTap;
                khNgdk.Text = selectedKH.ngDK;
                khNgsinh.Text = selectedKH.ngSinh;
                khSdt.Text = selectedKH.sdt;
            }
        }

        private int GetPackageDuration(string packageName)
        {
            switch (packageName)
            {
                case "VIP":
                    return 6;
                case "Normal":
                    return 1;
                case "Silver":
                    return 3;
                default:
                    return 0;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = search.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(myKhachHang.ItemsSource);

            if (!string.IsNullOrEmpty(keyword))
            {
                view.Filter = item =>
                {
                    KhachHang khachHang = item as KhachHang;
                    return khachHang != null && (khachHang.hoTen.ToLower().Contains(keyword) ||
                                                  khachHang.ngSinh.ToLower().Contains(keyword) ||
                                                  khachHang.gioiTinh.ToLower().Contains(keyword) ||
                                                  khachHang.sdt.ToLower().Contains(keyword) ||
                                                  khachHang.goiTap.ToLower().Contains(keyword) ||
                                                  khachHang.ngDK.ToLower().Contains(keyword) ||
                                                  khachHang.thGianTap.ToLower().Contains(keyword));
                };
            }
            else
            {
                view.Filter = null;
            }
        }


    }
}