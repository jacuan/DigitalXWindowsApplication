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
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool IsSelected { get; set; }

        public Product Product { get; set; }

        public SearchProduct()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            loadCategoryTable();
        }
        public void loadCategoryTable()
        {
            listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
        }

        private void userNameTextBoxSearchProductWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userNameTextBoxSearchProductWindow.Text.Length == 0)
            {
                userNameTextBoxSearchProductWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                listViewProductPageProductTable.ItemsSource = null;
                Binding binding = new Binding("SelectedItem.Products");
                binding.Source = listViewProductPageSubCategoryTable;
                listViewProductPageProductTable.SetBinding(ListView.ItemsSourceProperty, binding);
            }
            else
            {
                userNameTextBoxSearchProductWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var products = from product in jackyDigitalXDBEntities.Products
                                where product.ProductName.Contains(userNameTextBoxSearchProductWindow.Text)
                                select product;
                listViewProductPageProductTable.ItemsSource = products.ToList();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageProductTable.SelectedItem != null)
            {
                IsSelected = true;
                Product = listViewProductPageProductTable.SelectedItem as Product;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a Product!");
            }
        }
    }
}

