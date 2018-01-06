using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace JackyFinalProject.Products
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public bool SaveButtonPressed { get; set; } = false;

        public AddProduct()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void checkBoxIsDiscontinuedAddProductWindow_Checked(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.IsDiscontinued = true;
        }

        private void checkBoxIsDiscontinuedAddProductWindow_Unchecked(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.IsDiscontinued = false;
                
        }

        private void buttonAddProductPhotoProductPage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string imagePath = dlg.FileName;
            if (imagePath != null && imagePath != "")
            {
                byte[] imageByte = File.ReadAllBytes(imagePath);
                MemoryStream ms = new MemoryStream(imageByte);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                ImageSource imageSource = bitmapImage as ImageSource;
                imageProductAddProductWindow.Source = imageSource;
                ProductsPage.Product.Picture = imageByte;
            }
        }

        private void saveButtonAddProductWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var productName = (from pn in jackyDigitalXDBEntities.Products
                               where pn.ProductName == textBoxProductNameAddProductWindow.Text
                               select pn);

            if (productName.Count() != 0)
            {
                MessageBox.Show("Specified Product Name already exists, please choose another name.");
            }
            else
                {
                    while (textBoxRetailerIDEditProductWindow.Text == "0")
                    {
                        MessageBox.Show("Invalid Retailer ID, plese re-select a Retailer ID");
                        RetailerInfo.RetailerInformation retailerInformaiton = new RetailerInfo.RetailerInformation();
                        retailerInformaiton.ShowDialog();
                        textBoxRetailerIDEditProductWindow.Text = RetailerInfo.RetailerInformation.retailerID.ToString();
                    }

                ProductsPage.Product.ProductID = Convert.ToInt32(textBlockProductIDAddProductWindow.Text);
                ProductsPage.Product.RetailerID = Convert.ToInt32(textBoxRetailerIDEditProductWindow.Text);
                ProductsPage.Product.SubCategoryID = Convert.ToInt32(textBoxSubCategoryIDAddProductWindow.Text);
                ProductsPage.Product.ProductName = textBoxProductNameAddProductWindow.Text;
                ProductsPage.Product.ProductDescription = textBoxProductDescriptionAddProductWindow.Text;
                ProductsPage.Product.Price = Convert.ToInt32(textBoxPriceAddProductWindow.Text);
                ProductsPage.Product.UnitsInStock = Convert.ToInt32(textBoxUnitsInStockAddProductWindow.Text);
                SaveButtonPressed = true;
                this.Close();
            }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Invalid characters detected. Please verify all fields and try again. 
                    For instance, some fields (e.g., prices, units in stocks) allow only numbers; 
                    apart from product description, product photo and 'IsDiscountinued' options, all other fields cannot be empty.");
            }
        }

        private void cancelButtonAddProductWindow_Click(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.Picture = null;
            ProductsPage.Product.IsDiscontinued = null;
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            RetailerInfo.RetailerInformation retailerInformaiton = new RetailerInfo.RetailerInformation();
            retailerInformaiton.ShowDialog();
            textBoxRetailerIDEditProductWindow.Text = RetailerInfo.RetailerInformation.retailerID.ToString();
            
        }
    }
}