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
    /// Interaction logic for AddNewEmployeeAddress.xaml
    /// </summary>
    public partial class AddNewEmployeeAddress : Window
    {
        public bool SaveButtonPressed { get; set; } = false;
        public AddNewEmployeeAddress()
        {
            InitializeComponent();
        }

        private void saveButtonAddEmmployeeAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddressTypeAddEmmployeeAddressWindow.Text != null && textBlockAddressTypeAddEmmployeeAddressWindow.Text != "")
            {
                EmployeesPage.Address.AddressID = Convert.ToInt32(textBlockAddressIDAddEmmployeeAddressWindow.Text);
                EmployeesPage.Address.AddressType = Convert.ToInt32(textBlockAddressTypeAddEmmployeeAddressWindow.Text);
                EmployeesPage.Address.Street = textBoxStreetAddEmmployeeAddressWindow.Text;
                EmployeesPage.Address.Suburb = textBoxSuburbAddEmmployeeAddressWindow.Text;
                EmployeesPage.Address.City = textBoxCityAddEmmployeeAddressWindow.Text;
                EmployeesPage.Address.Country = textBoxCountryAddEmmployeeAddressrWindow.Text;
                EmployeesPage.Address.PostalCode = textBoxPostCodeAddEmmployeeAddressWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
            else 
            {
                MessageBox.Show("Address Type cannot be empty.");
            }
        }

        private void calcelButtonAddEmmployeeAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonFindAddressType_Click(object sender, RoutedEventArgs e)
        {
            AddressInfo.AddressInformation addressInformation = new AddressInfo.AddressInformation();
            addressInformation.ShowDialog();
            if (addressInformation.AddressTypeSelected == true)
            {
                textBlockAddressTypeAddEmmployeeAddressWindow.Text = addressInformation.AddressTypeInt.ToString();
            }
            else
            {
                return;
            }
        }
    }
}
