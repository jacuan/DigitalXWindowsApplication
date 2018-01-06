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
    /// Interaction logic for EditSubCategory.xaml
    /// </summary>
    public partial class EditSubCategory : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public EditSubCategory()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonEditSubCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsPage.OriginalSubCategoryName != textBoxSubCategoryEditSubCategoryWindow.Text)
            {
                var subCategoryName = (from scn in jackyDigitalXDBEntities.ProductSubCategories
                                       where scn.SubCategory == textBoxSubCategoryEditSubCategoryWindow.Text
                                       select scn);

                if (subCategoryName.Count() != 0)
                {
                    MessageBox.Show("Specified SubCategory Name already exists, please choose another name.");
                }
                else
                {
                    ProductsPage.ProductSubCategory.SubCategory = textBoxSubCategoryEditSubCategoryWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
            else
            {
                ProductsPage.ProductSubCategory.SubCategory = textBoxSubCategoryEditSubCategoryWindow.Text;
                SaveButtonPressed = true;
                this.Close();
            }
        }

        private void cancelButtonEditSubcategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
