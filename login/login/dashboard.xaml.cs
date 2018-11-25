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

            for (int i = 0; i < 14; i++)
            {
                Canvas display = new Canvas();
                display.Height = 350;

                //create image cell phone
                Image img = new Image();
                img.Height = 294;
                img.Width = 294;
                img.Source = new BitmapImage(new Uri(@"/cell phone/oppo-a7.jpg", UriKind.RelativeOrAbsolute));
                display.Children.Add(img);

                //create textblock name cell phone
                TextBlock txtName = new TextBlock();
                txtName.Width = 290;
                txtName.FontSize = 12;
                txtName.Text = "OPPO A7";
                Canvas.SetTop(txtName, 300);
                txtName.TextAlignment = TextAlignment.Center;
                txtName.FontSize = 30;
                txtName.FontFamily = new FontFamily("Axure Handwriting");
                txtName.FontWeight = FontWeights.Bold;
                display.Children.Add(txtName);

                //create textblock salary cell phone
                TextBlock txtSalary = new TextBlock();
                txtSalary.Width = 294;
                txtSalary.Text = "5.990.000đ";
                txtSalary.FontSize = 20;
                Canvas.SetTop(txtSalary, 10);
                txtSalary.TextAlignment = TextAlignment.Right;
                txtSalary.FontWeight = FontWeights.Bold;
                display.Children.Add(txtSalary);

                UFG.Children.Add(display);
            }


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
