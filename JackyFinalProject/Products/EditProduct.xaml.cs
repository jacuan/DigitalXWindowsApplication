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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public EditProduct()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonEditProductWindow_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsPage.OriginalProductName != textBoxProductNameEditProductWindow.Text)
            {
                var productName = (from pn in jackyDigitalXDBEntities.Products
                                   where pn.ProductName == textBoxProductNameEditProductWindow.Text
                                   select pn);

                if (productName.Count() != 0)
                {
                    MessageBox.Show("Specified Product Name already exists, please choose another name.");
                }
                else
                {
                    ProductsPage.Product.ProductName = textBoxProductNameEditProductWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else
            {
                ProductsPage.Product.ProductName = textBoxProductNameEditProductWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
        }

        private void cancelButtonEditProductWindow_Click(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.Picture = ProductsPage.OriginalPicture;
            this.Close();
        }

        private void checkBoxIsDiscontinuedEditProductWindow_Checked(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.IsDiscontinued = true;
        }

        private void checkBoxIsDiscontinuedEditProductWindow_Unchecked(object sender, RoutedEventArgs e)
        {
            ProductsPage.Product.IsDiscontinued = false;
        }

        private void buttonEditProductPhotoProductPage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string imagePath = dlg.FileName;
            if (imagePath != null && imagePath != "")
            {
                byte[]imageByte = File.ReadAllBytes(imagePath);
                MemoryStream ms = new MemoryStream(imageByte);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                ImageSource imageSource = bitmapImage as ImageSource;
                imageProductEditProductWindow.Source = imageSource;
                ProductsPage.Product.Picture = imageByte;
            }
        }
    }
}
