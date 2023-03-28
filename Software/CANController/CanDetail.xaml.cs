using CAN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace CANController
{
    /// <summary>
    /// CanDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CanDetail : Window , INotifyPropertyChanged
    {
        public CanDetail()
        {
            InitializeComponent();
        }

        public void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            FrameInfo message = e.CanFrameInfo;
            //Console.WriteLine("2号窗收到消息");
            //Console.WriteLine("ID   " + message.FrameID + "   Data    " + message.Data);
            CurrentProgress += "接收时间：" + DateTime.Now.ToString() + "   帧ID： " + message.FrameID + "   帧格式：" + message.FrameFormat + "   " + message.FrameType + "   帧数据：" 
                + message.Data[0] + message.Data[1] + ' '
                + message.Data[2] + message.Data[3] + ' '
                + message.Data[4] + message.Data[5] + ' '
                + message.Data[6] + message.Data[7] + "\n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private String currentProgress = "";
        public String CurrentProgress
        {
            get { return currentProgress; }
            set
            {
                currentProgress = value;
                OnPropertyChanged("CurrentProgress");
            }
        }

        private void CAN_SendData(object sender, RoutedEventArgs e)
        {
        }
    }
}
