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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public bool SaveButtonPressed { get; set; } = false;

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public AddCategory()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void saveButtonAddCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
                var categoryName = (from cn in jackyDigitalXDBEntities.ProductCategories
                                    where cn.Category == textBoxCategoryAddCategoryWindow.Text
                                    select cn);

                if (categoryName.Count() != 0)
                {
                    MessageBox.Show("Specified Category Name already exists, please choose another name.");
                }
                 else if (textBoxCategoryAddCategoryWindow.Text == null || textBoxCategoryAddCategoryWindow.Text == "")
                {
                  MessageBox.Show("Please specify a name.");
                }
                else
                {
                    ProductsPage.ProductCategory.Category = textBoxCategoryAddCategoryWindow.Text;
                    SaveButtonPressed = true;
                    this.Close();
                }
        }

        private void cancelButtonAddCategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
