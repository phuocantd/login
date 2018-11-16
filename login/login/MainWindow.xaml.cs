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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void moveMouse(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void lblClose_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblMinimize_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {

        }

        private void lblSignIn(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblSignUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnRegister(object sender, RoutedEventArgs e)
        {

        }
    }
}
