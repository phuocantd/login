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
        /// <summary>
        /// cau truc cua 1 tai khoan mail
        /// </summary>
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
        
        
        /// <summary>
        /// doc du lieu tu file config.ini
        /// </summary>
        private void loadData()
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\config.ini";
            IniFile MyIni = new IniFile(path);

            int sz;
            try
            {
                sz = Int32.Parse(MyIni.Read("size"));
            }
            catch
            {
                sz = 0;
                MyIni.Write("size", "0");
            }

            string user, email, pass;
            for (int i = 0; i < sz; i++)
            {
                user = MyIni.Read($"{i}", "username");
                email = MyIni.Read("${i}", "email");
                pass = MyIni.Read($"{i}", "password");
                acc.Add(user, new mail(email, pass));
            }
        }

        /// <summary>
        /// luu du lieu vao file config.ini
        /// </summary>
        /// <param name="user">ten tai khoan</param>
        /// <param name="email">ten email</param>
        /// <param name="pass">mat khau</param>
        private void saveData(string user,string email,string pass)
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\config.ini";
            IniFile MyIni = new IniFile(path);

            int sz;
            try
            {
                sz = Int32.Parse(MyIni.Read("size"));
            }
            catch
            {
                sz = 0;
                MyIni.Write("size", "0");
            }

            MyIni.Write($"{sz}", user, "username");
            MyIni.Write($"{sz}", email, "email");
            MyIni.Write($"{sz}", pass, "password");
            sz++;
            MyIni.Write("size", $"{sz}");
        }

        /// <summary>
        /// main
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            loadData();
            //acc.Add("admin", new mail("admin@gmail.com","123"));
        }

        /// <summary>
        /// di chuyen chuot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveMouse(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// xu ly su kien tat chuong trinh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ẩn chuong trinh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblMinimize_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// đổi màn hình sign in sang sign up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSignIn(object sender, MouseButtonEventArgs e)
        {
            changeCanvas(SignIn, SignUp);
            this.lblTitle.Content = "Register";
        }

        /// <summary>
        /// đổi màn hình sign up sang sign in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSignUp(object sender, MouseButtonEventArgs e)
        {
            changeCanvas(SignUp, SignIn);
            this.lblTitle.Content = "Login";
        }

        /// <summary>
        /// đặt tất cả text box lại bằng rỗng
        /// </summary>
        private void setNIL()
        {
            this.txtAccount_sUp.Text = "";
            this.txtAcount_sIn.Text = "";
            this.txtEmail_sUp.Text = "";
            this.txtPass_sIn.Password = "";
            this.txtPass_sUp.Password = "";
        }

        /// <summary>
        /// thay đổi 2 canvas hiển thị ra màn hình
        /// </summary>
        /// <param name="cIn">canvas sẽ ẩn</param>
        /// <param name="cOut">canvas sẽ hiện</param>
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

        /// <summary>
        /// kiểm tra account nếu thành công sẽ mở dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// kiểm tra user hợp lệ
        /// </summary>
        /// <param name="st">tên user</param>
        /// <returns></returns>
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

        /// <summary>
        /// kiểm tra email hợp lệ
        /// </summary>
        /// <param name="email">tên email</param>
        /// <returns></returns>
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

        /// <summary>
        /// kiểm tra password hợp lệ
        /// </summary>
        /// <param name="st">chuỗi password</param>
        /// <returns></returns>
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

        /// <summary>
        /// kiểm tra đăng kí tài khoản nếu thành công sẽ mở dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        
    }
}


namespace MyProg
{
    /// <summary>
    /// lớp xử lý file ini
    /// </summary>
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
