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
    /// Interaction logic for AddBasicOrderInformationWindow.xaml
    /// </summary>
    public partial class AddBasicOrderInformationWindow : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public bool SaveButtonPressed { get; set; } = false;
        public AddBasicOrderInformationWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            textBlockOrderDateAddBasicOrderInformationWindow.Text = DateTime.Now.ToString();
        }

        private void buttonOrderGetOrderTimeAddBasicOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            textBlockOrderDateAddBasicOrderInformationWindow.Text = DateTime.Now.ToString();
        }

        private void cancelButtonAddBasicOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButtonAddBasicOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            if ((textBlockOrderIDAddBasicOrderInformationWindow.Text != null && textBlockCustomerIDAddBasicOrderInformationWindow.Text != null) &&
                textBlockOrderIDAddBasicOrderInformationWindow.Text != "" && textBlockCustomerIDAddBasicOrderInformationWindow.Text != "")
            {
                OrdersPage.Order.OrderID = Convert.ToInt32(textBlockOrderIDAddBasicOrderInformationWindow.Text);
                OrdersPage.Order.CustomerID = Convert.ToInt32(textBlockCustomerIDAddBasicOrderInformationWindow.Text);
                OrdersPage.Order.AddressID = Convert.ToInt32(textBlockCustomerAddressIDAddBasicOrderInformationWindow.Text);
                OrdersPage.Order.OrderDate = Convert.ToDateTime(textBlockOrderDateAddBasicOrderInformationWindow.Text);
                OrdersPage.Order.Complete = false;
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Essential information is missing, please complete all fields and try again.");
            }
        }

        private void buttonFindCustomerAddressID_Click(object sender, RoutedEventArgs e)
        {
            SearchCustomer searchCustomer = new SearchCustomer();
            searchCustomer.ShowDialog();
            if (searchCustomer.IsSelected == true)
            {
               textBlockCustomerIDAddBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Customer.CustomerID);
               textBlockCustomerAddressIDAddBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Address.AddressID);
            }
        }

        private void buttonFindCustomerID_Click(object sender, RoutedEventArgs e)
        {
            SearchCustomer searchCustomer = new SearchCustomer();
            searchCustomer.ShowDialog();
            if (searchCustomer.IsSelected == true)
            {
                textBlockCustomerIDAddBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Customer.CustomerID);
                textBlockCustomerAddressIDAddBasicOrderInformationWindow.Text = Convert.ToString(searchCustomer.Address.AddressID);
            }
        }
    }
}
