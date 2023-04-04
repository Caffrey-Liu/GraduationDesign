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
using System.Windows.Interop;

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

        static AirConditionInfo airConditionInfo = new AirConditionInfo();
        private static void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
           // Console.WriteLine("收到消息");
            FrameInfo message = e.CanFrameInfo;
            if (e.CanFrameInfo.FrameID.Equals("00000101")) {
                airConditionInfo.STR_TAM("" + message.Data[0] + message.Data[1]);
                airConditionInfo.STR_TR("" + message.Data[2] + message.Data[3]);
                airConditionInfo.STR_TE("" + message.Data[4] + message.Data[5] + message.Data[6] + message.Data[7]);
                airConditionInfo.STR_RH("" + message.Data[8] + message.Data[9] + message.Data[10] + message.Data[11]);
                airConditionInfo.STR_TS("" + message.Data[12] + message.Data[13]);
                //Console.WriteLine(airConditionInfo.TAM);
                //Console.WriteLine(airConditionInfo.TR);
                //Console.WriteLine(airConditionInfo.TE);
                //Console.WriteLine(airConditionInfo.RH);
                //Console.WriteLine(airConditionInfo.TS);
            }
            if (e.CanFrameInfo.FrameID.Equals("00000102")) {
                airConditionInfo.STR_BLMLVL("" + message.Data[1]);
                airConditionInfo.STR_AIRMIXDR("" + message.Data[2] + message.Data[3]);
                airConditionInfo.STR_AIRMIXPA("" + message.Data[4] + message.Data[5]);
                airConditionInfo.STR_INLET("" + message.Data[6] + message.Data[7]);
                airConditionInfo.STR_OUTLET("" + message.Data[8] + message.Data[9]);
                airConditionInfo.STR_OUTLETDIR("" + message.Data[10]);
                airConditionInfo.STR_INLETDIR("" + message.Data[11]);
                airConditionInfo.STR_RRDEFSTATUS("" + message.Data[13]);
            }
            RefreshPanel();
        }

        private static void RefreshPanel() 
        {
            //TODO 将变量和面板连接
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
                                                  "1C"); //时间低位,默认 00 1C 500Mps
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
                //can.ResetCANDevice();
                can.CloseCANDevice();
                Console.WriteLine("关闭成功");
                can.ReceviedData -= Can_ReceviedData;
            }
            //can.SendData("00000222", DateTime.Now.ToString(), 0, 0, "61 62 63 64 65 66 67 68", 0);
        }

        CanDetail CD = new CanDetail();
        int CDtag = 0;
        private void showCANDetail(object sender, RoutedEventArgs e) {
            if (CDtag == 0)
            {
                CD.PassDataBetweenForm += new CanDetail.PassDataBetweenFormHandler(sendFrameData);
                can.ReceviedData += CD.Can_ReceviedData;
                CD.Show();
                WindowInteropHelper parentHelper = new WindowInteropHelper(this);
                WindowInteropHelper childHelper = new WindowInteropHelper(CD);

                Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);

                CD.WindowState = WindowState.Normal; 
                CDtag = 1;
            }
            else {
                can.ReceviedData -= CD.Can_ReceviedData;
                CD.PassDataBetweenForm -= new CanDetail.PassDataBetweenFormHandler(sendFrameData);
                CD.Hide();
                CDtag = 0;
            }        
        }

        private void sendFrameData(object sender, PassDataWinFormEventArgs e)
        {
            Console.WriteLine(" ID "+e.frameID + "  Data " + e.frameData);
            for (int i = 0; i <= e.frameNumber; i++) {
                can.SendData(e.frameID, DateTime.Now.ToString(), 0, 0, e.frameData, 0);
            }
        }
    }
}