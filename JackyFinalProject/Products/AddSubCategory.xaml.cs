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
    /// Interaction logic for AddSubCategory.xaml
    /// </summary>
    public partial class AddSubCategory : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool SaveButtonPressed { get; set; } = false;
        public AddSubCategory()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void cancelButtonAddSubcategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButtonAddSubCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
                var subCategoryName = (from scn in jackyDigitalXDBEntities.ProductSubCategories
                                    where scn.SubCategory == textBoxSubCategoryAddSubCategoryWindow.Text
                                    select scn);

                if (subCategoryName.Count() != 0)
                {
                    MessageBox.Show("Specified SubCategory Name already exists, please choose another name.");
                }
                else if (textBoxSubCategoryAddSubCategoryWindow.Text == null || textBoxSubCategoryAddSubCategoryWindow.Text == "")
                {
                    MessageBox.Show("Please specify a name.");
                }
                else
                {
                    ProductsPage.ProductSubCategory.SubCategory = textBoxSubCategoryAddSubCategoryWindow.Text;
                    ProductsPage.ProductSubCategory.SubCategoryID = Convert.ToInt32(textBlockSubCategoryIDAddSubCategoryWindow.Text);
                    ProductsPage.ProductSubCategory.CategoryID = Convert.ToInt32(textBlockCategoryIDAddSubCategoryWindow.Text);
                    SaveButtonPressed = true;
                    this.Close();
                }
        }
    }
}
