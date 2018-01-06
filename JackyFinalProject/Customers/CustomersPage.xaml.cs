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

namespace JackyFinalProject.Customers
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public static string OriginalCustomerUserName{ get; set; }
        public static Customer Customer { get; set; }
        public static Address Address { get; set; }

        public CustomersPage()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            loadCustomerTable();
        }

        public void loadCustomerTable()
        {
            listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
        }

        private void buttonNewCustomerBasicInformationCustomerPage_Click(object sender, RoutedEventArgs e)
        {
                Customer = new Customer();
                AddNewCustomerBasicInformation addNewCustomerBasicInformation = new AddNewCustomerBasicInformation();
                addNewCustomerBasicInformation.DataContext = Customer;
                addNewCustomerBasicInformation.ShowDialog();
                if (addNewCustomerBasicInformation.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.Customers.Add(Customer);
                    jackyDigitalXDBEntities.SaveChanges();
                    listViewCustomersPageCustomerTable.ItemsSource = null;
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                    Helper.ShowMessage1();
                }
                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                }
        }

        private void buttonNewCustomerAddressCustomerPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCustomersPageCustomerTable.SelectedItem != null)
            {
                int i = listViewCustomersPageCustomerTable.SelectedIndex;
                Address = new Address();
                AddNewCustomerAddress addNewCustomerAddress = new AddNewCustomerAddress();
                addNewCustomerAddress.DataContext = Address;
                Customer = listViewCustomersPageCustomerTable.SelectedItem as Customer;
                addNewCustomerAddress.ShowDialog();
                if (addNewCustomerAddress.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.Addresses.Add(Address);
                    Customer.Addresses.Add(Address);
                    jackyDigitalXDBEntities.SaveChanges();
                    listViewCustomersPageCustomerTable.ItemsSource = null;
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                    listViewCustomersPageCustomerTable.SelectedIndex = i;

                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a customer first!");
            }
            
        }

        private void buttonEditCustomerBasicInformationCustomerPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCustomersPageCustomerTable.SelectedItem != null)
            {
                int i = listViewCustomersPageCustomerTable.SelectedIndex;
                Customer = listViewCustomersPageCustomerTable.SelectedItem as Customer;
                OriginalCustomerUserName = Customer.UserName;
                EditCustomerBasicInformation editCustomerBasicInformation = new EditCustomerBasicInformation();
                editCustomerBasicInformation.DataContext = listViewCustomersPageCustomerTable.SelectedItem;
                editCustomerBasicInformation.ShowDialog();

                if (editCustomerBasicInformation.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.SaveChanges();
                    Helper.ShowMessage1();
                }
                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                    listViewCustomersPageCustomerTable.SelectedIndex = i;
                }
            }
            else
            {
                MessageBox.Show("Please select a customer first!");
            }
        }

        private void buttonEditCustomerAddressCustomerPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCustomerPageCustomerAddressTable.SelectedItem != null)
            {
                int i = listViewCustomerPageCustomerAddressTable.SelectedIndex;
                int j = listViewCustomersPageCustomerTable.SelectedIndex;
                Address = listViewCustomerPageCustomerAddressTable.SelectedItem as Address;
                EditCustomerAddress editCustomerAddress = new EditCustomerAddress();
                editCustomerAddress.DataContext = listViewCustomerPageCustomerAddressTable.SelectedItem;
                editCustomerAddress.ShowDialog();

                if (editCustomerAddress.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.SaveChanges();
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                    listViewCustomersPageCustomerTable.SelectedIndex = j;
                    listViewCustomerPageCustomerAddressTable.SelectedIndex = i;
                    Helper.ShowMessage1();
                }

                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
                    listViewCustomersPageCustomerTable.SelectedIndex = j;
                    listViewCustomerPageCustomerAddressTable.SelectedIndex = i;
                }
            }
            else
            {
                MessageBox.Show("Please select an address first!");
            }
        }

        private void customerTextBoxSearchCustomerWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (customerTextBoxSearchCustomerWindow.Text.Length == 0)
            {
                customerTextBoxSearchCustomerWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewCustomersPageCustomerTable.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
            }
            else
            {
                customerTextBoxSearchCustomerWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var customers = from customer in jackyDigitalXDBEntities.Customers
                                where customer.LastName.Contains(customerTextBoxSearchCustomerWindow.Text)
                                select customer;
                listViewCustomersPageCustomerTable.ItemsSource = customers.ToList();
            }
        }
    }
}
   
