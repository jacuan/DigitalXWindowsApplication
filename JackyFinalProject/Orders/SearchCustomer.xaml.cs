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

namespace JackyFinalProject.Orders
{
    /// <summary>
    /// Interaction logic for SearchCustomer.xaml
    /// </summary>
    public partial class SearchCustomer : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool IsSelected { get; set; }

        public Customer Customer { get; set; }

        public Address Address { get; set; }
        public SearchCustomer()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            listViewCustomerInfoSearchCustomer.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
        }

        private void userNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userNameTextBoxSearchCustomerWindow.Text.Length == 0 )
            {
                userNameTextBoxSearchCustomerWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewCustomerInfoSearchCustomer.ItemsSource = jackyDigitalXDBEntities.Customers.ToList();
            }
            else
            {
                userNameTextBoxSearchCustomerWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var customers = from customer in jackyDigitalXDBEntities.Customers
                                where customer.LastName.Contains(userNameTextBoxSearchCustomerWindow.Text)
                                select customer;
                listViewCustomerInfoSearchCustomer.ItemsSource = customers.ToList();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCustomerAddressSearchCustomer.SelectedItem != null)
            {
                IsSelected = true;
                Customer = listViewCustomerInfoSearchCustomer.SelectedItem as Customer;
                Address = listViewCustomerAddressSearchCustomer.SelectedItem as Address;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an Address!");
            }
        }
    }
}
