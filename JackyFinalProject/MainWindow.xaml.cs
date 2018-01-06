using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using JackyFinalProject.UserControls;

namespace JackyFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        Orders.OrdersPage ordersPage; 
        Products.ProductsPage productsPage; 
        Customers.CustomersPage customersPage; 
        Employees.EmployeesPage employeesPage;
        SplashWindow.PageAfterLogout pageAfterLogout;
        private string password;
        public static string Username { get; set; }

        public MainWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            LoadSplash();
            HideControls();
        }

        public void HideControls()
        {
            buttonOrders.Visibility = Visibility.Collapsed;
            buttonCustomers.Visibility = Visibility.Collapsed;
            buttonEmployees.Visibility = Visibility.Collapsed;
            buttonProducts.Visibility = Visibility.Collapsed;
            //logoutButton.Visibility = Visibility.Collapsed;
            mainFrame.Visibility = Visibility.Collapsed;
        }

        public void ShowControls()
        {
            buttonOrders.Visibility = Visibility.Visible;
            buttonCustomers.Visibility = Visibility.Visible;
            buttonEmployees.Visibility = Visibility.Visible;
            buttonProducts.Visibility = Visibility.Visible;
            //logoutButton.Visibility = Visibility.Visible;
            mainFrame.Visibility = Visibility.Visible;
        }

        public void LoadSplash()
        {
            SplashWindow.SplashWindow splashWindow = new SplashWindow.SplashWindow();
            int timeShowing = 3000;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int timeLeft = timeShowing - (int)timer.ElapsedMilliseconds;
            while (timeLeft > 0)
            {
                timeLeft = timeShowing - (int)timer.ElapsedMilliseconds;
                splashWindow.Show();
            }
            splashWindow.Close();
        }

        private void loginUserControl_LoginClicked(object sender, RoutedEventArgs e)
        {
            LoginClickedArgs loginClickedArgs = e as LoginClickedArgs;
            Username = loginClickedArgs.UserName;
            password = loginClickedArgs.Password;

            Employee employee = (from emp in jackyDigitalXDBEntities.Employees
                                 where emp.Username == Username && emp.Password == password
                                 select emp).FirstOrDefault();

            if (employee == null)
            {
                loginUserControl.userNameTextBox.Text = "";
                loginUserControl.passwordBox.Password = "";
                loginUserControl.WrongUserNameTextBlock.Text = "INVALID Username or password";
                loginUserControl.WrongPasswordBlock.Text = "";
                loginUserControl.userNameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 200, 200));
                loginUserControl.passwordBox.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
            else
            {
                int employeeID = employee.EmployeeID;
                var thisEmployeeRoles = jackyDigitalXDBEntities.selectEmployeeRoles(employeeID);
                Verification verification = new Verification();

                if (jackyDigitalXDBEntities.selectEmployeeRoles(employeeID).Count() != 0)
                {
                    foreach (string aRole in thisEmployeeRoles)
                    {
                        if (aRole.Contains(verification.Administrator))
                        {
                            loginUserControl.WrongUserNameTextBlock.Text = "";
                            loginUserControl.WrongPasswordBlock.Text = "";
                            loginUserControl.Visibility = Visibility.Collapsed;
                            ShowControls();
                            pageAfterLogout = new SplashWindow.PageAfterLogout();
                            mainFrame.NavigationService.Navigate(pageAfterLogout);
                            ordersPage = new Orders.OrdersPage();
                            productsPage = new Products.ProductsPage();
                            customersPage = new Customers.CustomersPage();
                            employeesPage = new Employees.EmployeesPage();
                            return;
                        }

                        else if (aRole.Contains(verification.HumanResources))
                        {
                            loginUserControl.WrongUserNameTextBlock.Text = "";
                            loginUserControl.WrongPasswordBlock.Text = "";
                            loginUserControl.Visibility = Visibility.Collapsed;
                            mainFrame.Visibility = Visibility.Visible;
                            buttonEmployees.Visibility = Visibility.Visible;
                            pageAfterLogout = new SplashWindow.PageAfterLogout();
                            mainFrame.NavigationService.Navigate(pageAfterLogout);
                            employeesPage = new Employees.EmployeesPage();
                        }

                        else if (aRole.Contains(verification.CustomerService))
                        {
                            loginUserControl.Visibility = Visibility.Collapsed;
                            mainFrame.Visibility = Visibility.Visible;
                            buttonCustomers.Visibility = Visibility.Visible;
                            buttonOrders.Visibility = Visibility.Visible;
                            pageAfterLogout = new SplashWindow.PageAfterLogout();
                            mainFrame.NavigationService.Navigate(pageAfterLogout);
                            customersPage = new Customers.CustomersPage();
                            ordersPage = new Orders.OrdersPage();
                        }

                        else if (aRole.Contains(verification.DespatchShipping))
                        {
                            loginUserControl.Visibility = Visibility.Collapsed;
                            mainFrame.Visibility = Visibility.Visible;
                            buttonOrders.Visibility = Visibility.Visible;
                            buttonProducts.Visibility = Visibility.Visible;
                            buttonCustomers.Visibility = Visibility.Visible;
                            pageAfterLogout = new SplashWindow.PageAfterLogout();
                            mainFrame.NavigationService.Navigate(pageAfterLogout);
                            ordersPage = new Orders.OrdersPage();
                            ordersPage.buttonOrderPageAddBasicOrderInformation.Visibility = Visibility.Collapsed;
                            ordersPage.buttonOrderPageAddDetailedOrderInformation.Visibility = Visibility.Collapsed;
                            customersPage = new Customers.CustomersPage();
                            customersPage.buttonNewCustomerBasicInformationCustomerPage.Visibility = Visibility.Collapsed;
                            customersPage.buttonEditCustomerBasicInformationCustomerPage.Visibility = Visibility.Collapsed;
                            customersPage.buttonNewCustomerAddressCustomerPage.Visibility = Visibility.Collapsed;
                            customersPage.buttonEditCustomerAddressCustomerPage.Visibility = Visibility.Collapsed;
                        }

                        else if (aRole.Contains(verification.ProductControl))
                        {
                            loginUserControl.Visibility = Visibility.Collapsed;
                            mainFrame.Visibility = Visibility.Visible;
                            buttonProducts.Visibility = Visibility.Visible;
                            pageAfterLogout = new SplashWindow.PageAfterLogout();
                            mainFrame.NavigationService.Navigate(pageAfterLogout);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have not assigend to any role yet. Please see your administrator for further assistance.");
                }
            }
        }

        private void buttonOrders_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(ordersPage);
        }

        private void buttonProducts_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = (from emp in jackyDigitalXDBEntities.Employees
                                 where emp.Username == Username && emp.Password == password
                                 select emp).FirstOrDefault();

            int employeeID = employee.EmployeeID;
            var thisEmployeeRoles = jackyDigitalXDBEntities.selectEmployeeRoles(employeeID);
            Verification verification = new Verification();

            foreach (string aRole in thisEmployeeRoles)
            {
                if (aRole.Contains(verification.Administrator))
                {
                    productsPage = new Products.ProductsPage();
                    mainFrame.NavigationService.Navigate(productsPage);
                }
                else if (aRole.Contains(verification.DespatchShipping))
                {
                    productsPage = new Products.ProductsPage();
                    productsPage.buttonAddCategoryProductPage.Visibility = Visibility.Collapsed;
                    productsPage.buttonEditCategoryProductPage.Visibility = Visibility.Collapsed;
                    productsPage.buttonAddSubCategoryProductPage.Visibility = Visibility.Collapsed;
                    productsPage.buttonEditSubCategoryProductPage.Visibility = Visibility.Collapsed;
                    productsPage.buttonAddProductProductPage.Visibility = Visibility.Collapsed;
                    productsPage.buttonEditProductProductPage.Visibility = Visibility.Collapsed;
                    mainFrame.NavigationService.Navigate(productsPage);
                }
                else if (aRole.Contains(verification.ProductControl))
                {
                    productsPage = new Products.ProductsPage();
                    mainFrame.NavigationService.Navigate(productsPage);
                }
            }  
        }

        private void buttonEmployees_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(employeesPage);
        }

        private void buttonCustomers_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(customersPage);
        }
    }

    public class Verification
    {
        public string Administrator { get; set; } = "Administrator";
        public string HumanResources { get; set; } = "Human Resources";
        public string CustomerService { get; set; } = "Customer Service";
        public string DespatchShipping { get; set; } = "Despatch & Shipping";
        public string ProductControl { get; set; } = "Product Control";
    }
}

