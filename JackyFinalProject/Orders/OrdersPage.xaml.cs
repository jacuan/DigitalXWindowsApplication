using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JackyFinalProject.Orders
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public static OrderDetail OrderDetail { get; set; }

        public static Order Order { get; set; }

        public static Invoiced Invoiced { get; set; }

        public static Employee Employee { get; set; }

        public OrdersPage()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            LoadListView();
        }

        public void LoadListView()
        {
            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
        }

        private void buttonOrderPageAddBasicOrderInformation_Click(object sender, RoutedEventArgs e)
        {
            Order = new Order();
            AddBasicOrderInformationWindow addBasicOrderInfromatinWindow = new AddBasicOrderInformationWindow();
            addBasicOrderInfromatinWindow.DataContext = Order;
            addBasicOrderInfromatinWindow.ShowDialog();
            if (addBasicOrderInfromatinWindow.SaveButtonPressed == true)
            {
                jackyDigitalXDBEntities.Orders.Add(Order);
                jackyDigitalXDBEntities.SaveChanges();
                listViewOrderPageOrderTable.ItemsSource = null;
                listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                Helper.ShowMessage1();
            }
            else
            {
                return;
            }  
        }

        private void buttonOrderPageEditBasicOrderInformation_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderPageOrderTable.SelectedItem != null)
            {
                Order = listViewOrderPageOrderTable.SelectedItem as Order;

                if (Order.Complete == false)
                {
                    int i = listViewOrderPageOrderTable.SelectedIndex;
                    EditBasicOrderInformationWindow editBasicOrderInformationWindow = new EditBasicOrderInformationWindow();
                    editBasicOrderInformationWindow.DataContext = listViewOrderPageOrderTable.SelectedItem;
                    editBasicOrderInformationWindow.ShowDialog();
                    if (editBasicOrderInformationWindow.SaveButtonPressed == true)
                    {
                        jackyDigitalXDBEntities.SaveChanges();
                        listViewOrderPageOrderTable.ItemsSource = null;
                        listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                        listViewOrderPageOrderTable.SelectedIndex = i;
                        Helper.ShowMessage1();
                    }
                    else
                    {
                        listViewOrderPageOrderTable.ItemsSource = null;
                        listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                        jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewOrderPageOrderTable.SelectedIndex = i;
                    }
                }
                else
                {
                    MessageBox.Show("This 'Order Information' cannot be modified because the status of this order is 'Completed'.");
                }
            }
            else
            {
                Helper.ShowMessage2();
            }
        }

        private void buttonOrderPageAddDetailedOrderInformation_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderPageOrderTable.SelectedItem != null)
            {
                int i = listViewOrderPageOrderTable.SelectedIndex;
                Order = listViewOrderPageOrderTable.SelectedItem as Order;

                if (Order.Complete == false)
                {
                    OrderDetail = new OrderDetail();
                    AddDetatiledOrderInformationWindow addDetailedOrderInformationWindow = new AddDetatiledOrderInformationWindow();
                    addDetailedOrderInformationWindow.textBlockPackaged.Visibility = Visibility.Collapsed;
                    addDetailedOrderInformationWindow.textBlockUnpackaged.Visibility = Visibility.Visible;
                    addDetailedOrderInformationWindow.DataContext = Order;
                    addDetailedOrderInformationWindow.ShowDialog();
                    if (addDetailedOrderInformationWindow.SaveButtonPressed == true)
                    {
                        if (OrderDetail.Quantity <= AddDetatiledOrderInformationWindow.ProductStockUnits)
                        {
                            Order.OrderDetails.Add(OrderDetail);
                            int stockRemaining = AddDetatiledOrderInformationWindow.ProductStockUnits - OrderDetail.Quantity;
                            jackyDigitalXDBEntities.updateProduct(AddDetatiledOrderInformationWindow.ProductSelected.ProductID,
                                AddDetatiledOrderInformationWindow.ProductSelected.RetailerID, AddDetatiledOrderInformationWindow.ProductSelected.SubCategoryID,
                                AddDetatiledOrderInformationWindow.ProductSelected.ProductName, AddDetatiledOrderInformationWindow.ProductSelected.ProductDescription,
                                AddDetatiledOrderInformationWindow.ProductSelected.Price, stockRemaining, AddDetatiledOrderInformationWindow.ProductSelected.Picture);
                            jackyDigitalXDBEntities.SaveChanges();
                            listViewOrderPageOrderTable.ItemsSource = null;
                            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                            listViewOrderPageOrderTable.SelectedIndex = i;
                            MessageBox.Show("Order Detail created successfully!");
                        }
                        else if ((OrderDetail.Quantity > AddDetatiledOrderInformationWindow.ProductStockUnits) && AddDetatiledOrderInformationWindow.ProductStockUnits != 0)
                        {
                            int backOrders = OrderDetail.Quantity - AddDetatiledOrderInformationWindow.ProductStockUnits;
                            MessageBox.Show("The order quantity is greater thant the units in stock. A second order will be generated automatically.");
                            OrderDetail.Quantity = AddDetatiledOrderInformationWindow.ProductStockUnits;
                            Order.OrderDetails.Add(OrderDetail);
                            int stockRemaining = 0;
                            jackyDigitalXDBEntities.updateProduct(AddDetatiledOrderInformationWindow.ProductSelected.ProductID,
                                AddDetatiledOrderInformationWindow.ProductSelected.RetailerID, AddDetatiledOrderInformationWindow.ProductSelected.SubCategoryID,
                                AddDetatiledOrderInformationWindow.ProductSelected.ProductName, AddDetatiledOrderInformationWindow.ProductSelected.ProductDescription,
                                AddDetatiledOrderInformationWindow.ProductSelected.Price, stockRemaining, AddDetatiledOrderInformationWindow.ProductSelected.Picture);
                            jackyDigitalXDBEntities.SaveChanges();
                            listViewOrderPageOrderTable.ItemsSource = null;
                            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                            listViewOrderPageOrderTable.SelectedIndex = i;

                            Order Order2 = new Order();
                            Order2.OrderID = 0;
                            Order2.CustomerID = Order.CustomerID;
                            Order2.AddressID = Order.AddressID;
                            Order2.OrderDate = Order.OrderDate;
                            Order2.Complete = false;
                            jackyDigitalXDBEntities.Orders.Add(Order2);

                            OrderDetail OrderDetail2 = new OrderDetail();
                            OrderDetail2.DetailID = 0;
                            OrderDetail2.OrderID = Order2.OrderID;
                            OrderDetail2.ProductID = OrderDetail.ProductID;
                            OrderDetail2.Quantity = backOrders;
                            OrderDetail2.Packaged = false;
                            OrderDetail2.PackagedBy = null;
                            Order2.OrderDetails.Add(OrderDetail2);

                            jackyDigitalXDBEntities.SaveChanges();
                            listViewOrderPageOrderTable.ItemsSource = null;
                            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                            listViewOrderPageOrderTable.SelectedIndex = i;
                        }
                        else if ((OrderDetail.Quantity > AddDetatiledOrderInformationWindow.ProductStockUnits) && AddDetatiledOrderInformationWindow.ProductStockUnits == 0)
                        {
                            MessageBox.Show("Please not that the unit in stock for this product is 0 (zero). An order will still be generated, however, please purchase more products in order to finalise this order.");
                            Order.OrderDetails.Add(OrderDetail);
                            OrderDetail.Packaged = false;
                            OrderDetail.PackagedBy = null;
                            jackyDigitalXDBEntities.SaveChanges();
                            listViewOrderPageOrderTable.ItemsSource = null;
                            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                            listViewOrderPageOrderTable.SelectedIndex = i;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("The status of this order is 'Completed'. Please create a new order instead.");
                }
            }
            else
            {
                MessageBox.Show("Please select an Order first!");
            }
        }

        private void buttonOrderPageEditDetailedOrderInformation_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderPageOrderDetailedTable.SelectedItem != null)
            {
                Order = listViewOrderPageOrderTable.SelectedItem as Order;
                OrderDetail = listViewOrderPageOrderDetailedTable.SelectedItem as OrderDetail;
                int originalQuantity = OrderDetail.Quantity;
                int i = listViewOrderPageOrderTable.SelectedIndex;
                int j = listViewOrderPageOrderDetailedTable.SelectedIndex;
                if (Order.Complete == false && OrderDetail.Packaged == false)
                {
                    EditDetatiledOrderInformationWindow editDetailedOrderInformationWindow = new EditDetatiledOrderInformationWindow();
                    editDetailedOrderInformationWindow.DataContext = listViewOrderPageOrderDetailedTable.SelectedItem;

                         if (OrderDetail.Packaged == true)
                         {
                            editDetailedOrderInformationWindow.textBlockPackaged.Visibility = Visibility.Visible;
                            editDetailedOrderInformationWindow.textBlockUnpackaged.Visibility = Visibility.Collapsed;
                            editDetailedOrderInformationWindow.Packaged = true;
                         }
                         else
                         {
                            editDetailedOrderInformationWindow.textBlockPackaged.Visibility = Visibility.Collapsed;
                            editDetailedOrderInformationWindow.textBlockUnpackaged.Visibility = Visibility.Visible;
                            editDetailedOrderInformationWindow.Packaged = false;
                         }

                    editDetailedOrderInformationWindow.ShowDialog();

                     if (editDetailedOrderInformationWindow.SaveButtonPressed == true)
                       {

                        if (originalQuantity != Convert.ToInt32(editDetailedOrderInformationWindow.textBoxQuantityEditDetailedOrderInformationWindow.Text.ToString()))
                        {
                            if (EditDetatiledOrderInformationWindow.ButtonFindPressed == true)
                            {
                                if (OrderDetail.Quantity <= EditDetatiledOrderInformationWindow.ProductStockUnits)
                                {
                                    Order.OrderDetails.Add(OrderDetail);
                                    int stockRemaining = EditDetatiledOrderInformationWindow.ProductStockUnits - OrderDetail.Quantity;
                                    jackyDigitalXDBEntities.updateProduct(EditDetatiledOrderInformationWindow.ProductSelected.ProductID,
                                            EditDetatiledOrderInformationWindow.ProductSelected.RetailerID, EditDetatiledOrderInformationWindow.ProductSelected.SubCategoryID,
                                            EditDetatiledOrderInformationWindow.ProductSelected.ProductName, EditDetatiledOrderInformationWindow.ProductSelected.ProductDescription,
                                            EditDetatiledOrderInformationWindow.ProductSelected.Price, stockRemaining, EditDetatiledOrderInformationWindow.ProductSelected.Picture);
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                    Helper.ShowMessage1();
                                }
                                else if ((OrderDetail.Quantity > EditDetatiledOrderInformationWindow.ProductStockUnits) && EditDetatiledOrderInformationWindow.ProductStockUnits != 0)
                                {
                                    int backOrders = OrderDetail.Quantity - EditDetatiledOrderInformationWindow.ProductStockUnits;
                                    MessageBox.Show("The order quantity is greater thant the units in stock. A second order will be generated automatically.");
                                    OrderDetail.Quantity = EditDetatiledOrderInformationWindow.ProductStockUnits;
                                    Order.OrderDetails.Add(OrderDetail);
                                    int stockRemaining = 0;
                                    jackyDigitalXDBEntities.updateProduct(EditDetatiledOrderInformationWindow.ProductSelected.ProductID,
                                        EditDetatiledOrderInformationWindow.ProductSelected.RetailerID, EditDetatiledOrderInformationWindow.ProductSelected.SubCategoryID,
                                        EditDetatiledOrderInformationWindow.ProductSelected.ProductName, EditDetatiledOrderInformationWindow.ProductSelected.ProductDescription,
                                        EditDetatiledOrderInformationWindow.ProductSelected.Price, stockRemaining, EditDetatiledOrderInformationWindow.ProductSelected.Picture);
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;

                                    Order Order2 = new Order();
                                    Order2.OrderID = 0;
                                    Order2.CustomerID = Order.CustomerID;
                                    Order2.AddressID = Order.AddressID;
                                    Order2.OrderDate = Order.OrderDate;
                                    Order2.Complete = false;
                                    jackyDigitalXDBEntities.Orders.Add(Order2);

                                    OrderDetail OrderDetail2 = new OrderDetail();
                                    OrderDetail2.DetailID = 0;
                                    OrderDetail2.OrderID = Order2.OrderID;
                                    OrderDetail2.ProductID = OrderDetail.ProductID;
                                    OrderDetail2.Quantity = backOrders;
                                    OrderDetail2.Packaged = false;
                                    OrderDetail2.PackagedBy = null;
                                    Order2.OrderDetails.Add(OrderDetail2);

                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                }
                                else if ((OrderDetail.Quantity > EditDetatiledOrderInformationWindow.ProductStockUnits) && EditDetatiledOrderInformationWindow.ProductStockUnits == 0)
                                {
                                    MessageBox.Show("Please not that the unit in stock for this product is 0 (zero). An order will still be generated, however, please purchase more products in order to finalise this order.");
                                    Order.OrderDetails.Add(OrderDetail);
                                    OrderDetail.Packaged = false;
                                    OrderDetail.PackagedBy = null;
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                }
                            }
                            else
                            {
                                Product thisProduct = (from tp in jackyDigitalXDBEntities.Products
                                                       where tp.ProductID == OrderDetail.ProductID
                                                       select tp).FirstOrDefault();
                                int currentStocks = thisProduct.UnitsInStock;

                                if (OrderDetail.Quantity <= currentStocks)
                                {
                                    Order.OrderDetails.Add(OrderDetail);
                                    int stockRemaining = currentStocks - OrderDetail.Quantity;
                                    
                                    jackyDigitalXDBEntities.updateProduct(thisProduct.ProductID,
                                            thisProduct.RetailerID, thisProduct.SubCategoryID,
                                            thisProduct.ProductName, thisProduct.ProductDescription,
                                            thisProduct.Price, stockRemaining, thisProduct.Picture);
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                    Helper.ShowMessage1();
                                }
                                else if ((OrderDetail.Quantity > currentStocks) && currentStocks != 0)
                                {
                                    int backOrders = OrderDetail.Quantity - currentStocks;
                                    MessageBox.Show("The order quantity is greater thant the units in stock. A second order will be generated automatically.");
                                    OrderDetail.Quantity = currentStocks;
                                    Order.OrderDetails.Add(OrderDetail);
                                    int stockRemaining = 0;
                                    jackyDigitalXDBEntities.updateProduct(thisProduct.ProductID,
                                            thisProduct.RetailerID, thisProduct.SubCategoryID,
                                            thisProduct.ProductName, thisProduct.ProductDescription,
                                            thisProduct.Price, stockRemaining, thisProduct.Picture);
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;

                                    Order Order2 = new Order();
                                    Order2.OrderID = 0;
                                    Order2.CustomerID = Order.CustomerID;
                                    Order2.AddressID = Order.AddressID;
                                    Order2.OrderDate = Order.OrderDate;
                                    Order2.Complete = false;
                                    jackyDigitalXDBEntities.Orders.Add(Order2);

                                    OrderDetail OrderDetail2 = new OrderDetail();
                                    OrderDetail2.DetailID = 0;
                                    OrderDetail2.OrderID = Order2.OrderID;
                                    OrderDetail2.ProductID = OrderDetail.ProductID;
                                    OrderDetail2.Quantity = backOrders;
                                    OrderDetail2.Packaged = false;
                                    OrderDetail2.PackagedBy = null;
                                    Order2.OrderDetails.Add(OrderDetail2);

                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                }
                                else if ((OrderDetail.Quantity > currentStocks) && currentStocks == 0)
                                {
                                    MessageBox.Show("Please not that the unit in stock for this product is 0 (zero). An order will still be generated, however, please purchase more products in order to finalise this order.");
                                    Order.OrderDetails.Add(OrderDetail);
                                    OrderDetail.Packaged = false;
                                    OrderDetail.PackagedBy = null;
                                    jackyDigitalXDBEntities.SaveChanges();
                                    listViewOrderPageOrderTable.ItemsSource = null;
                                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                                    listViewOrderPageOrderTable.SelectedIndex = i;
                                    listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                                }
                            }
                        }
                        else
                        {
                            jackyDigitalXDBEntities.SaveChanges();
                            listViewOrderPageOrderTable.ItemsSource = null;
                            listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                            listViewOrderPageOrderTable.SelectedIndex = i;
                            listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                        }
                    }
                    else
                    {
                        jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewOrderPageOrderTable.ItemsSource = null;
                        listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                        listViewOrderPageOrderTable.SelectedIndex = i;
                        listViewOrderPageOrderDetailedTable.SelectedIndex = j;
                    }
                }
                else
                {
                    MessageBox.Show("This 'Detailed Order Information' cannot be modified because EITHER the status of this order is 'Completed' OR the order was already 'Packaged'.");
                }
            }
            else
            {
                Helper.ShowMessage2();
            }
        }
        private void despatchFinaliseOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderPageOrderTable.SelectedItem != null)
            {
                Order = listViewOrderPageOrderTable.SelectedItem as Order;
                int i = listViewOrderPageOrderTable.SelectedIndex;

                if (Order.Complete == false)
                {
                    

                    var orderDetails = from ods in jackyDigitalXDBEntities.OrderDetails
                                       where ods.OrderID.Equals(Order.OrderID)
                                       select ods;

                    var unpackageds = from us in jackyDigitalXDBEntities.OrderDetails
                                       where us.OrderID.Equals(Order.OrderID) 
                                       where us.Packaged.Equals(false)
                                       select us;

                    var invoices = from ins in jackyDigitalXDBEntities.Invoiceds
                                   where ins.OrderID.Equals(Order.OrderID)
                                   select ins;
                  

                    if ((orderDetails.Count() != 0 && invoices.Count() != 0) && unpackageds.Count() == 0)
                    {
                        Order.Complete = true;
                        jackyDigitalXDBEntities.SaveChanges();
                        listViewOrderPageOrderTable.ItemsSource = null;
                        listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                        listViewOrderPageOrderTable.SelectedIndex = i;
                        MessageBox.Show("Order Completed!");
                    }
                    else
                    {
                        listViewOrderPageOrderTable.ItemsSource = null;
                        jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                        listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                        listViewOrderPageOrderTable.SelectedIndex = i;
                        MessageBox.Show("Action Falied! This order cannot be completed becasue essential information is missing. Please verify all information, for instance, Invoice infromation, Order Detailed informaiton, Order Packaged status, and try again.");
                    }
                }
                else
                {
                    Helper.ShowMessage6();
                }
            }
            else
            {
                Helper.ShowMessage4();
            }
        }

        private void buttonOrderPageGenerateInvoic_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderPageOrderTable.SelectedItem != null)
            {
                int i = listViewOrderPageOrderTable.SelectedIndex;
                Order = listViewOrderPageOrderTable.SelectedItem as Order;
                var orderDetails = (from ods in jackyDigitalXDBEntities.OrderDetails
                       where ods.OrderID.Equals(Order.OrderID)
                       select ods);
                if (orderDetails.Count() != 0)
                {
                    Invoiced = new Invoiced();
                    Invoiced.InvoiceID = 0;
                    Invoiced.OrderID = Order.OrderID;
                    Invoiced.EmployeeID = GetEmployeeID();
                    Invoiced.ShippingOption = 1;
                    Invoiced.InvoiceDate = System.DateTime.Now;
                    jackyDigitalXDBEntities.Invoiceds.Add(Invoiced);
                    jackyDigitalXDBEntities.SaveChanges();
                    listViewOrderPageOrderTable.ItemsSource = null;
                    listViewOrderPageOrderTable.ItemsSource = jackyDigitalXDBEntities.Orders.ToList();
                    listViewOrderPageOrderTable.SelectedIndex = i;
                    MessageBox.Show("Invoice was created successfully!");
                }
                else
                {
                    MessageBox.Show("Action Failed! 'Order Detail' must be completed before generating an invoice.");
                }
            }
            else
            {
                MessageBox.Show("Please select an Order first!");
            }
        }
        public int GetEmployeeID()
        {
            Employee = (from emp in jackyDigitalXDBEntities.Employees
                        where emp.Username.Equals(MainWindow.Username)
                        select emp).FirstOrDefault();
            return Employee.EmployeeID;
        }
    }
}