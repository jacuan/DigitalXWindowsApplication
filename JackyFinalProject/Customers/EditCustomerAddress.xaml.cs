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
    /// Interaction logic for EditCustomerAddress.xaml
    /// </summary>
    public partial class EditCustomerAddress : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditCustomerAddress()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void calcelButtonEditCustomerAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButtonEditCustomerAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddressTypeEditCustomerWindow.Text != null && textBlockAddressTypeEditCustomerWindow.Text != "")
            {   CustomersPage.Address.AddressID = Convert.ToInt32(textBoxAddressIDEditCustomerWindow.Text);
                CustomersPage.Address.AddressType = Convert.ToInt32(textBlockAddressTypeEditCustomerWindow.Text);
                CustomersPage.Address.Street = textBoxStreetEditCustomerWindow.Text;
                CustomersPage.Address.Suburb = textBoxSuburbEditCustomerWindow.Text;
                CustomersPage.Address.City = textBoxCityEditCustomerWindow.Text;
                CustomersPage.Address.PostalCode = textBoxPostCodeEditCustomerWindow.Text;
                CustomersPage.Address.Country = textBoxCountryEditCustomerWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Address Type cannot be empty.");
            }
        }

        private void buttonFindAddressType_Click(object sender, RoutedEventArgs e)
        {
            AddressInfo.AddressInformation addressInformation = new AddressInfo.AddressInformation();
            addressInformation.ShowDialog();
            if (addressInformation.AddressTypeSelected == true)
            {
                textBlockAddressTypeEditCustomerWindow.Text = addressInformation.AddressTypeInt.ToString();
            }
            else
            {
                return;
            }
        }
    }
}
