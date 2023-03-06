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
using static System.Net.Mime.MediaTypeNames;

namespace CANController
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //按钮按下改变颜色,各个按钮互不影响
        Dictionary<string, int> tags = new Dictionary<string, int>();
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int tag;
            if (tags.ContainsKey(btn.Name)){
                tag = (tags[btn.Name] + 1) % 2;
                tags[btn.Name] = tag;
            }
            else {
                tag = 1;
                tags.Add(btn.Name, tag);
            }
            if (tag == 0) { 
                btn.Background = new SolidColorBrush(Colors.White);
                Console.WriteLine(btn.Name + "关闭");
            }    
            else{
                //btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#18dcff"));
                btn.Background = new SolidColorBrush(Colors.LightSkyBlue);
                Console.WriteLine(btn.Name + "开启");
            }
        }
    }
}