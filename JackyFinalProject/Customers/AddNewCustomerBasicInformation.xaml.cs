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
    /// Interaction logic for AddNewCustomerBasicInformation.xaml
    /// </summary>
    public partial class AddNewCustomerBasicInformation : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public bool SaveButtonPressed { get; set; } = false;
        public AddNewCustomerBasicInformation()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void cancelButtonUserNameAddCustomerBasicInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButtonUserNameAddCustomerBasicInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUserNameAddCustomerBasicInformationWindow.Text != "" && textBoxUserNameAddCustomerBasicInformationWindow.Text != null)
            {

                var customerUserName = (from cun in jackyDigitalXDBEntities.Customers
                                        where cun.UserName == textBoxUserNameAddCustomerBasicInformationWindow.Text
                                        select cun);
                if (customerUserName.Count() != 0)
                {
                    MessageBox.Show("Specified Customer Username already exists, please choose another name.");
                }
                else if (textBoxFirstNameAddCustomerBasicInformationWindow.Text == null || textBoxFirstNameAddCustomerBasicInformationWindow.Text == "" ||
                         textBoxLastNameAddCustomerBasicInformationWindow.Text == null || textBoxLastNameAddCustomerBasicInformationWindow.Text == "")
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    CustomersPage.Customer.CustomerID = Convert.ToInt32(textBlockCustomerIDAddCustomerBasicInformationWindow.Text);
                    CustomersPage.Customer.UserName = textBoxUserNameAddCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.FirstName = textBoxFirstNameAddCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.LastName = textBoxLastNameAddCustomerBasicInformationWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else if (textBoxUserNameAddCustomerBasicInformationWindow.Text == "" || textBoxUserNameAddCustomerBasicInformationWindow.Text == null)
            {
                if (textBoxFirstNameAddCustomerBasicInformationWindow.Text == null || textBoxFirstNameAddCustomerBasicInformationWindow.Text == "" ||
                         textBoxLastNameAddCustomerBasicInformationWindow.Text == null || textBoxLastNameAddCustomerBasicInformationWindow.Text == "")
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    CustomersPage.Customer.CustomerID = Convert.ToInt32(textBlockCustomerIDAddCustomerBasicInformationWindow.Text);
                    CustomersPage.Customer.UserName = textBoxUserNameAddCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.FirstName = textBoxFirstNameAddCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.LastName = textBoxLastNameAddCustomerBasicInformationWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }    
        }
    }
}
