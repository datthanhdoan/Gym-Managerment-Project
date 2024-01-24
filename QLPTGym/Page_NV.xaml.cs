using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QLPTGym
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }

    public class StaffManager
    {
        private List<Staff> staffList;

        public StaffManager()
        {
            staffList = new List<Staff>();
            // Add some sample data if needed
            staffList.Add(new Staff { StaffId = 1, StaffName = "John Doe", Position = "Manager", Salary = 50000 });
            staffList.Add(new Staff { StaffId = 2, StaffName = "Jane Doe", Position = "Assistant", Salary = 40000 });
        }

        public List<Staff> GetStaffMembers()
        {
            return staffList;
        }

        public void AddStaff(Staff newStaff)
        {
            staffList.Add(newStaff);
        }
    }

    public partial class Page_NV : Page
    {
        private StaffManager staffManager;

        public Page_NV()
        {
            InitializeComponent();
            staffManager = new StaffManager();
            RefreshStaffList();
        }

        private void RefreshStaffList()
        {
            StaffListView.ItemsSource = staffManager.GetStaffMembers();
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (!int.TryParse(IdTextBox.Text, out int staffId))
                {
                    throw new FormatException("Mã nhân viên phải là số nguyên");
                }

                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    throw new FormatException("Tên nhân viên không được để trống");
                }

                if (string.IsNullOrEmpty(PositionTextBox.Text))
                {
                    throw new FormatException("Chức vụ nhân viên không được để trống");
                }

                if (!decimal.TryParse(SalaryTextBox.Text, out decimal salary))
                {
                    throw new FormatException("Lương nhân viên phải là số thập phân");
                }

                Staff newStaff = new Staff
                {
                    StaffId = staffId,
                    StaffName = NameTextBox.Text,
                    Position = PositionTextBox.Text,
                    Salary = salary
                };

                staffManager.AddStaff(newStaff);
                RefreshStaffList();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
