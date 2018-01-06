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
    /// Interaction logic for AddDetatiledOrderInformationWindow.xaml
    /// </summary>
    public partial class AddDetatiledOrderInformationWindow : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool SaveButtonPressed { get; set; } = false;

        public bool Packaged { get; set; } = false;

        public static int ProductStockUnits { get; set; }

        public static Product ProductSelected { get; set; }

        public SearchProduct searchProduct { get; set; }

        public AddDetatiledOrderInformationWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            
        }

        private void cancelButtonAddDetailedOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButtonOnAddDetailedOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBoxQuantityAddDetailedOrderInformationWindow.Text) <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.");
                }
                else
                {
                    if ((textBlockProductIDAddOrderDetailedInformationWindow.Text != null && textBoxQuantityAddDetailedOrderInformationWindow.Text != null) &&
                        (textBlockProductIDAddOrderDetailedInformationWindow.Text != "" && textBoxQuantityAddDetailedOrderInformationWindow.Text != "") &&
                        textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text == null || textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text == "")
                    {
                        OrdersPage.OrderDetail.DetailID = Convert.ToInt32(textBlockDetailedIDAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.OrderID = Convert.ToInt32(textBlockOrderIDAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.ProductID = Convert.ToInt32(textBlockProductIDAddOrderDetailedInformationWindow.Text);
                        OrdersPage.OrderDetail.Quantity = Convert.ToInt32(textBoxQuantityAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.Packaged = false;
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else if ((textBlockProductIDAddOrderDetailedInformationWindow.Text != null && textBoxQuantityAddDetailedOrderInformationWindow.Text != null) &&
                        (textBlockProductIDAddOrderDetailedInformationWindow.Text != "" && textBoxQuantityAddDetailedOrderInformationWindow.Text != "") &&
                        (textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text != null && textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text != ""))
                    {
                        OrdersPage.OrderDetail.DetailID = Convert.ToInt32(textBlockDetailedIDAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.OrderID = Convert.ToInt32(textBlockOrderIDAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.ProductID = Convert.ToInt32(textBlockProductIDAddOrderDetailedInformationWindow.Text);
                        OrdersPage.OrderDetail.Quantity = Convert.ToInt32(textBoxQuantityAddDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.Packaged = Packaged;
                        OrdersPage.OrderDetail.PackagedBy = Convert.ToInt32(textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text);
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Product ID and Quantity cannot be empty.");
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Action FAILED! Please note that fields for Product ID, Quantity, and Packaged By cannot be empty. Please also verify all fields contain valid data format. For instance, Quantity can only accep integers and Packaged By can only accept Employee ID. ");
            }
        }

        private void buttonFindProduct_Click(object sender, RoutedEventArgs e)
        {
            searchProduct = new SearchProduct();
            searchProduct.ShowDialog();
            if (searchProduct.IsSelected == true)
            {
                textBlockProductIDAddOrderDetailedInformationWindow.Text = Convert.ToString(searchProduct.Product.ProductID);
                ProductStockUnits = searchProduct.Product.UnitsInStock;
                ProductSelected = searchProduct.Product;
            }
        }

        private void buttonFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchEmployee searchEmployee = new SearchEmployee();
            searchEmployee.ShowDialog();
            if (searchEmployee.IsSelected == true)
            {
                textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text = Convert.ToString(searchEmployee.Employee.EmployeeID);
                Packaged = true;
            }

            if (textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text != null && textBlockEmployeeIDAddOrderDetailedInformationWindow_Copy.Text !="")
            {
                textBlockPackaged.Visibility = Visibility.Visible;
                textBlockUnpackaged.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBlockUnpackaged.Visibility = Visibility.Visible;
                textBlockPackaged.Visibility = Visibility.Collapsed;
            }
        }
    }
}
