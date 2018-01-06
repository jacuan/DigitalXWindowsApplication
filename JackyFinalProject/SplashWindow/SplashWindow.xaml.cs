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
using System.Windows.Shapes;

namespace JackyFinalProject.SplashWindow
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            LoadCompanyName();
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
    }
}
