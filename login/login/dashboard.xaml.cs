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

namespace login
{
    /// <summary>
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class dashboard : Window
    {
        struct item
        {
            public string image;
            public string salary;
            public string name;
            public item(string i, string s, string n)
            {
                image = i;
                salary = s;
                name = n;
            }

        }

        item[] arr = new item[14];

        public dashboard()
        {
            InitializeComponent();

            


        }

        /// <summary>
        /// di chuyển màn hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDashboard(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// quay trở lại giao diện sign in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gotoLogin(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        /// <summary>
        /// tắt dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void turnOffDashBoard(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
