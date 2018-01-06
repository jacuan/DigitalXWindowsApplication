using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace JackyFinalProject.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public static JackyDigitalXDBEntities JackyDigitalXDBEntities { set; get; }

        public static Employee Emp { get; set; }

        public static Address Address { get; set; }

        public static string OriginalEmployeeUsername { get; set; }

        public static string OriginalPassword { get; set; }

        public static byte[] OriginalPicture { get; set; }

        public EmployeesPage()
        {
            JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            loadEmployeeTable();
        }

        public void loadEmployeeTable()
        {
            listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();  
        }

        private void editRoleButtonEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEmployeePageEmployeeTable.SelectedItem != null)
            {
                Employee employee = (from emp in JackyDigitalXDBEntities.Employees
                                     where emp.Username == MainWindow.Username
                                     select emp).FirstOrDefault();
                int employeeID = employee.EmployeeID;
                var thisEmployeeRoles = JackyDigitalXDBEntities.selectEmployeeRoles(employeeID);
                Verification verification = new Verification();

                if (thisEmployeeRoles.Contains(verification.Administrator))
                {
                    int i = listViewEmployeePageEmployeeTable.SelectedIndex;
                    Emp = listViewEmployeePageEmployeeTable.SelectedItem as Employee;
                    EmployeeRoleManagementWindow employeeRoleManagementWindow = new EmployeeRoleManagementWindow();
                    employeeRoleManagementWindow.rightLB.ItemsSource = Emp.Roles.ToList();
                    employeeRoleManagementWindow.leftLB.ItemsSource = JackyDigitalXDBEntities.Roles.ToList();
                    employeeRoleManagementWindow.ShowDialog();

                    if (employeeRoleManagementWindow.NewAdminNeeded == false)
                    {
                        JackyDigitalXDBEntities.SaveChanges();
                        listViewEditEmployeePageRoleTable.ItemsSource = null;
                        JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                        listViewEmployeePageEmployeeTable.SelectedIndex = i;
                        Binding binding = new Binding("SelectedItem.Roles");
                        binding.Source = listViewEmployeePageEmployeeTable;
                        listViewEditEmployeePageRoleTable.SetBinding(ListView.ItemsSourceProperty, binding);
                    }
                    else
                    {
                        AddNewAdmin addNewAdmin = new AddNewAdmin();
                        addNewAdmin.listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                        addNewAdmin.ShowDialog();

                        while(addNewAdmin.IsSelected == false)
                        {
                            MessageBox.Show("A new Administrator is required because the system must have ONE (and only one) Administrator at any time. Please choose a new Administrator in the new dialog box.");
                            addNewAdmin = new AddNewAdmin();
                            addNewAdmin.listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                            addNewAdmin.ShowDialog();
                        }
                        Role adminRole = (from ar in JackyDigitalXDBEntities.Roles
                                          where ar.Role1 == verification.Administrator
                                          select ar).FirstOrDefault();
                            Emp.Roles.Remove(adminRole);
                            addNewAdmin.NewAdmin.Roles.Add(adminRole);
                            JackyDigitalXDBEntities.SaveChanges();
                            listViewEmployeePageEmployeeTable.ItemsSource = null;
                            listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                            listViewEmployeePageEmployeeTable.SelectedIndex = i;
                        MessageBox.Show(string.Format("You successfully assigned Administrator role to {0}  {1}", addNewAdmin.NewAdmin.FirstName, addNewAdmin.NewAdmin.LastName)); 
                    }
                }
                else
                {
                    int i = listViewEmployeePageEmployeeTable.SelectedIndex;
                    Emp = listViewEmployeePageEmployeeTable.SelectedItem as Employee;
                    verification = new Verification();
                    var selectedEmployeeRoles = JackyDigitalXDBEntities.selectEmployeeRoles(Emp.EmployeeID);
                    if (selectedEmployeeRoles.Contains(verification.Administrator))
                    {
                        MessageBox.Show("You do NOT have permission to modify this user");
                    }
                    else
                    {
                        var rolesWithoutAdmin = from rwa in JackyDigitalXDBEntities.Roles
                                                where rwa.Role1 != verification.Administrator
                                                select rwa;

                        EmployeeRoleManagementWindow employeeRoleManagementWindow = new EmployeeRoleManagementWindow();
                        employeeRoleManagementWindow.rightLB.ItemsSource = Emp.Roles.ToList();
                        employeeRoleManagementWindow.leftLB.ItemsSource = rolesWithoutAdmin.ToList();
                        employeeRoleManagementWindow.ShowDialog();
                        JackyDigitalXDBEntities.SaveChanges();
                        listViewEditEmployeePageRoleTable.ItemsSource = null;
                        JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                        listViewEmployeePageEmployeeTable.SelectedIndex = i;
                        Binding binding = new Binding("SelectedItem.Roles");
                        binding.Source = listViewEmployeePageEmployeeTable;
                        listViewEditEmployeePageRoleTable.SetBinding(ListView.ItemsSourceProperty, binding);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee first!");
            }
        }

        private void buttonNewEmployeeBasicInformationEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            Emp = new Employee();
            AddNewEmployeeBasicInformation addnewEmployeeBasicInformation = new AddNewEmployeeBasicInformation();
            addnewEmployeeBasicInformation.DataContext = Emp;
            addnewEmployeeBasicInformation.passwordBoxAddNewEmployeeBasicInformation.Visibility = Visibility.Collapsed;
            addnewEmployeeBasicInformation.passwordBoxConfirmNewEmployeeBasicInformation.Visibility = Visibility.Collapsed;
            addnewEmployeeBasicInformation.ShowDialog();
            if (addnewEmployeeBasicInformation.SaveButtonPressed == true)
            {
                JackyDigitalXDBEntities.Employees.Add(Emp);
                JackyDigitalXDBEntities.SaveChanges();
                listViewEmployeePageEmployeeTable.ItemsSource = null;
                listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                Helper.ShowMessage1();
            }
            else
            {
                return;
            }
        }

        private void ButtonEditEmployeeBasicInformationEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEmployeePageEmployeeTable.SelectedItem != null)
            {
                int i = listViewEmployeePageEmployeeTable.SelectedIndex;
                Emp = listViewEmployeePageEmployeeTable.SelectedItem as Employee;

                Employee employee = (from emp in JackyDigitalXDBEntities.Employees
                                     where emp.Username == MainWindow.Username
                                     select emp).FirstOrDefault();
                int employeeID = employee.EmployeeID;
                var thisEmployeeRoles = JackyDigitalXDBEntities.selectEmployeeRoles(employeeID);
                Verification verification = new Verification();

                if (thisEmployeeRoles.Contains(verification.Administrator))
                {
                    OriginalEmployeeUsername = Emp.Username;
                    OriginalPassword = Emp.Password;
                    OriginalPicture = Emp.EmployeeImage;
                    EditEmployeeBasicInformation editEmployeeBasicInformation = new EditEmployeeBasicInformation();
                    editEmployeeBasicInformation.DataContext = listViewEmployeePageEmployeeTable.SelectedItem;
                    editEmployeeBasicInformation.passwordBoxEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                    editEmployeeBasicInformation.passwordBoxConfirmEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                    editEmployeeBasicInformation.ShowDialog();

                    if (editEmployeeBasicInformation.SaveButtonPressed == true)
                    {
                        JackyDigitalXDBEntities.SaveChanges();
                        JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                        listViewEmployeePageEmployeeTable.SelectedIndex = i;
                        Helper.ShowMessage1();
                    }
                    else
                    {
                        JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                        listViewEmployeePageEmployeeTable.SelectedIndex = i;
                    }
                }
                else
                {
                    verification = new Verification();
                    var selectedEmployeeRoles = JackyDigitalXDBEntities.selectEmployeeRoles(Emp.EmployeeID);
                    if (selectedEmployeeRoles.Contains(verification.Administrator))
                    {
                        MessageBox.Show("You do NOT have permission to modify this user");
                    }
                    else
                    {
                        OriginalEmployeeUsername = Emp.Username;
                        OriginalPassword = Emp.Password;
                        OriginalPicture = Emp.EmployeeImage;
                        EditEmployeeBasicInformation editEmployeeBasicInformation = new EditEmployeeBasicInformation();
                        editEmployeeBasicInformation.DataContext = listViewEmployeePageEmployeeTable.SelectedItem;
                        editEmployeeBasicInformation.passwordBoxEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                        editEmployeeBasicInformation.passwordBoxConfirmEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                        editEmployeeBasicInformation.ShowDialog();

                        if (editEmployeeBasicInformation.SaveButtonPressed == true)
                        {
                            JackyDigitalXDBEntities.SaveChanges();
                            JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                            listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                            listViewEmployeePageEmployeeTable.SelectedIndex = i;
                            Helper.ShowMessage1();
                        }
                        else
                        {
                            JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                            listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                            listViewEmployeePageEmployeeTable.SelectedIndex = i;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee first!");
            }
        }

        private void buttonNewAddressEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEmployeePageEmployeeTable.SelectedItem != null)
            {
                int i = listViewEmployeePageEmployeeTable.SelectedIndex;
                Emp = listViewEmployeePageEmployeeTable.SelectedItem as Employee;
                Address = new Address();
                AddNewEmployeeAddress addNewEmployeeAddress = new AddNewEmployeeAddress();
                addNewEmployeeAddress.DataContext = Address;
                addNewEmployeeAddress.ShowDialog();
                if (addNewEmployeeAddress.SaveButtonPressed == true)
                {
                    JackyDigitalXDBEntities.Addresses.Add(Address);
                    Emp.Addresses.Add(Address);
                    JackyDigitalXDBEntities.SaveChanges();
                    listViewEmployeePageEmployeeTable.ItemsSource = null;
                    listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                    listViewEmployeePageEmployeeTable.SelectedIndex = i;
                }
                
            }
            else
            {
                MessageBox.Show("Please select an Employee first!");
            }
        }

        private void ButtonEditEmployeeAddressEmployeePage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEmployeeAddressEmployeePage.SelectedItem != null)
            {
                int i = listViewEmployeePageEmployeeTable.SelectedIndex;
                int j = listViewEmployeeAddressEmployeePage.SelectedIndex;
                Address = listViewEmployeeAddressEmployeePage.SelectedItem as Address;
                EditEmployeeAddress editEmployeeAddress = new EditEmployeeAddress();
                editEmployeeAddress.DataContext = listViewEmployeeAddressEmployeePage.SelectedItem;
                editEmployeeAddress.ShowDialog();
                if (editEmployeeAddress.SaveButtonPressed == true)
                {
                    JackyDigitalXDBEntities.SaveChanges();
                    listViewEmployeePageEmployeeTable.ItemsSource = null;
                    listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                    listViewEmployeePageEmployeeTable.SelectedIndex = i;
                    listViewEmployeeAddressEmployeePage.SelectedIndex = j;
                }
                else
                {
                    listViewEmployeePageEmployeeTable.ItemsSource = null;
                    JackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
                    listViewEmployeePageEmployeeTable.SelectedIndex = i;
                    listViewEmployeeAddressEmployeePage.SelectedIndex = j;
                }

            }
            else
            {
                MessageBox.Show("Please selecte an address first!");
            }
        }

        private void customerTextBoxSearchCustomerWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (employeeTextBoxSearchEmployeeWindow.Text.Length == 0)
            {
                employeeTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewEmployeePageEmployeeTable.ItemsSource = JackyDigitalXDBEntities.Employees.ToList();
            }
            else
            {
                employeeTextBoxSearchEmployeeWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var employees = from employee in JackyDigitalXDBEntities.Employees
                                where employee.LastName.Contains(employeeTextBoxSearchEmployeeWindow.Text)
                                select employee;
                listViewEmployeePageEmployeeTable.ItemsSource = employees.ToList();
            }

        }
    }
}