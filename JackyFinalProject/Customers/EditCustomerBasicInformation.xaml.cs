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
    /// Interaction logic for EditCustomerBasicInformation.xaml
    /// </summary>
    public partial class EditCustomerBasicInformation : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditCustomerBasicInformation()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonUserNameEditCustomerBasicInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            if ((CustomersPage.OriginalCustomerUserName != textBoxUserNameEditCustomerBasicInformationWindow.Text) && 
                (textBoxUserNameEditCustomerBasicInformationWindow.Text != "" && textBoxUserNameEditCustomerBasicInformationWindow.Text != null))
            {
                var customerUserName = (from cun in jackyDigitalXDBEntities.Customers
                                        where cun.UserName == textBoxUserNameEditCustomerBasicInformationWindow.Text
                                        select cun);

                if (customerUserName.Count() != 0)
                {
                    MessageBox.Show("Specified Customer Username already exists, please choose another name.");
                }
                else if (textBoxFirstNameEditCustomerBasicInformationWindow.Text == null || textBoxFirstNameEditCustomerBasicInformationWindow.Text == "" ||
                        textBoxLastNameEditCustomerBasicInformationWindow.Text == null || textBoxLastNameEditCustomerBasicInformationWindow.Text == "")
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    CustomersPage.Customer.UserName = textBoxUserNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.FirstName = textBoxFirstNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.LastName = textBoxLastNameEditCustomerBasicInformationWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else if ((CustomersPage.OriginalCustomerUserName != textBoxUserNameEditCustomerBasicInformationWindow.Text) &&
                (textBoxUserNameEditCustomerBasicInformationWindow.Text == "" || textBoxUserNameEditCustomerBasicInformationWindow.Text == null))
            {
                if (textBoxFirstNameEditCustomerBasicInformationWindow.Text == null || textBoxFirstNameEditCustomerBasicInformationWindow.Text == "" ||
                        textBoxLastNameEditCustomerBasicInformationWindow.Text == null || textBoxLastNameEditCustomerBasicInformationWindow.Text == "")
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    CustomersPage.Customer.UserName = textBoxUserNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.FirstName = textBoxFirstNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.LastName = textBoxLastNameEditCustomerBasicInformationWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else if (CustomersPage.OriginalCustomerUserName == textBoxUserNameEditCustomerBasicInformationWindow.Text)
            {
                if (textBoxFirstNameEditCustomerBasicInformationWindow.Text == null || textBoxFirstNameEditCustomerBasicInformationWindow.Text == "" ||
                                       textBoxLastNameEditCustomerBasicInformationWindow.Text == null || textBoxLastNameEditCustomerBasicInformationWindow.Text == "")
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    CustomersPage.Customer.UserName = textBoxUserNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.FirstName = textBoxFirstNameEditCustomerBasicInformationWindow.Text;
                    CustomersPage.Customer.LastName = textBoxLastNameEditCustomerBasicInformationWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
        }
        private void cancelButtonUserNameEditCustomerBasicInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}