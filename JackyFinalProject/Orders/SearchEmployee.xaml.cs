﻿using System;
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

namespace JackyFinalProject.Orders
{
    /// <summary>
    /// Interaction logic for SearchEmployee.xaml
    /// </summary>
    public partial class SearchEmployee : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public bool IsSelected { get; set; }

        public Employee Employee { get; set; }
        public SearchEmployee()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            listViewEmployeePageEmployeeTable.ItemsSource = jackyDigitalXDBEntities.Employees.ToList();

        }

        private void userNameTextBoxSearchEmployeeWindow_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (userNameTextBoxSearchEmployeeWindow.Text.Length == 0)
            {
                userNameTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewEmployeePageEmployeeTable.ItemsSource = jackyDigitalXDBEntities.Employees.ToList();
            }
            else
            {
                userNameTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var employees = from employee in jackyDigitalXDBEntities.Employees
                                where employee.LastName.Contains(userNameTextBoxSearchEmployeeWindow.Text)
                                select employee;
                listViewEmployeePageEmployeeTable.ItemsSource = employees.ToList();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEmployeePageEmployeeTable.SelectedItem != null)
            {
                IsSelected = true;
                Employee = listViewEmployeePageEmployeeTable.SelectedItem as Employee;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an Employee!");
            }
        }
    }
}
