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
    /// Interaction logic for EditEmployeeBasicInformation.xaml
    /// </summary>
    public partial class EditEmployeeBasicInformation : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool SaveButtonPressed { get; set; } = false;

        byte[] imageByte;
        public EditEmployeeBasicInformation()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void buttonSaveEditEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
        {
            if ((EmployeesPage.OriginalEmployeeUsername != textBoxUsernameEditEmployeeBasicInformation.Text) && 
                (textBoxUsernameEditEmployeeBasicInformation.Text != null && textBoxUsernameEditEmployeeBasicInformation.Text != ""))
            {
                var userName = (from un in jackyDigitalXDBEntities.Employees
                                where un.Username == textBoxUsernameEditEmployeeBasicInformation.Text
                                select un);

                if (userName.Count() != 0)
                {
                    MessageBox.Show("Specified Username already exists, please choose another name.");
                }
                else
                {
                    if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Collapsed)
                    {
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Visible)
                    {
                        if ((passwordBoxEditEmployeeBasicInformation.Password != null && passwordBoxEditEmployeeBasicInformation.Password != "") && 
                            (passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password) && 
                            (passwordBoxEditEmployeeBasicInformation.Password.Length <=8))
                        {
                            EmployeesPage.Emp.Password = passwordBoxEditEmployeeBasicInformation.Password;
                            SaveButtonPressed = true;
                            this.Close();
                        }
                        else if ((passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password) &&
                            (passwordBoxEditEmployeeBasicInformation.Password.Length > 8))
                        {
                            MessageBox.Show("Password is too long. The maximum length of password is 8 characters.");
                        }
                        else if ((passwordBoxEditEmployeeBasicInformation.Password != null && passwordBoxEditEmployeeBasicInformation.Password != "") &&
                            (passwordBoxEditEmployeeBasicInformation.Password != passwordBoxConfirmEditEmployeeBasicInformation.Password))
                        {
                            passwordBoxEditEmployeeBasicInformation.Password = "";
                            passwordBoxConfirmEditEmployeeBasicInformation.Password = "";
                            MessageBox.Show("Passwords entered are NOT match! Please re-enter password.");
                        }
                        else if ((passwordBoxEditEmployeeBasicInformation.Password == null || passwordBoxEditEmployeeBasicInformation.Password == "") &&
                            (passwordBoxEditEmployeeBasicInformation.Password != passwordBoxConfirmEditEmployeeBasicInformation.Password))
                        {
                            passwordBoxEditEmployeeBasicInformation.Password = "";
                            passwordBoxConfirmEditEmployeeBasicInformation.Password = "";
                            MessageBox.Show("Passwords entered are NOT match! Please re-enter password.");
                        }
                        else if ((passwordBoxEditEmployeeBasicInformation.Password == null || passwordBoxEditEmployeeBasicInformation.Password == "") &&
                            (passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password))
                        {
                            MessageBox.Show("Password cannot be empty. If you do not want to set/change password, please press 'Change / Unchange Password' button again.");
                        }
                    }
                }
            }
            else if ((EmployeesPage.OriginalEmployeeUsername != textBoxUsernameEditEmployeeBasicInformation.Text) &&
                (textBoxUsernameEditEmployeeBasicInformation.Text == null || textBoxUsernameEditEmployeeBasicInformation.Text == ""))
            {
                SaveButtonPressed = true;
                this.Close();
            }
            else if (EmployeesPage.OriginalEmployeeUsername == textBoxUsernameEditEmployeeBasicInformation.Text)
            {
                if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Collapsed)
                {
                    SaveButtonPressed = true;
                    this.Close();
                }
                else if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Visible)
                {
                    if ((passwordBoxEditEmployeeBasicInformation.Password != null && passwordBoxEditEmployeeBasicInformation.Password != "") &&
                        (passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password) &&
                        (passwordBoxEditEmployeeBasicInformation.Password.Length <= 8))
                    {
                        EmployeesPage.Emp.Password = passwordBoxEditEmployeeBasicInformation.Password;
                        SaveButtonPressed = true;
                        this.Close();
                    }
                    else if ((passwordBoxEditEmployeeBasicInformation.Password != null && passwordBoxEditEmployeeBasicInformation.Password != "") &&
                        (passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password) &&
                        (passwordBoxEditEmployeeBasicInformation.Password.Length > 8))
                    {
                        MessageBox.Show("Password is too long. The maximum length of password is 8 characters.");
                    }
                    else if ((passwordBoxEditEmployeeBasicInformation.Password != null && passwordBoxEditEmployeeBasicInformation.Password != "") &&
                        (passwordBoxEditEmployeeBasicInformation.Password != passwordBoxConfirmEditEmployeeBasicInformation.Password))
                    {
                        passwordBoxEditEmployeeBasicInformation.Password = "";
                        passwordBoxConfirmEditEmployeeBasicInformation.Password = "";
                        MessageBox.Show("Passwords entered are NOT match! Please re-enter password.");
                    }
                    else if ((passwordBoxEditEmployeeBasicInformation.Password == null || passwordBoxEditEmployeeBasicInformation.Password == "") &&
                        (passwordBoxEditEmployeeBasicInformation.Password != passwordBoxConfirmEditEmployeeBasicInformation.Password))
                    {
                        passwordBoxEditEmployeeBasicInformation.Password = "";
                        passwordBoxConfirmEditEmployeeBasicInformation.Password = "";
                        MessageBox.Show("Passwords entered are NOT match! Please re-enter password.");
                    }
                    else if ((passwordBoxEditEmployeeBasicInformation.Password == null || passwordBoxEditEmployeeBasicInformation.Password == "") &&
                        (passwordBoxEditEmployeeBasicInformation.Password == passwordBoxConfirmEditEmployeeBasicInformation.Password))
                    {
                        MessageBox.Show("Password cannot be empty. If you do not want to set/change password, please press 'Change / Unchange Password' button again.");
                    }
                }
            }
        }

        private void buttonCancelEditEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
        {
            EmployeesPage.Emp.EmployeeImage = EmployeesPage.OriginalPicture;
            this.Close();
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Visible && passwordBoxConfirmEditEmployeeBasicInformation.Visibility == Visibility.Visible)
            {
                passwordBoxEditEmployeeBasicInformation.Password = "";
                passwordBoxConfirmEditEmployeeBasicInformation.Password = "";
                passwordBoxEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
                passwordBoxConfirmEditEmployeeBasicInformation.Visibility = Visibility.Collapsed;
            }
            else if (passwordBoxEditEmployeeBasicInformation.Visibility == Visibility.Collapsed && passwordBoxConfirmEditEmployeeBasicInformation.Visibility == Visibility.Collapsed)
            {
                if (textBoxUsernameEditEmployeeBasicInformation.Text == null || textBoxUsernameEditEmployeeBasicInformation.Text == "")
                {
                    MessageBox.Show("Please specify a username first!");
                }
                else
                {
                    passwordBoxEditEmployeeBasicInformation.Visibility = Visibility.Visible;
                    passwordBoxConfirmEditEmployeeBasicInformation.Visibility = Visibility.Visible;
                }
            }
        }

        private void textBoxUsernameEditEmployeeBasicInformation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxUsernameEditEmployeeBasicInformation.Text == "" || textBoxUsernameEditEmployeeBasicInformation.Text == null)
            {
                MessageBox.Show("If a USERNAME is NOT supplied, the associated previous password (if any) will be deleted. If you want to keep previous password (if any), then please supply a valid username.");
                EmployeesPage.Emp.Password = null;
            }
            else
            {
                EmployeesPage.Emp.Password = EmployeesPage.OriginalPassword;
            }
        }

        private void editPhotoButtonEditEmployeeBasicInformation_Click(object sender, RoutedEventArgs e)
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
                imageEmployeeEditEmployeeBasicInformation.Source = imageSource;
                EmployeesPage.Emp.EmployeeImage = imageByte;
            }
        }
    }
}
