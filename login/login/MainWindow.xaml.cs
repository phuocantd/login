using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using MyProg;


namespace login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        struct mail
        {
            public string email;
            public string pass;
            public mail(string e, string p)
            {
                email = e;
                pass = p;
            }
        }
        Dictionary<string, mail> acc = new Dictionary<string, mail>();
        
        

        private void loadData()
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\config.ini";
            IniFile MyIni = new IniFile(path);

            int sz = Int32.Parse(MyIni.Read("size"));
            string user, email, pass;
            for (int i = 0; i < sz; i++)
            {
                user = MyIni.Read($"{i}", "username");
                email = MyIni.Read("${i}", "email");
                pass = MyIni.Read($"{i}", "password");
                acc.Add(user, new mail(email, pass));
            }
        }

        private void saveData(string user,string email,string pass)
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\config.ini";
            IniFile MyIni = new IniFile(path);

            int sz = Int32.Parse(MyIni.Read("size"));
            MyIni.Write($"{sz}", user, "username");
            MyIni.Write($"{sz}", email, "email");
            MyIni.Write($"{sz}", pass, "password");
            sz++;
            MyIni.Write("size", $"{sz}");
        }

        public MainWindow()
        {
            InitializeComponent();
            loadData();
            //acc.Add("admin", new mail("admin@gmail.com","123"));
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
            mail pass;
            if (acc.TryGetValue(txtAcount_sIn.Text, out pass))
            {
                if (txtPass_sIn.Password.ToString() == pass.pass) 
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

            mail pass;
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
            bool result = true;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                result = addr.Address == email;
                foreach (KeyValuePair<string, mail> item in acc)
                {
                    if (item.Value.email == email)
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        private bool isValidPassword(string st)
        {
            bool result = true;

            //Make sure it's more than 15 characters, or at least 8 characters, including at least one digit, 
            //at least one alphabetic character, no special characters
            if (Regex.IsMatch(st, @"(?!^[0-9]*$)(?!^[A-zA-Z]*$)^([a-zA-Z0-9]{8,})$"))
            {

            }
            else
            {
                result = false;
            }

            return result;
        }

        private void lblRegister(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Register");
            if (isValidkUsername(txtAccount_sUp.Text))
            {
                //MessageBox.Show("success");
                if (isValidEmail(txtEmail_sUp.Text))
                {
                    if (isValidPassword(txtPass_sUp.Password.ToString()))
                    {
                        //success
                        //acc.Add(txtAccount_sUp.Text, new mail(txtEmail_sUp.Text, txtPass_sUp.Password.ToString()));
                        saveData(txtAccount_sUp.Text, txtEmail_sUp.Text, txtPass_sUp.Password.ToString());
                        dashboard dash = new dashboard();
                        dash.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password is invalid. Make sure it's more than 15 characters, or at least 8 characters, including at least one digit, at least one alphabetic character, no special characters");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Email is invalid or already taken");
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

namespace MyProg
{
    class IniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
