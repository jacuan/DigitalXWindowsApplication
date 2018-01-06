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
    /// Interaction logic for EditBasicOrderInformationWindow.xaml
    /// </summary>
    public partial class EditBasicOrderInformationWindow : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditBasicOrderInformationWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonEditBasicOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            if ((textBlockOrderIDEditBasicOrderInformationWindow.Text != null && textBlockCustomerIDEditBasicOrderInformationWindow.Text != null) &&
               textBlockOrderIDEditBasicOrderInformationWindow.Text != "" && textBlockCustomerIDEditBasicOrderInformationWindow.Text != "")
            {
                OrdersPage.Order.OrderID = Convert.ToInt32(textBlockOrderIDEditBasicOrderInformationWindow.Text);
                OrdersPage.Order.CustomerID = Convert.ToInt32(textBlockCustomerIDEditBasicOrderInformationWindow.Text);
                OrdersPage.Order.AddressID = Convert.ToInt32(textBlockCustomerAddressIDEditBasicOrderInformationWindow.Text);
                OrdersPage.Order.OrderDate = OrdersPage.Order.OrderDate;
                OrdersPage.Order.Complete = OrdersPage.Order.Complete;
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Essential information is missing, please complete all fields and try again.");
            }
        }

        private void cancelButtonEditBasicOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonFindCustomerID_Click(object sender, RoutedEventArgs e)
        {
            SearchCustomer searchCustomer = new SearchCustomer();
            searchCustomer.ShowDialog();
            if (searchCustomer.IsSelected == true)
            {
                textBlockCustomerIDEditBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Customer.CustomerID);
                textBlockCustomerAddressIDEditBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Address.AddressID);
            }
        }

        private void buttonFindCustomerAddressID_Click(object sender, RoutedEventArgs e)
        {
            SearchCustomer searchCustomer = new SearchCustomer();
            searchCustomer.ShowDialog();
            if (searchCustomer.IsSelected == true)
            {
                textBlockCustomerIDEditBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Customer.CustomerID);
                textBlockCustomerAddressIDEditBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Address.AddressID);
            }
        }
    }
}
