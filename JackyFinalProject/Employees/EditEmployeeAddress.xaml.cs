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
    /// Interaction logic for EditEmployeeAddress.xaml
    /// </summary>
    public partial class EditEmployeeAddress : Window
    {
        public bool SaveButtonPressed { get; set; } = false;
        public EditEmployeeAddress()
        {
            InitializeComponent();
        }

        private void saveButtonEditEmployeeAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddressTypeEditEmployeeWindow.Text != null && textBlockAddressTypeEditEmployeeWindow.Text != "")
            {
                EmployeesPage.Address.AddressID = Convert.ToInt32(textBoxAddressIDEditEmployeeWindow.Text);
                EmployeesPage.Address.AddressType = Convert.ToInt32(textBlockAddressTypeEditEmployeeWindow.Text);
                EmployeesPage.Address.Street = textBoxStreetEditEmployeeWindow.Text;
                EmployeesPage.Address.Suburb = textBoxSuburbEditEmployeeWindow.Text;
                EmployeesPage.Address.City = textBoxCityEditEmployeeWindow.Text;
                EmployeesPage.Address.Country = textBoxCountryEditEmployeeWindow.Text;
                EmployeesPage.Address.PostalCode = textBoxPostCodeEditEmployeeWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Address Type cannot be empty.");
            }
        }

        private void calcelButtonEditEmployeeAddressWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonFindAddressType_Click(object sender, RoutedEventArgs e)
        {
            AddressInfo.AddressInformation addressInformation = new AddressInfo.AddressInformation();
            addressInformation.ShowDialog();
            if (addressInformation.AddressTypeSelected == true)
            {
                textBlockAddressTypeEditEmployeeWindow.Text = addressInformation.AddressTypeInt.ToString();
            }
            else
            {
                return;
            }
        }
    }
}
