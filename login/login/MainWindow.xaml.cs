using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Dictionary<string, string> acc = new Dictionary<string, string>();
        

        public MainWindow()
        {
            InitializeComponent();
            acc.Add("admin", "123");
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

        private void setNIL()
        {
            this.txtAccount_sUp.Text = "";
            this.txtAcount_sIn.Text = "";
            this.txtEmail_sUp.Text = "";
            this.txtPass_sIn.Password = "";
            this.txtPass_sUp.Password = "";
        }

        private void changeCanvas(Canvas cIn,Canvas cOut)
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
            setNIL();
        }

        private void lblLogin(object sender, MouseButtonEventArgs e)
        {
            string pass = "";
            if (acc.TryGetValue(txtAcount_sIn.Text, out pass))
            {
                if (txtPass_sIn.Password.ToString() == pass) 
                {
                    dashboard dash = new dashboard();
                    dash.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorect");
                }
            }
            else
            {
                MessageBox.Show("no account");
            }


        }

        private bool isValidkUsername(string st)
        {
            bool result = true;

            string pass = "";
            if (acc.TryGetValue(st, out pass))
            {
                result = false;
                MessageBox.Show("Username is already taken");
            }
            else
            {
                if (Regex.IsMatch(st, @"^[a-zA-Z0-9_]+$"))
                {

                }
                else
                {
                    result = false;
                    MessageBox.Show("Username may only contain alphanumeric characters or single hyphens, and cannot begin or end with a hyphen");
                }
            }

            return result;
        }

        private bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void lblRegister(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Register");
            if (isValidkUsername(txtAccount_sUp.Text))
            {
                //MessageBox.Show("success");
                if (isValidEmail(txtEmail_sUp.Text))
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("fail");
                }
                
            }
            else
            {
                //MessageBox.Show("fail");
            }

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txtAcount_sIn.Text = "You Entered: " + txtAcount_sIn.Text;
                MessageBox.Show("hh");
            }
        }
    }
}

