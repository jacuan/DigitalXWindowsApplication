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
    /// Interaction logic for AddInvoiceWindow.xaml
    /// </summary>
    public partial class AddInvoiceWindow : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool SaveButtonPressed { get; set; } = false;

        public int ShippingOption { get; set; }

        public Employee Employee { get; set; }

        public AddInvoiceWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            textBlockInvoicedDateAddInvoiceWindow.Text = System.DateTime.Now.ToString();
            textBlockInvoicedByEmployeeID.Text = GetEmployeeID().ToString();

        }

        private void buttonGetInvoicedTimeAddInvoiceWindow_Click(object sender, RoutedEventArgs e)
        {
            textBlockInvoicedDateAddInvoiceWindow.Text = DateTime.Now.ToString();
        }

        private void calcelButtonAddInvoiceWindow1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButtonAddInvoiceWindow_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true || radioButton_Copy.IsChecked == true || radioButton_Copy1.IsChecked == true || radioButton_Copy2.IsChecked == true)
            {
                OrdersPage.Invoiced.InvoiceID = Convert.ToInt32(textBlockInvoicedIDAddInvoiceWindow.Text);
                OrdersPage.Invoiced.OrderID = Convert.ToInt32(textBlockOrderIDAddInvoiceWindow.Text);
                OrdersPage.Invoiced.EmployeeID = GetEmployeeID();
                OrdersPage.Invoiced.ShippingOption = ShippingOption;
                OrdersPage.Invoiced.InvoiceDate = Convert.ToDateTime(textBlockInvoicedDateAddInvoiceWindow.Text);
                SaveButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please choose an Shipping Option first!");
            }
        }

        public int GetEmployeeID()
        {
            Employee = (from emp in jackyDigitalXDBEntities.Employees
                                  where emp.Username.Equals(MainWindow.Username)
                                  select emp).FirstOrDefault();
            return Employee.EmployeeID;
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            ShippingOption = 1;
        }

        private void radioButton_Copy_Checked(object sender, RoutedEventArgs e)
        {
            ShippingOption = 2;
        }

        private void radioButton_Copy1_Checked(object sender, RoutedEventArgs e)
        {
            ShippingOption = 3;
        }

        private void radioButton_Copy2_Checked(object sender, RoutedEventArgs e)
        {
            ShippingOption = 4;
        }
    }
}
