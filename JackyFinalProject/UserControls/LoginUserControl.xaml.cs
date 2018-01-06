using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JackyFinalProject.UserControls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public event RoutedEventHandler LoginClicked;
        public LoginUserControl()
        {
            InitializeComponent();
            LoadCompanyName();
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
        }

        void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new Action(delegate ()
                {
                    userNameTextBox.Focus();
                }));
            }
        }

        public void LoadCompanyName()
        {
            FormattedText formattedText = new FormattedText(
               "DigitalX", CultureInfo.GetCultureInfo("en-us"),
               FlowDirection.LeftToRight, new Typeface("Verdana"), 100, Brushes.Blue);
            formattedText.SetFontWeight(FontWeights.Bold);
            Geometry geometry = formattedText.BuildGeometry(new Point(0, 0));
            PathGeometry pathGeometry = geometry.GetFlattenedPathGeometry();
            path.Data = pathGeometry;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length == 0)
            {
                passwordBox.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
            else
            {
                passwordBox.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userNameTextBox.Text.Length == 0)
            {
                userNameTextBox.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
            else
            {
                userNameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 128, 185, 255));
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginClicked != null)
            {
                LoginClicked(this, new LoginClickedArgs()
                {
                    UserName = userNameTextBox.Text,
                    Password = passwordBox.Password
                });
            }
        }
    }

    public class LoginClickedArgs : RoutedEventArgs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
