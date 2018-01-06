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

namespace JackyFinalProject.RetailerInfo
{
    /// <summary>
    /// Interaction logic for RetailerInformation.xaml
    /// </summary>
    public partial class RetailerInformation : Window
    {
        public static int retailerID { get; set; }

        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public RetailerInformation()
        {
            InitializeComponent();

            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            RetailerList.ItemsSource = jackyDigitalXDBEntities.Retailers.ToList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {   Retailer retailer = RetailerList.SelectedItem as Retailer;
                retailerID = retailer.RetailerID;
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Please choose a Retailer");
            }

        }
    }
}
