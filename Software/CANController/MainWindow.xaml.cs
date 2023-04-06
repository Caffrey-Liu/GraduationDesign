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
using System.ComponentModel;

namespace CANController
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 定义各种面板变量,实现接口
        public AirConditionInfo airConditionInfo = new AirConditionInfo();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private String _OutCyclePath = "./Image/OutCycle_off.png";
        public String OutCyclePath
        {
            get { return _OutCyclePath; }
            set
            {
                _OutCyclePath = value;
                OnPropertyChanged("OutCyclePath");
            }
        }

        private String _InnerCyclePath = "./Image/InnerCycle_off.png";
        public String InnerCyclePath
        {
            get { return _InnerCyclePath; }
            set
            {
                _InnerCyclePath = value;
                OnPropertyChanged("InnerCyclePath");
            }
        }

        private String _FrontGlassPath = "./Image/FrontGlass_off.png";
        public String FrontGlassPath
        {
            get { return _FrontGlassPath; }
            set
            {
                _FrontGlassPath = value;
                OnPropertyChanged("FrontGlassPath");
            }
        }

        private String _BackGlassPath = "./Image/BackGlass_off.png";
        public String BackGlassPath
        {
            get { return _BackGlassPath; }
            set
            {
                _BackGlassPath = value;
                OnPropertyChanged("BackGlassPath");
            }
        }
        private String _PowerPath = "./Image/Power_off.png";
        public String PowerPath
        {
            get { return _PowerPath; }
            set
            {
                _PowerPath = value;
                OnPropertyChanged("PowerPath");
            }
        }
        private String _FacePath = "./Image/Face_off.png";
        public String FacePath
        {
            get { return _FacePath; }
            set
            {
                _FacePath = value;
                OnPropertyChanged("FacePath");
            }
        }
        private String _BLPath = "./Image/BL_off.png";
        public String BLPath
        {
            get { return _BLPath; }
            set
            {
                _BLPath = value;
                OnPropertyChanged("BLPath");
            }
        }
        private String _FootPath = "./Image/Foot_off.png";
        public String FootPath
        {
            get { return _FootPath; }
            set
            {
                _FootPath = value;
                OnPropertyChanged("FootPath");
            }
        }
        private String _FDPath = "./Image/FD_off.png";
        public String FDPath
        {
            get { return _FDPath; }
            set
            {
                _FDPath = value;
                OnPropertyChanged("FDPath");
            }
        }
        private String _BLWPath = "./Image/BLW0.png";
        public String BLWPath
        {
            get { return _BLWPath; }
            set
            {
                _BLWPath = value;
                OnPropertyChanged("BLWPath");
            }
        }
        #endregion
        private void RefreshPanel()
        {
            //刷新电源
            if (airConditionInfo.POWER == 0)
            {
                PowerPath = "./Image/Power_off.png";
            }
            else {
                PowerPath = "./Image/Power_on.png";
            }
            //刷新内循环外循环
            if (airConditionInfo.POWER == 0) {
                InnerCyclePath = "./Image/InnerCycle_off.png";
                OutCyclePath = "./Image/OutCycle_off.png";
            }
            else if (airConditionInfo.INLETDIR == 0){
                InnerCyclePath = "./Image/InnerCycle_on.png";
                OutCyclePath = "./Image/OutCycle_off.png";
            }
            else {
                InnerCyclePath = "./Image/InnerCycle_off.png";
                OutCyclePath = "./Image/OutCycle_on.png";
            }
            //刷新风向
            if (airConditionInfo.RRDEFSTATUS == 0)
            {
                BackGlassPath = "./Image/BackGlass_off.png";
            }
            else {
                BackGlassPath = "./Image/BackGlass_on.png";
            }
            if (airConditionInfo.POWER == 0)
            {
                FacePath = "./Image/Face_off.png";
                BLPath = "./Image/BL_off.png";
                FootPath = "./Image/Foot_off.png";
                FDPath = "./Image/FD_off.png";
                FrontGlassPath = "./Image/FrontGlass_off.png";
            }
            else
            {
                if (airConditionInfo.OUTLETDIR == 0)
                {
                    FacePath = "./Image/Face_on.png";
                    BLPath = "./Image/BL_off.png";
                    FootPath = "./Image/Foot_off.png";
                    FDPath = "./Image/FD_off.png";
                    FrontGlassPath = "./Image/FrontGlass_off.png";
                }
                if (airConditionInfo.OUTLETDIR == 1)
                {
                    FacePath = "./Image/Face_off.png";
                    BLPath = "./Image/BL_on.png";
                    FootPath = "./Image/Foot_off.png";
                    FDPath = "./Image/FD_off.png";
                    FrontGlassPath = "./Image/FrontGlass_off.png";
                }
                if (airConditionInfo.OUTLETDIR == 2)
                {
                    FacePath = "./Image/Face_off.png";
                    BLPath = "./Image/BL_off.png";
                    FootPath = "./Image/Foot_on.png";
                    FDPath = "./Image/FD_off.png";
                    FrontGlassPath = "./Image/FrontGlass_off.png";
                }
                if (airConditionInfo.OUTLETDIR == 3)
                {
                    FacePath = "./Image/Face_off.png";
                    BLPath = "./Image/BL_off.png";
                    FootPath = "./Image/Foot_off.png";
                    FDPath = "./Image/FD_on.png";
                    FrontGlassPath = "./Image/FrontGlass_off.png";
                }
                if (airConditionInfo.OUTLETDIR == 4)
                {
                    FacePath = "./Image/Face_off.png";
                    BLPath = "./Image/BL_off.png";
                    FootPath = "./Image/Foot_off.png";
                    FDPath = "./Image/FD_off.png";
                    FrontGlassPath = "./Image/FrontGlass_on.png";
                }
            }
            //刷新风速
            if (airConditionInfo.BLMLVL == 0) 
            {
                BLWPath = "./Image/BLW0.png";
            }
            if (airConditionInfo.BLMLVL == 1)
            {
                BLWPath = "./Image/BLW1.png";
            }
            if (airConditionInfo.BLMLVL == 2)
            {
                BLWPath = "./Image/BLW2.png";
            }
            if (airConditionInfo.BLMLVL == 3)
            {
                BLWPath = "./Image/BLW3.png";
            }
            if (airConditionInfo.BLMLVL == 4)
            {
                BLWPath = "./Image/BLW4.png";
            }
            if (airConditionInfo.BLMLVL == 5)
            {
                BLWPath = "./Image/BLW5.png";
            }
            if (airConditionInfo.BLMLVL == 6)
            {
                BLWPath = "./Image/BLW6.png";
            }
            if (airConditionInfo.BLMLVL == 7)
            {
                BLWPath = "./Image/BLW7.png";
            }
        }

        private void Can_ReceviedData(object sender, CANFrameInfoArgs e)
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
        PanelMSG panelMSG = new PanelMSG();
        CANhelper can = new CANhelper();
        int firstTime = 0;
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name.Equals("power") && firstTime == 0) 
            {
                    can.CanSetting.SetCAN((uint)0, //deviceIndex 设备索引号
                                                  (byte)0, //canIndex CAN的路索引号
                                                  (byte)CanFilterType.DualFilter, //filterType 过滤类型
                                                  (byte)CanMode.NormalMode, //canMode CAN模式
                                                  "00000000", //canCode 过滤码
                                                  "FFFFFFFF", //canMask 掩码
                                                  "00", //时间高位
                                                  "1C"); //时间低位,默认 00 1C 500Mps
                    firstTime = 1;
            }         
            if (firstTime != 0 )
            {
                if(!((airConditionInfo.POWER == 0) && (!btn.Name.Equals("power"))))
                {
                    panelMSG.getValueFromAirConditionInfo(airConditionInfo, (char)airConditionInfo.POWER);
                    if (!btn.Name.Equals("power"))
                    {
                        if (btn.Name.Equals("InnerCycle") || btn.Name.Equals("OutCycle"))
                        {
                            panelMSG.ILETSET = (char)((panelMSG.ILETSET + 1) % 2);
                        }
                        if (btn.Name.Equals("BackGlass")) 
                        {
                            panelMSG.RRDEF = (char)((panelMSG.RRDEF + 1) % 2);
                        }
                        if (btn.Name.Equals("Face")) {
                            panelMSG.OLETSET = (char)0;
                            panelMSG.FRDEF = (char)0;
                        }
                        if (btn.Name.Equals("BL"))
                        {
                            panelMSG.OLETSET = (char)1;
                            panelMSG.FRDEF = (char)0;
                        }
                        if (btn.Name.Equals("Foot"))
                        {
                            panelMSG.OLETSET = (char)2;
                            panelMSG.FRDEF = (char)0;
                        }
                        if (btn.Name.Equals("FD"))
                        {
                            panelMSG.OLETSET = (char)3;
                            panelMSG.FRDEF = (char)1;
                        }
                        if (btn.Name.Equals("FrontGlass"))
                        {
                            if (panelMSG.OLETSET != 4 && panelMSG.FRDEF != 1)
                            {
                                panelMSG.OLETSET = (char)4;
                                panelMSG.FRDEF = (char)1;
                            }
                            else{
                                panelMSG.OLETSET = (char)0;
                                panelMSG.FRDEF = (char)0;
                            }
                        }
                        if (btn.Name.Equals("BigFan") && panelMSG.BLWSET < 7) {
                            panelMSG.BLWSET = (char)(panelMSG.BLWSET + 1);
                        }
                        if (btn.Name.Equals("SmallFan") && panelMSG.BLWSET > 0)
                        {
                            panelMSG.BLWSET = (char)(panelMSG.BLWSET - 1);
                        }
                    }
                    else
                    {
                        if (airConditionInfo.POWER == 0)
                        {
                            airConditionInfo.POWER = 1;
                            can.ConnectCANDevice(); //连接CAN设备
                            Console.WriteLine("连接成功");
                            can.StartCAN(); //启动CAN设备
                            Console.WriteLine("启动成功");
                            can.ReceviedData += Can_ReceviedData;
                        }
                        else
                        {
                            airConditionInfo.POWER = 0;
                            can.CloseCANDevice();
                            Console.WriteLine("关闭成功");
                            can.ReceviedData -= Can_ReceviedData;
                            airConditionInfo = new AirConditionInfo();
                            RefreshPanel();
                        }
                    }
                    buttonSendFrameData();
                }
            }
        }
        private void buttonSendFrameData()
        {
            string str = panelMSG.getCANFrameData();
            Console.WriteLine("发送   ID = " + "00000222" + "  Data = " + str);
            can.SendData("00000222", DateTime.Now.ToString(), 0, 0, str, 0);
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
            Console.WriteLine("发送   ID = "+e.frameID + "  Data = " + e.frameData);
            for (int i = 0; i <= e.frameNumber; i++) {
                can.SendData(e.frameID, DateTime.Now.ToString(), 0, 0, e.frameData, 0);
            }
        }
    }
}