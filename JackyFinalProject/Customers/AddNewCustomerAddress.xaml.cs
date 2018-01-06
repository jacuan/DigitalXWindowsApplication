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

namespace JackyFinalProject.Customers
{
    /// <summary>
    /// Interaction logic for AddNewCustomerAddress.xaml
    /// </summary>
    public partial class AddNewCustomerAddress : Window
    {
        public bool SaveButtonPressed { get; set; } = false;
        public AddNewCustomerAddress()
        {
            InitializeComponent();
        }

        private void saveButtonAddCustomerAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddressTypeAddCustomerAddressWindow.Text != null && textBlockAddressTypeAddCustomerAddressWindow.Text != "")
            {   CustomersPage.Address.AddressID = Convert.ToInt32(textBlockAddressIDAddCustomerAddressWindow.Text);
                CustomersPage.Address.AddressType = Convert.ToInt32(textBlockAddressTypeAddCustomerAddressWindow.Text);
                CustomersPage.Address.Street = textBoxStreetAddCustomerAddressWindow.Text;
                CustomersPage.Address.Suburb = textBoxSuburbAddCustomerAddressWindow.Text;
                CustomersPage.Address.City = textBoxCityAddCustomerAddressWindow.Text;
                CustomersPage.Address.Country = textBoxCountryAddCustomerAddressrWindow.Text;
                CustomersPage.Address.PostalCode = textBoxPostCodeAddCustomerAddressWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Address Type cannot be empty.");
            }
        }

        private void calcelButtonAddCustomerAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonFindAddressType_Click(object sender, RoutedEventArgs e)
        {
            AddressInfo.AddressInformation addressInformation = new AddressInfo.AddressInformation();
            addressInformation.ShowDialog();
            if (addressInformation.AddressTypeSelected == true)
            {
                textBlockAddressTypeAddCustomerAddressWindow.Text = addressInformation.AddressTypeInt.ToString();
            }
            else
            {
                return;
            }
        }
    }
}
