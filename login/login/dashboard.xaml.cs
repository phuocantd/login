using MyProg;
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

namespace login
{
    /// <summary>
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class dashboard : Window
    {

        List<item> arr = new List<item>();
        List<Canvas> canvas = new List<Canvas>();

        private void loadData()
        {
            string path = Directory.GetCurrentDirectory();
            path = path + @"\config.ini";
            IniFile MyIni = new IniFile(path);
            int sz;
            try
            {
                sz = Int32.Parse(MyIni.Read("szItem"));
            }
            catch
            {
                sz = 0;
                MyIni.Write("size", "0");
            }

            string url, name, salary;
            for (int i = 0; i < sz; i++)
            {
                url = MyIni.Read($"{i}", "urlItem");
                name = MyIni.Read($"{i}", "nameItem");
                salary = MyIni.Read($"{i}", "salaryItem");
                arr.Add(new item { nameItem = name, imgItem = url, salaryItem=salary });
            }
        }

        public dashboard()
        {
            InitializeComponent();
            loadData();
            displayItem();


        }

        private void displayItem()
        {
            foreach (var temp in arr)
            {
                Canvas display = new Canvas();
                display.Height = 350;

                //create image cell phone
                Image img = new Image();
                img.Height = 294;
                img.Width = 294;
                img.Source = new BitmapImage(new Uri($"/cell phone/{temp.imgItem}", UriKind.RelativeOrAbsolute));
                display.Children.Add(img);

                //create textblock name cell phone
                TextBlock txtName = new TextBlock();
                txtName.Width = 290;
                txtName.Text = temp.nameItem;
                Canvas.SetTop(txtName, 300);
                txtName.TextAlignment = TextAlignment.Center;
                txtName.FontSize = 25;
                txtName.FontFamily = new FontFamily("Axure Handwriting");
                txtName.FontWeight = FontWeights.Bold;
                display.Children.Add(txtName);

                //create textblock salary cell phone
                TextBlock txtSalary = new TextBlock();
                Color color = (Color)ColorConverter.ConvertFromString("#ccc");
                txtSalary.Foreground = new System.Windows.Media.SolidColorBrush(color);
                txtSalary.Width = 250;
                txtSalary.Text = temp.salaryItem;
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

public class item
{
    private string img;
    private string name;
    private string salary;
    public string imgItem
    {
        get { return img; }
        set { img = value; }
    }
    public string nameItem
    {
        get { return name; }
        set { name = value; }
    }
    public string salaryItem
    {
        get { return salary; }
        set { salary = value; }
    }
}