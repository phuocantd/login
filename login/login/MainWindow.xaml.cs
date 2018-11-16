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
using System.Windows.Media.Animation;
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

        private void lblSignIn(object sender, MouseButtonEventArgs e)
        {
            changeCanvas(SignIn, SignUp);
            this.lblTitle.Content = "Register";
        }

        private void lblSignUp(object sender, MouseButtonEventArgs e)
        {
            changeCanvas(SignUp, SignIn);
            this.lblTitle.Content = "Login";
        }

        

        static void changeCanvas(Canvas cIn,Canvas cOut)
        {
            Canvas IN, OUT;
            IN = cIn;
            DoubleAnimation hidden = new DoubleAnimation();
            hidden.From = 0;
            hidden.To = -450;
            hidden.Duration = TimeSpan.FromSeconds(0.75);
            IN.BeginAnimation(Canvas.LeftProperty, hidden);

            OUT = cOut;
            DoubleAnimation show = new DoubleAnimation();
            show.From = 450;
            show.To = 0;
            show.Duration = TimeSpan.FromSeconds(0.75);
            OUT.BeginAnimation(Canvas.LeftProperty, show);
            OUT.Visibility = Visibility.Visible;
        }

        private void lblLogin(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Login");
        }

        private void lblRegister(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Register");
        }
    }
}
