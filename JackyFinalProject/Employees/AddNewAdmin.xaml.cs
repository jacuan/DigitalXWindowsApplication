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

namespace JackyFinalProject.Employees
{
    /// <summary>
    /// Interaction logic for AddNewAdmin.xaml
    /// </summary>
    public partial class AddNewAdmin : Window
    {
        public bool IsSelected { get; set; } = false;

        public Employee NewAdmin { get; set; }

        public AddNewAdmin()
        {
            InitializeComponent();
        }
        private void userNameTextBoxSearchEmployeeWindow_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (userNameTextBoxSearchEmployeeWindow.Text.Length == 0)
            {
                userNameTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewEmployeePageEmployeeTable.ItemsSource = EmployeesPage.JackyDigitalXDBEntities.Employees.ToList();
            }
            else
            {
                userNameTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var employees = from employee in EmployeesPage.JackyDigitalXDBEntities.Employees
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
                NewAdmin = listViewEmployeePageEmployeeTable.SelectedItem as Employee;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an Employee!");
            }
        }
    }
}
