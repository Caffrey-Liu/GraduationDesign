using CAN;
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

        private static void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            Console.WriteLine("收到消息    ");
            FrameInfo message = e.CanFrameInfo;
            //Console.WriteLine("ID   " + message.FrameID);
            //Console.WriteLine("Data    " + message.Data);
            //throw new NotImplementedException();
        }

        //按钮按下改变颜色,各个按钮互不影响
        Dictionary<string, int> tags = new Dictionary<string, int>();
        CANhelper can = new CANhelper();
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
                if (btn.Name.Equals("power")) {
                    can.CanSetting.SetCAN((uint)0, //deviceIndex 设备索引号
                                                  (byte)0, //canIndex CAN的路索引号
                                                  (byte)CanFilterType.DualFilter, //filterType 过滤类型
                                                  (byte)CanMode.NormalMode, //canMode CAN模式
                                                  "00000000", //canCode 过滤码
                                                  "FFFFFFFF", //canMask 掩码
                                                  "00", //时间高位
                                                  "1C"); //时间低位,默认 4F 2F
                }
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
            if (btn.Name.Equals("power") && tags[btn.Name] == 1) {
                can.ConnectCANDevice(); //连接CAN设备
                Console.WriteLine("连接成功");
                can.StartCAN(); //启动CAN设备
                Console.WriteLine("启动成功");
                can.ReceviedData += Can_ReceviedData;
            }
            if (btn.Name.Equals("power") && tags[btn.Name] == 0) {
                can.CloseCANDevice();
                Console.WriteLine("关闭成功");
            }
        }
    }
}