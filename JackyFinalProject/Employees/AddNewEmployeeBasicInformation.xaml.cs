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

namespace JackyFinalProject.Employees
{
    /// <summary>
    /// Interaction logic for AddNewEmployeeBasicInformation.xaml
    /// </summary>
    public partial class AddNewEmployeeBasicInformation : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;
        public bool SaveButtonPressed { get; set; } = false;

        byte[] imageByte;
        public AddNewEmployeeBasicInformation()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void buttonCancelCreateNewEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSaveCreateNewEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUsernameCreateNewEEmployeeBasicInformation.Text != null && textBoxUsernameCreateNewEEmployeeBasicInformation.Text != "")
            {
                var employeeUserName = (from eun in jackyDigitalXDBEntities.Employees
                                        where eun.Username == textBoxUsernameCreateNewEEmployeeBasicInformation.Text
                                        select eun);


                if (employeeUserName.Count() != 0)
                {
                    MessageBox.Show("Specified Employee Username already exits, please choose another name.");
                }
                else if ((textBoxFirstNameCreateNewEmployeeBasicInformation.Text == null || textBoxFirstNameCreateNewEmployeeBasicInformation.Text == "" ||
                        textBoxLastNameCreateNewEmployeeBasicInformation.Text == null || textBoxLastNameCreateNewEmployeeBasicInformation.Text == ""))
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else if (passwordBoxAddNewEmployeeBasicInformation.Password != passwordBoxConfirmNewEmployeeBasicInformation.Password)
                {
                    MessageBox.Show("Passwords entered are NOT match! Please re-enter password.");
                    passwordBoxAddNewEmployeeBasicInformation.Password = "";
                    passwordBoxConfirmNewEmployeeBasicInformation.Password = "";
                }
                else
                {
                    if (passwordBoxAddNewEmployeeBasicInformation.Password == null || passwordBoxAddNewEmployeeBasicInformation.Password == "")
                    {
                        EmployeesPage.Emp.EmployeeID = Convert.ToInt32(textBlockEmployeeIDCreateNewEmployeeBasicInformation.Text);
                        EmployeesPage.Emp.FirstName = textBoxFirstNameCreateNewEmployeeBasicInformation.Text;
                        EmployeesPage.Emp.LastName = textBoxLastNameCreateNewEmployeeBasicInformation.Text;
                        EmployeesPage.Emp.Username = textBoxUsernameCreateNewEEmployeeBasicInformation.Text;
                        EmployeesPage.Emp.Password = null;
                        EmployeesPage.Emp.EmployeeImage = imageByte;
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else
                    {
                        if (passwordBoxAddNewEmployeeBasicInformation.Password.Length <= 8)
                        {
                            EmployeesPage.Emp.EmployeeID = Convert.ToInt32(textBlockEmployeeIDCreateNewEmployeeBasicInformation.Text);
                            EmployeesPage.Emp.FirstName = textBoxFirstNameCreateNewEmployeeBasicInformation.Text;
                            EmployeesPage.Emp.LastName = textBoxLastNameCreateNewEmployeeBasicInformation.Text;
                            EmployeesPage.Emp.Username = textBoxUsernameCreateNewEEmployeeBasicInformation.Text;
                            EmployeesPage.Emp.Password = passwordBoxAddNewEmployeeBasicInformation.Password;
                            EmployeesPage.Emp.EmployeeImage = imageByte;
                            SaveButtonPressed = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Password is too long. The maximum length of password is 8 characters.");
                        }
                    }
                }
            }

            else
            {
                if ((textBoxFirstNameCreateNewEmployeeBasicInformation.Text == null || textBoxFirstNameCreateNewEmployeeBasicInformation.Text == "" ||
                        textBoxLastNameCreateNewEmployeeBasicInformation.Text == null || textBoxLastNameCreateNewEmployeeBasicInformation.Text == ""))
                {
                    MessageBox.Show("Fields for 'First Name' and 'Last Name' cannot be empty.");
                }
                else
                {
                    EmployeesPage.Emp.EmployeeID = Convert.ToInt32(textBlockEmployeeIDCreateNewEmployeeBasicInformation.Text);
                    EmployeesPage.Emp.FirstName = textBoxFirstNameCreateNewEmployeeBasicInformation.Text;
                    EmployeesPage.Emp.LastName = textBoxLastNameCreateNewEmployeeBasicInformation.Text;
                    EmployeesPage.Emp.Username = textBoxUsernameCreateNewEEmployeeBasicInformation.Text;
                    EmployeesPage.Emp.Password = passwordBoxAddNewEmployeeBasicInformation.Password;
                    EmployeesPage.Emp.EmployeeImage = imageByte;
                    SaveButtonPressed = true;
                    this.Close();
                }
            }
        }

        private void editPictureButtonCreateNewEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string imagePath = dlg.FileName;
            if (imagePath != null && imagePath != "")
            {
                imageByte = File.ReadAllBytes(imagePath);
                MemoryStream ms = new MemoryStream(imageByte);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                ImageSource imageSource = bitmapImage as ImageSource;
                imageEmployeeCreateNewEmployeeBasicInformation.Source = imageSource;
            }
        }

        private void textBoxUsernameCreateNewEEmployeeBasicInformation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxUsernameCreateNewEEmployeeBasicInformation.Text != null && textBoxUsernameCreateNewEEmployeeBasicInformation.Text != "")
            {
                passwordBoxAddNewEmployeeBasicInformation.Visibility = Visibility.Visible;
                passwordBoxConfirmNewEmployeeBasicInformation.Visibility = Visibility.Visible;
            }
            else if (textBoxUsernameCreateNewEEmployeeBasicInformation.Text == null || textBoxUsernameCreateNewEEmployeeBasicInformation.Text == "")
            {
                passwordBoxAddNewEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                passwordBoxConfirmNewEmployeeBasicInformation.Visibility = Visibility.Collapsed;
            }
        }
    }
}
