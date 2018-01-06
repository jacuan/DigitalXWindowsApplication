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

namespace JackyFinalProject.AddressInfo
{
    /// <summary>
    /// Interaction logic for AddressInformation.xaml
    /// </summary>
    public partial class AddressInformation : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public int AddressTypeInt { get; set; }
        public bool AddressTypeSelected { get; set; } = false;
        public AddressInformation()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
            listViewAddressInformationAddressInformationWindow.ItemsSource = jackyDigitalXDBEntities.AddressTypes.ToList();
        }

        private void selectButtonAddressInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            if (listViewAddressInformationAddressInformationWindow.SelectedItem != null)
            {
                AddressTypeSelected = true;
                AddressType addressType = listViewAddressInformationAddressInformationWindow.SelectedItem as AddressType;
                AddressTypeInt = addressType.TypeID;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please choose an Address Type first!");
            }
        }
    }
}
