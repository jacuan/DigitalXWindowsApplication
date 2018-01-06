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

namespace JackyFinalProject.Products
{
    /// <summary>
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditCategory()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonEditCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsPage.OriginalCategoryName != textBoxCategoryEditCategoryWindow.Text)
            {
                var categoryName = (from cn in jackyDigitalXDBEntities.ProductCategories
                                    where cn.Category == textBoxCategoryEditCategoryWindow.Text
                                    select cn);

                if (categoryName.Count() != 0)
                {
                    MessageBox.Show("Specified Category Name already exists, please choose another name.");
                }
                else
                {
                    ProductsPage.ProductCategory.Category = textBoxCategoryEditCategoryWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else
            {
                ProductsPage.ProductCategory.Category = textBoxCategoryEditCategoryWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
        }

        private void cancelButtonEditCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
