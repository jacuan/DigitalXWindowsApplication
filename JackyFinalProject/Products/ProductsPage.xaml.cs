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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JackyFinalProject.Products
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public static Product Product { get; set; }
        public static byte[] OriginalPicture { get; set; }
        public static ProductCategory ProductCategory { get; set; }
        public static ProductSubCategory ProductSubCategory { get; set; }
        public static string OriginalProductName { get; set; }
        public static string OriginalSubCategoryName { get; set; }
        public static string OriginalCategoryName { get; set; }

        public ProductsPage()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            loadCategoryTable();
        }

        public void loadCategoryTable()
        {
            listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
        }

        private void buttonAddCategoryProductPage_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            ProductCategory = new ProductCategory();
            addCategory.DataContext = ProductCategory;
            addCategory.ShowDialog();
            if (addCategory.SaveButtonPressed == true)
            {
                jackyDigitalXDBEntities.ProductCategories.Add(ProductCategory);
                jackyDigitalXDBEntities.SaveChanges();
                listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                Helper.ShowMessage1();
            }
            else
            {
                return;
            }
        }

        private void buttonEditCategoryProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageCategoryTable.SelectedItem != null)
            {
                int i = listViewProductPageCategoryTable.SelectedIndex;
                EditCategory editCategory = new EditCategory();
                ProductCategory = listViewProductPageCategoryTable.SelectedItem as ProductCategory;
                editCategory.DataContext = listViewProductPageCategoryTable.SelectedItem;
                OriginalCategoryName = ProductCategory.Category;
                editCategory.ShowDialog();
                if (editCategory.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.SaveChanges();
                    Helper.ShowMessage1();
                }
                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageCategoryTable.SelectedIndex = i;
                }
            }
            else
            {
                Helper.ShowMessage2();
            }
        }

        private void buttonAddSubCategoryProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageCategoryTable.SelectedItem != null)
            {
                int i = listViewProductPageCategoryTable.SelectedIndex;
                AddSubCategory addSubCategory = new AddSubCategory();
                ProductSubCategory = new ProductSubCategory();
                addSubCategory.DataContext = listViewProductPageCategoryTable.SelectedItem;
                addSubCategory.ShowDialog();
                if (addSubCategory.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.ProductSubCategories.Add(ProductSubCategory);
                    jackyDigitalXDBEntities.SaveChanges();
                    listViewProductPageCategoryTable.ItemsSource = null;
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageCategoryTable.SelectedIndex = i;
                    Helper.ShowMessage1();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a Category first!");
            }
        }

        private void buttonEditSubCategoryProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageSubCategoryTable.SelectedItem != null)
            {
                int j = listViewProductPageCategoryTable.SelectedIndex;
                int i = listViewProductPageSubCategoryTable.SelectedIndex;
                EditSubCategory editSubCategory = new EditSubCategory();
                ProductSubCategory = listViewProductPageSubCategoryTable.SelectedItem as ProductSubCategory;
                editSubCategory.DataContext = listViewProductPageSubCategoryTable.SelectedItem;
                OriginalSubCategoryName = ProductSubCategory.SubCategory;
                editSubCategory.ShowDialog();
                if (editSubCategory.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.SaveChanges();
                    Helper.ShowMessage1();
                }
                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageCategoryTable.SelectedIndex = j;
                    listViewProductPageSubCategoryTable.SelectedIndex = i;
                }
            }
            else
            {
                Helper.ShowMessage2();
            } 
        }

        private void buttonAddProductProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageSubCategoryTable.SelectedItem != null)
            {
                int j = listViewProductPageSubCategoryTable.SelectedIndex;
                int k = listViewProductPageCategoryTable.SelectedIndex;
                
                AddProduct addProduct = new AddProduct();
                Product = new Product();
                addProduct.DataContext = listViewProductPageSubCategoryTable.SelectedItem;
                addProduct.ShowDialog();
                if (addProduct.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.Products.Add(Product);
                    jackyDigitalXDBEntities.SaveChanges();
                    listViewProductPageCategoryTable.ItemsSource = null;
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageSubCategoryTable.SelectedIndex = j;
                    listViewProductPageCategoryTable.SelectedIndex = k;
                    int i = listViewProductPageProductTable.Items.Count;
                    listViewProductPageProductTable.SelectedIndex = i - 1;
                    Helper.ShowMessage1();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a SubCategory first!");
            }
        }

        private void buttonEditProductProductPage_Click(object sender, RoutedEventArgs e)
        {
            if (listViewProductPageProductTable.SelectedItem != null)
            {
                int i = listViewProductPageProductTable.SelectedIndex;
                int j = listViewProductPageSubCategoryTable.SelectedIndex;
                int k = listViewProductPageCategoryTable.SelectedIndex;
                EditProduct editProduct = new EditProduct();
                editProduct.DataContext = listViewProductPageProductTable.SelectedItem;
                Product = listViewProductPageProductTable.SelectedItem as Product;
                OriginalPicture = Product.Picture;
                OriginalProductName = Product.ProductName;
                editProduct.ShowDialog();
                if (editProduct.SaveButtonPressed == true)
                {
                    jackyDigitalXDBEntities.SaveChanges();
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageProductTable.SelectedIndex = i;
                    listViewProductPageSubCategoryTable.SelectedIndex = j;
                    listViewProductPageCategoryTable.SelectedIndex = k;
                }
                else
                {
                    jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
                    listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                    listViewProductPageProductTable.SelectedIndex = i;
                    listViewProductPageSubCategoryTable.SelectedIndex = j;
                    listViewProductPageCategoryTable.SelectedIndex = k;
                }
            }
            else
            {
                Helper.ShowMessage2();
            }
        }

        private void productTextBoxSearchProductWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (productTextBoxSearchProductWindow.Text.Length == 0)
            {
                productTextBoxSearchProductWindow.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                listViewProductPageCategoryTable.ItemsSource = jackyDigitalXDBEntities.ProductCategories.ToList();
                listViewProductPageProductTable.ItemsSource = null;
                Binding binding = new Binding("SelectedItem.Products");
                binding.Source = listViewProductPageSubCategoryTable;
                listViewProductPageProductTable.SetBinding(ListView.ItemsSourceProperty, binding);

            }
            else
            {
                productTextBoxSearchProductWindow.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));

                var products = from product in jackyDigitalXDBEntities.Products
                               where product.ProductName.Contains(productTextBoxSearchProductWindow.Text)
                               select product;
                listViewProductPageProductTable.ItemsSource = products.ToList();
            }
        }
    }
}
