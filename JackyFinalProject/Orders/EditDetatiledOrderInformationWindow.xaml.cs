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
    /// Interaction logic for EditDetatiledOrderInformationWindow.xaml
    /// </summary>
    public partial class EditDetatiledOrderInformationWindow : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        public bool Packaged { get; set; } = false;

        public static int ProductStockUnits { get; set; }

        public static Product ProductSelected { get; set; }

        public static bool ButtonFindPressed { get; set; } = false;

        public static int orderQuantity { get; set; }

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditDetatiledOrderInformationWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void cancelButtonEditDetailedOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void checkBoxPackagedEditDetailedOrderInformationWindow_Checked(object sender, RoutedEventArgs e)
        {
            Packaged = true;
        }

        private void checkBoxPackagedEditDetailedOrderInformationWindow_Unchecked(object sender, RoutedEventArgs e)
        {
            Packaged = false;
        }

        private void buttonFindProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchProduct searchProduct = new SearchProduct();
            searchProduct.ShowDialog();
            if (searchProduct.IsSelected == true)
            {
                textBlockProductIDEditOrderDetailedInformationWindow.Text = Convert.ToString(searchProduct.Product.ProductID);
                ProductStockUnits = searchProduct.Product.UnitsInStock;
                ProductSelected = searchProduct.Product;
                ButtonFindPressed = true;
            }
            else
            {
                //ProductStockUnits = searchProduct.Product.UnitsInStock;
                //ProductSelected = searchProduct.Product;
                ButtonFindPressed = false;
            }
        }

        private void SaveButtonOnEditDetailedOrderInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBoxQuantityEditDetailedOrderInformationWindow.Text) <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.");
                }
                else
                {
                    if ((textBlockProductIDEditOrderDetailedInformationWindow.Text != null && textBoxQuantityEditDetailedOrderInformationWindow.Text != null) &&
                        (textBlockProductIDEditOrderDetailedInformationWindow.Text != "" && textBoxQuantityEditDetailedOrderInformationWindow.Text != "") &&
                        (textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text == null || textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text == ""))
                    {
                        OrdersPage.OrderDetail.DetailID = Convert.ToInt32(textBoxDetailedIDEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.OrderID = Convert.ToInt32(textBlockOrderIDEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.ProductID = Convert.ToInt32(textBlockProductIDEditOrderDetailedInformationWindow.Text);
                        OrdersPage.OrderDetail.Quantity = Convert.ToInt32(textBoxQuantityEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.Packaged = false;
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else if ((textBlockProductIDEditOrderDetailedInformationWindow.Text != null && textBoxQuantityEditDetailedOrderInformationWindow.Text != null) &&
                        (textBlockProductIDEditOrderDetailedInformationWindow.Text != "" && textBoxQuantityEditDetailedOrderInformationWindow.Text != "") &&
                        (textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text != null && textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text != ""))
                    {
                        OrdersPage.OrderDetail.DetailID = Convert.ToInt32(textBoxDetailedIDEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.OrderID = Convert.ToInt32(textBlockOrderIDEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.ProductID = Convert.ToInt32(textBlockProductIDEditOrderDetailedInformationWindow.Text);
                        OrdersPage.OrderDetail.Quantity = Convert.ToInt32(textBoxQuantityEditDetailedOrderInformationWindow.Text);
                        OrdersPage.OrderDetail.Packaged = Packaged;
                        OrdersPage.OrderDetail.PackagedBy = Convert.ToInt32(textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text);
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Product ID and Quantity cannot be empty.");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Action FAILED! Please note that fields for Product ID, Quantity, and Packaged By cannot be empty. Please also verify all fields contain valid data format. For instance, Quantity can only accep integers and Packaged By can only accept Employee ID. ");
            }
        }

        private void buttonFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            int proID = Convert.ToInt32(textBlockProductIDEditOrderDetailedInformationWindow.Text.ToString());
            Product thisProduct = (from tp in jackyDigitalXDBEntities.Products
                                   where tp.ProductID == proID
                                   select tp).FirstOrDefault();
            int currentStocks = thisProduct.UnitsInStock;
            if (currentStocks >= Convert.ToInt32(textBoxQuantityEditDetailedOrderInformationWindow.Text))
            {
                SearchEmployee searchEmployee = new SearchEmployee();
                searchEmployee.ShowDialog();
                if (searchEmployee.IsSelected == true)
                {
                    Packaged = true;
                    textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text = Convert.ToString(searchEmployee.Employee.EmployeeID);
                }
                if (textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text != null && textBlockEmployeeIDEditOrderDetailedInformationWindow_Copy.Text != "")
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
            else
            {
                MessageBox.Show("This order cannot be Packaged because the unit in stock is NOT sufficient.");
            }
        }
    }
}
